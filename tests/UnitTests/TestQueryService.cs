using System.Threading.Tasks;
using FootballMatchCrudApi.Dto;
using FootballMatchCrudApi.Matches.Repository.interfaces;
using FootballMatchCrudApi.Matches.Service;
using FootballMatchCrudApi.Matches.Service.Interfaces;
using FootballMatchCrudApi.System.Constant;
using FootballMatchCrudApi.System.Exceptions;
using Moq;
using tests.Helpers;
using Xunit;

namespace tests.UnitTests;

public class TestQueryService
{
    Mock<IFootballMatchRepository> _mock;
    IMatchQueryService _service;

    public TestQueryService()
    {
        _mock=new Mock<IFootballMatchRepository>();
        _service=new MatchQueryService(_mock.Object);
    }
    
    [Fact]
    public async Task GetAll_ItemsDoNotExist()
    {
        _mock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(new ListMatchDto());

        var exception = await Assert.ThrowsAsync<ItemDoesNotExist>(() => _service.GetAllMatches());

        Assert.Equal(exception.Message, Constants.NO_MATCHES_EXIST);       

    }
    
    [Fact]
    public async Task GetAll_ReturnAllMatches()
    {

        var matches = TestMatchFactory.CreateMatches(5);

        _mock.Setup(repo=>repo.GetAllAsync()).ReturnsAsync(matches);

        var result = await _service.GetAllMatches();

        Assert.NotNull(result);
        Assert.Contains(matches.matchList[1], result.matchList);

    }
    
    [Fact]
    public async Task GetById_ItemDoesNotExist()
    {

        _mock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((MatchDto)null);

        var exception = await Assert.ThrowsAsync<ItemDoesNotExist>(()=>_service.GetById(1));

        Assert.Equal(Constants.MATCH_DOES_NOT_EXIST, exception.Message);

    }
    
    [Fact]
    public async Task GetById_ReturnMatch()
    {

        var match = TestMatchFactory.CreateMatch(5);

        _mock.Setup(repo => repo.GetByIdAsync(5)).ReturnsAsync(match);

        var result = await _service.GetById(5);

        Assert.NotNull(result);
        Assert.Equal(match, result);

    }
    
    [Fact]
    public async Task GetByStadium_ItemDoesNotExist()
    {

        _mock.Setup(repo => repo.GetByStadiumAsync("")).ReturnsAsync((MatchDto)null);

        var exception = await Assert.ThrowsAsync<ItemDoesNotExist>(() => _service.GetByStadium(""));

        Assert.Equal(Constants.MATCH_DOES_NOT_EXIST, exception.Message);

    }
    
    [Fact]
    public async Task GetByStadium_ReturnMatch()
    {

        var match = TestMatchFactory.CreateMatch(5);

        match.Stadium="test";

        _mock.Setup(repo => repo.GetByStadiumAsync("test")).ReturnsAsync(match);

        var result = await _service.GetByStadium("test");

        Assert.NotNull(result);
        Assert.Equal(match, result);

    }
    
    [Fact]
    public async Task GetByScore_ItemDoesNotExist()
    {

        _mock.Setup(repo => repo.GetByScoreAsync("")).ReturnsAsync((MatchDto)null);

        var exception = await Assert.ThrowsAsync<ItemDoesNotExist>(() => _service.GetByScore(""));

        Assert.Equal(Constants.MATCH_DOES_NOT_EXIST, exception.Message);

    }
    
    [Fact]
    public async Task GetByScore_ReturnMatch()
    {

        var match = TestMatchFactory.CreateMatch(5);

        match.Score="test";

        _mock.Setup(repo => repo.GetByScoreAsync("test")).ReturnsAsync(match);

        var result = await _service.GetByScore("test");

        Assert.NotNull(result);
        Assert.Equal(match, result);

    }
    
    [Fact]
    public async Task GetByCountry_ItemDoesNotExist()
    {

        _mock.Setup(repo => repo.GetByCountryAsync("")).ReturnsAsync((MatchDto)null);

        var exception = await Assert.ThrowsAsync<ItemDoesNotExist>(() => _service.GetByCountry(""));

        Assert.Equal(Constants.MATCH_DOES_NOT_EXIST, exception.Message);

    }
    
    [Fact]
    public async Task GetByCountry_ReturnMatch()
    {

        var match = TestMatchFactory.CreateMatch(5);

        match.Country="test";

        _mock.Setup(repo => repo.GetByCountryAsync("test")).ReturnsAsync(match);

        var result = await _service.GetByCountry("test");

        Assert.NotNull(result);
        Assert.Equal(match, result);

    }
    
    
    
    
    
    
    
    
    
    
    
}