using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Text.Json;

namespace PensionBridge.Infrastructure.BusinessCentral;

public class BusinessCentralClient : IBusinessCentralClient
{
    private readonly HttpClient _httpClient;
    private readonly BusinessCentralOptions _options;

    public BusinessCentralClient(HttpClient httpClient, IOptions<BusinessCentralOptions> options)
    {
        _httpClient = httpClient;
        _options = options.Value;
    }

    public async Task<string> GetAccessTokenAsync(CancellationToken cancellationToken = default)
    {
        var tokenRequest = new
        {
            grant_type = "client_credentials",
            client_id = _options.ClientId,
            client_secret = _options.ClientSecret,
            scope = _options.Scope
        };

        var content = new StringContent(JsonSerializer.Serialize(tokenRequest), System.Text.Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync(_options.TokenUrl, content, cancellationToken);
        response.EnsureSuccessStatusCode();
        
        var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);
        var tokenResponse = JsonSerializer.Deserialize<TokenResponse>(responseContent);
        return tokenResponse?.AccessToken ?? string.Empty;
    }

    public async Task<string> PostContributionAsync(string payload, CancellationToken cancellationToken = default)
    {
        var token = await GetAccessTokenAsync(cancellationToken);
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        
        var content = new StringContent(payload, System.Text.Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync($"{_options.BaseUrl}/v2.0/companies({_options.CompanyId})/contributionBatches", content, cancellationToken);
        response.EnsureSuccessStatusCode();
        
        return await response.Content.ReadAsStringAsync(cancellationToken);
    }

    private class TokenResponse
    {
        public string? AccessToken { get; set; }
    }
}

public class BusinessCentralOptions
{
    public string BaseUrl { get; set; } = string.Empty;
    public string TokenUrl { get; set; } = string.Empty;
    public string ClientId { get; set; } = string.Empty;
    public string ClientSecret { get; set; } = string.Empty;
    public string Scope { get; set; } = string.Empty;
    public string CompanyId { get; set; } = string.Empty;
}
