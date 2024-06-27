using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FootballMatchCrudApi.Dto;
using FootballMatchCrudApi.Matches.Model;
using Newtonsoft.Json;
using tests.Infrastructure;
using Xunit;

namespace tests.IntegrationTests;

public class MatchIntegrationTests: IClassFixture<ApiWebApplicationFactory>
{
    private readonly HttpClient _client;

    public MatchIntegrationTests(ApiWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }
    
    [Fact]
    public async Task Post_Create_ValidRequest_ReturnsCreatedStatusCode_ValidProductContentResponse()
    {
        var request = "/api/v1/MatchController/create";
        var match = new CreateMatchRequest() { Stadium = "new stadium", Score = "new score", Country = "new country" };
        var content = new StringContent(JsonConvert.SerializeObject(match), Encoding.UTF8, "application/json");

        var response = await _client.PostAsync(request, content);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);

        var responseString = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<FootballMatch>(responseString);

        Assert.NotNull(result);
        Assert.Equal(match.Stadium, result.Stadium);
        Assert.Equal(match.Score, result.Score);
        Assert.Equal(match.Country, result.Country);
    }
    
    [Fact]
    public async Task Post_Create_MatchAlreadyExists_ReturnsBadRequestStatusCode()
    {
        var request = "/api/v1/MatchController/create";
        var match = new CreateMatchRequest() { Stadium = "new stadium", Score = "new score", Country = "new country" };
        var content = new StringContent(JsonConvert.SerializeObject(match), Encoding.UTF8, "application/json");

        await _client.PostAsync(request, content);
        var response = await _client.PostAsync(request, content);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        
    }
    
    [Fact]
    public async Task Put_Update_ValidRequest_ReturnsAcceptedStatusCode_ValidProductContentResponse()
    {
        var request = "/api/v1/MatchController/create";
        var match = new CreateMatchRequest() { Stadium = "new stadium", Score = "new score", Country = "new country" };
        var content = new StringContent(JsonConvert.SerializeObject(match), Encoding.UTF8, "application/json");

        var response = await _client.PostAsync(request, content);
        var responseString = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<FootballMatch>(responseString)!;

        request = "/api/v1/MatchController/update/"+result.Id;
        var updateMatch = new UpdateMatchRequest() { Stadium = "updated stadium", Score = "updated score", Country = "updated country" };
        content = new StringContent(JsonConvert.SerializeObject(updateMatch), Encoding.UTF8, "application/json");

        response = await _client.PutAsync(request, content);
        
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        responseString = await response.Content.ReadAsStringAsync();
        result = JsonConvert.DeserializeObject<FootballMatch>(responseString)!;

        Assert.Equal(updateMatch.Stadium, result.Stadium);
        Assert.Equal(updateMatch.Score, result.Score);
        Assert.Equal(updateMatch.Country, result.Country);
    }
    
    [Fact]
    public async Task Put_Update_MatchDoesNotExists_ReturnsNotFoundStatusCode()
    {
        
        var request = "/api/v1/MatchController/update/1";
        var updateMatch = new UpdateMatchRequest() { Stadium = "updated stadium", Score = "updated score", Country = "updated country" };
        var content = new StringContent(JsonConvert.SerializeObject(updateMatch), Encoding.UTF8, "application/json");

        var response = await _client.PutAsync(request, content);
        
        Assert.Equal(HttpStatusCode.NotFound,response.StatusCode);
        
    }
    
    [Fact]
    public async Task Delete_Delete_MatchExists_ReturnsDeletedMatch()
    {

        var request = "/api/v1/MatchController/create";
        var match = new CreateMatchRequest() { Stadium = "new stadium", Score = "new score", Country = "new country" };
        var content = new StringContent(JsonConvert.SerializeObject(match), Encoding.UTF8, "application/json");

        var response = await _client.PostAsync(request, content);
        var responseString = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<FootballMatch>(responseString)!;

        request = "/api/v1/MatchController/delete/" + result.Id;
        response = await _client.DeleteAsync(request);
        
        Assert.Equal(HttpStatusCode.Accepted,response.StatusCode);
    }
    
    
    
    
    
    
    
}