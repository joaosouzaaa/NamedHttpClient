using Moq;
using Moq.Protected;
using NamedHttpClient.API.Constants;
using NamedHttpClient.API.DataTransferObjects.Cep;
using NamedHttpClient.API.Services;
using System.Net;
using System.Text.Json;

namespace UnitTests.ServicesTests;

public sealed class ViaCepServiceTests
{
    private readonly Mock<IHttpClientFactory> _httpClientFactoryMock;
    private readonly Mock<HttpMessageHandler> _httpMessageHandlerMock;
    private readonly ViaCepService _viaCepService;

    public ViaCepServiceTests()
    {
        _httpClientFactoryMock = new Mock<IHttpClientFactory>();
        _httpMessageHandlerMock = new Mock<HttpMessageHandler>();
        _viaCepService = new ViaCepService(_httpClientFactoryMock.Object);
    }

    [Fact]
    public async Task GetZipCodeInfoAsync_SuccessfulScenario_ReturnsResponse()
    {
        // A
        var viaCepResponse = new ViaCepResponse(
            "test",
            "asd",
            null,
            "test",
            "asdasd",
            "ufu",
            "asd");
        var viaCepJsonString = JsonSerializer.Serialize(viaCepResponse);
        var httpResponseMessage = new HttpResponseMessage()
        {
            Content = new StringContent(viaCepJsonString),
            StatusCode = HttpStatusCode.OK
        };
        _httpMessageHandlerMock.Protected().Setup<Task<HttpResponseMessage>>("SendAsync",
            ItExpr.IsAny<HttpRequestMessage>(),
            ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(httpResponseMessage);

        const string baseAddress = "https://localhost";

        var httpClient = new HttpClient(_httpMessageHandlerMock.Object)
        {
            BaseAddress = new Uri(baseAddress)
        };
        _httpClientFactoryMock.Setup(h => h.CreateClient(HttpClientNamesConstants.ViaCepHttpClientName))
            .Returns(httpClient);

        // A
        var viaCepResponseResult = await _viaCepService.GetZipCodeInfoAsync("test", default);

        // A
        Assert.NotNull(viaCepResponseResult);
    }

    [Fact]
    public async Task GetZipCodeInfoAsync_InvalidHttpStatusCode_ReturnsNull()
    {
        // A
        var httpResponseMessage = new HttpResponseMessage()
        {
            StatusCode = HttpStatusCode.BadRequest
        };
        _httpMessageHandlerMock.Protected().Setup<Task<HttpResponseMessage>>("SendAsync",
            ItExpr.IsAny<HttpRequestMessage>(),
            ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(httpResponseMessage);

        const string baseAddress = "https://localhost";

        var httpClient = new HttpClient(_httpMessageHandlerMock.Object)
        {
            BaseAddress = new Uri(baseAddress)
        };
        _httpClientFactoryMock.Setup(h => h.CreateClient(HttpClientNamesConstants.ViaCepHttpClientName))
            .Returns(httpClient);

        // A
        var viaCepResponseResult = await _viaCepService.GetZipCodeInfoAsync("test", default);

        // A
        Assert.Null(viaCepResponseResult);
    }
}
