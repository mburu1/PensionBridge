using System.Security.Cryptography;
using CsvHelper;
using CsvHelper.Configuration;
using PensionBridge.Application.Abstractions;
using PensionBridge.Application.Ingestion;
using PensionBridge.Domain.Batches;
using PensionBridge.Domain.Common;
using PensionBridge.Domain.Contributions;
using PensionBridge.Domain.Members;

namespace PensionBridge.Infrastructure.Ingestion;

public class ContributionIngestionService : IContributionIngestionService
{
    private readonly IRepository<ContributionBatch> _batchRepository;
    private readonly IRepository<ContributionLine> _lineRepository;
    private readonly IRepository<Member> _memberRepository;

    public ContributionIngestionService(
        IRepository<ContributionBatch> batchRepository,
        IRepository<ContributionLine> lineRepository,
        IRepository<Member> memberRepository)
    {
        _batchRepository = batchRepository;
        _lineRepository = lineRepository;
        _memberRepository = memberRepository;
    }

    public async Task<ContributionBatch> IngestCsvAsync(
        Guid employerId,
        Guid schemeId,
        string period,
        Stream csvStream,
        string fileName,
        CancellationToken cancellationToken = default)
    {
        // Calculate file hash for idempotency
        var fileHash = await CalculateFileHashAsync(csvStream, cancellationToken);
        csvStream.Position = 0; // Reset stream position

        // Check for duplicate batch using idempotency key
        var idempotencyKey = $"{employerId}_{schemeId}_{period}_{fileHash}";
        var existingBatches = await _batchRepository.GetAllAsync(cancellationToken);
        if (existingBatches.Any(b => b.IdempotencyKey == idempotencyKey))
        {
            throw new InvalidOperationException("A batch with the same content already exists");
        }

        // Parse CSV
        var records = ParseCsv(csvStream);
        
        // Create batch
        var totalAmount = CalculateTotalAmount(records);
        var batch = new ContributionBatch(
            Guid.NewGuid(),
            employerId,
            schemeId,
            period,
            fileName,
            fileHash,
            idempotencyKey,
            totalAmount);

        await _batchRepository.AddAsync(batch, cancellationToken);

        // Create contribution lines
        var lineNumber = 1;
        var acceptedCount = 0;
        var rejectedCount = 0;

        foreach (var record in records)
        {
            lineNumber++;
            
            // Validate member
            var members = await _memberRepository.GetAllAsync(cancellationToken);
            var member = members.FirstOrDefault(m => 
                m.SchemeId == schemeId && 
                (m.MemberNumber == record.MemberNumber || m.ExternalReference == record.MemberNumber));

            if (member == null)
            {
                rejectedCount++;
                continue;
            }

            // Create contribution line
            var line = new ContributionLine(
                Guid.NewGuid(),
                batch.Id,
                member.Id,
                lineNumber,
                new Money(record.EmployerContribution, "USD"),
                new Money(record.EmployeeContribution, "USD"),
                new Money(record.VoluntaryContribution, "USD"));

            await _lineRepository.AddAsync(line, cancellationToken);
            acceptedCount++;
        }

        // Update batch status
        batch.StartValidation();
        batch.CompleteValidation(lineNumber - 1, acceptedCount, rejectedCount);
        await _batchRepository.UpdateAsync(batch, cancellationToken);

        return batch;
    }

    private async Task<string> CalculateFileHashAsync(Stream stream, CancellationToken cancellationToken)
    {
        using var sha256 = SHA256.Create();
        var hashBytes = await sha256.ComputeHashAsync(stream, cancellationToken);
        return BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
    }

    private List<ContributionCsvRecord> ParseCsv(Stream stream)
    {
        using var reader = new StreamReader(stream);
        using var csv = new CsvReader(reader, new CsvConfiguration(System.Globalization.CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = true,
            Delimiter = ","
        });

        return csv.GetRecords<ContributionCsvRecord>().ToList();
    }

    private Money CalculateTotalAmount(List<ContributionCsvRecord> records)
    {
        var total = records.Sum(r => r.EmployerContribution + r.EmployeeContribution + r.VoluntaryContribution);
        return new Money(total, "USD");
    }

    private class ContributionCsvRecord
    {
        public string MemberNumber { get; set; } = string.Empty;
        public decimal EmployerContribution { get; set; }
        public decimal EmployeeContribution { get; set; }
        public decimal VoluntaryContribution { get; set; }
    }
}
