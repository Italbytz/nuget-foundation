using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Italbytz.Common.Http;

/// <summary>
/// Minimal base class for HTTP JSON API clients with optional HttpClient ownership.
/// </summary>
public abstract class HttpJsonApiClientBase : IDisposable
{
    private readonly string _baseUrl;
    private readonly bool _disposeHttpClient;
    private readonly JsonSerializerOptions _serializerOptions;

    protected HttpJsonApiClientBase(string baseUrl, HttpClient? httpClient = null, JsonSerializerOptions? serializerOptions = null)
    {
        if (string.IsNullOrWhiteSpace(baseUrl))
        {
            throw new ArgumentException("Base URL must not be empty.", nameof(baseUrl));
        }

        HttpClient = httpClient ?? new HttpClient();
        _disposeHttpClient = httpClient is null;
        _baseUrl = baseUrl;
        _serializerOptions = serializerOptions ?? CreateDefaultSerializerOptions();
    }

    protected HttpClient HttpClient { get; }

    protected virtual JsonSerializerOptions CreateDefaultSerializerOptions()
    {
        return new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
    }

    protected string BuildPath(string resource, IEnumerable<KeyValuePair<string, string>> parameters)
    {
        if (resource is null)
        {
            throw new ArgumentNullException(nameof(resource));
        }

        if (parameters is null)
        {
            throw new ArgumentNullException(nameof(parameters));
        }

        var materializedParameters = parameters.ToArray();
        if (materializedParameters.Length == 0)
        {
            return resource;
        }

        var query = string.Join("&", materializedParameters.Select(parameter => $"{Uri.EscapeDataString(parameter.Key)}={Uri.EscapeDataString(parameter.Value)}"));
        return $"{resource}?{query}";
    }

    protected async Task<T?> GetAsync<T>(string relativePath, CancellationToken cancellationToken = default)
    {
        using var response = await HttpClient.GetAsync(BuildAbsoluteUri(relativePath), cancellationToken).ConfigureAwait(false);
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        return JsonSerializer.Deserialize<T>(json, _serializerOptions);
    }

    protected string BuildAbsoluteUri(string relativePath)
    {
        return $"{_baseUrl.TrimEnd('/')}/{relativePath.TrimStart('/')}";
    }

    public void Dispose()
    {
        if (_disposeHttpClient)
        {
            HttpClient.Dispose();
        }
    }
}