using PensionBridge.Application.Abstractions;
using PensionBridge.Domain.Posting;

namespace PensionBridge.Worker;

public class WorkerService
{
    private readonly IRepository<OutboxMessage> _outboxRepository;
    private readonly ILogger<WorkerService> _logger;

    public WorkerService(IRepository<OutboxMessage> outboxRepository, ILogger<WorkerService> logger)
    {
        _outboxRepository = outboxRepository;
        _logger = logger;
    }

    public async Task ProcessOutboxAsync(CancellationToken cancellationToken)
    {
        var messages = await _outboxRepository.GetAllAsync(cancellationToken);
        var pendingMessages = messages.Where(m => m.Status == OutboxStatus.Pending).ToList();

        foreach (var message in pendingMessages)
        {
            try
            {
                message.MarkAsProcessing();
                await _outboxRepository.UpdateAsync(message, cancellationToken);
                
                await Task.Delay(100, cancellationToken);
                
                message.MarkAsProcessed();
                await _outboxRepository.UpdateAsync(message, cancellationToken);
                _logger.LogInformation("Processed outbox message {MessageId}", message.Id);
            }
            catch (Exception ex)
            {
                message.IncrementRetry();
                if (message.RetryCount >= 3)
                {
                    message.MarkAsFailed();
                }
                await _outboxRepository.UpdateAsync(message, cancellationToken);
                _logger.LogError(ex, "Failed to process outbox message {MessageId}", message.Id);
            }
        }
    }
}
