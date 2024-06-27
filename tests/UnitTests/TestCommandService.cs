using System.Threading.Tasks;
using FootballMatchCrudApi.Dto;
using FootballMatchCrudApi.Matches.Repository;
using FootballMatchCrudApi.Matches.Repository.interfaces;
using FootballMatchCrudApi.Matches.Service;
using FootballMatchCrudApi.Matches.Service.Interfaces;
using FootballMatchCrudApi.System.Constant;
using FootballMatchCrudApi.System.Exceptions;
using Moq;
using tests.Helpers;
using Xunit;

namespace tests.UnitTests;

public class TestCommandService
{
    Mock<IFootballMatchRepository> _mock;
    IMatchCommandService _service;

    public TestCommandService()
    {
        _mock = new Mock<IFootballMatchRepository>();
        _service = new MatchCommandService(_mock.Object);
    }

    [Fact]
    public async Task Create_InvalidData()
    {
        var create = new CreateMatchRequest()
        {
            Stadium="test",
            Score="2-3",
            Country="Test"
        };


        var match = TestMatchFactory.CreateMatch(5);

        _mock.Setup(repo => repo.GetByScoreAsync("2-3")).ReturnsAsync(match);
                
        var exception=  await Assert.ThrowsAsync<ItemAlreadyExists>(()=>_service.CreateMatch(create));

        Assert.Equal(Constants.MATCH_ALREADY_EXIST, exception.Message);

    }
    
    [Fact]
    public async Task Create_ReturnMatch()
    {

        var create = new CreateMatchRequest()
        {
            Stadium="test",
            Score="2-3",
            Country="Test"
        };

        var match = TestMatchFactory.CreateMatch(5);

        match.Stadium=create.Stadium;
        match.Score=create.Score;
        match.Country=create.Country;

        _mock.Setup(repo => repo.CreateMatch(It.IsAny<CreateMatchRequest>())).ReturnsAsync(match);

        var result = await _service.CreateMatch(create);

        Assert.NotNull(result);
        Assert.Equal(result, match);
    }
    
    [Fact]
    public async Task Update_ItemDoesNotExist()
    {
        var update = new UpdateMatchRequest()
        {
            Stadium="test",
            Score="2-3",
            Country="Test"
        };

        _mock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((MatchDto)null);

        var exception = await Assert.ThrowsAsync<ItemDoesNotExist>(() => _service.UpdateMatch(1, update));

        Assert.Equal(Constants.MATCH_DOES_NOT_EXIST, exception.Message);

    }
    
    [Fact]
    public async Task Update_InvalidData()
    {
        var update = new UpdateMatchRequest()
        {
            Stadium="test",
            Score="2-3",
            Country="Test"
        };

        _mock.Setup(repo=>repo.GetByIdAsync(1)).ReturnsAsync((MatchDto)null);

        var exception = await Assert.ThrowsAsync<ItemDoesNotExist>(() => _service.UpdateMatch(5, update));

        Assert.Equal(Constants.MATCH_DOES_NOT_EXIST, exception.Message);

    }
    
    [Fact]
    public async Task Update_ValidData()
    {
        var update = new UpdateMatchRequest()
        {
            Stadium="test",
            Score="2-3",
            Country="Test"
        };
        var match = TestMatchFactory.CreateMatch(5);

        match.Stadium=update.Stadium;
        match.Score=update.Score;
        match.Country=update.Country;

        _mock.Setup(repo => repo.GetByIdAsync(5)).ReturnsAsync(match);
        _mock.Setup(repoo => repoo.UpdateMatch(It.IsAny<int>(), It.IsAny<UpdateMatchRequest>())).ReturnsAsync(match);

        var result = await _service.UpdateMatch(5, update);

        Assert.NotNull(result);
        Assert.Equal(match, result);

    }
    
    [Fact]
    public async Task Delete_ItemDoesNotExist()
    {

        _mock.Setup(repo => repo.DeleteMatch(It.IsAny<int>())).ReturnsAsync((MatchDto)null);

        var exception = await Assert.ThrowsAsync<ItemDoesNotExist>(() => _service.DeleteMatch(5));

        Assert.Equal(exception.Message, Constants.MATCH_DOES_NOT_EXIST);

    }
    
    [Fact]
    public async Task Delete_ValidData()
    {
        var match = TestMatchFactory.CreateMatch(1);

        _mock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(match);

        var result= await _service.DeleteMatch(1);

        Assert.NotNull(result);
        Assert.Equal(match, result);


    }
    
    
}