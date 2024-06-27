using System.Threading.Tasks;
using FootballMatchCrudApi.Dto;
using FootballMatchCrudApi.Matches.Controller;
using FootballMatchCrudApi.Matches.Controller.Interfaces;
using FootballMatchCrudApi.Matches.Service.Interfaces;
using FootballMatchCrudApi.System.Constant;
using FootballMatchCrudApi.System.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using tests.Helpers;
using Xunit;

namespace tests.UnitTests;

public class TestController
{
    Mock<IMatchCommandService> _command;
    Mock<IMatchQueryService> _query;
    MatchApiController _controller;

    public TestController()
    {
        _command = new Mock<IMatchCommandService>();
        _query = new Mock<IMatchQueryService>();
        _controller = new MatchControler(_command.Object, _query.Object);
    }
    
    [Fact]
    public async Task GetAll_ItemsDoNotExist()
    {

        _query.Setup(repo => repo.GetAllMatches()).ThrowsAsync(new ItemDoesNotExist(Constants.MATCH_DOES_NOT_EXIST));
           
        var result = await _controller.GetAll();

        var notFound = Assert.IsType<NotFoundObjectResult>(result.Result);

        Assert.Equal(404, notFound.StatusCode);
        Assert.Equal(Constants.MATCH_DOES_NOT_EXIST, notFound.Value);

    }
    
    [Fact]
    public async Task GetAll_ValidData()
    {

        var matches = TestMatchFactory.CreateMatches(5);

        _query.Setup(repo => repo.GetAllMatches()).ReturnsAsync(matches);

        var result = await _controller.GetAll();
        var okresult = Assert.IsType<OkObjectResult>(result.Result);
        var matchesAll = Assert.IsType<ListMatchDto>(okresult.Value);

        Assert.Equal(5, matchesAll.matchList.Count);
        Assert.Equal(200, okresult.StatusCode);


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

        _command.Setup(repo => repo.CreateMatch(It.IsAny<CreateMatchRequest>())).ThrowsAsync(new ItemAlreadyExists(Constants.MATCH_ALREADY_EXIST));

        var result = await _controller.CreateMatch(create);

        var bad=Assert.IsType<BadRequestObjectResult>(result.Result);

        Assert.Equal(400,bad.StatusCode);
        Assert.Equal(Constants.MATCH_ALREADY_EXIST, bad.Value);

    }
    
    [Fact]
    public async Task Create_ValidData()
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

        _command.Setup(repo => repo.CreateMatch(create)).ReturnsAsync(match);

        var result = await _controller.CreateMatch(create);

        var okResult= Assert.IsType<CreatedResult>(result.Result);

        Assert.Equal(okResult.StatusCode, 201);
        Assert.Equal(match, okResult.Value);

    }
    
    [Fact]
    public async Task Update_InvalidDate()
    {
        var update = new UpdateMatchRequest()
        {
            Stadium="test",
            Score="2-3",
            Country="Test"
        };

        _command.Setup(repo => repo.UpdateMatch(11, update)).ThrowsAsync(new ItemDoesNotExist(Constants.MATCH_DOES_NOT_EXIST));

        var result = await _controller.UpdateMatch(11, update);

        var bad = Assert.IsType<NotFoundObjectResult>(result.Result);

        Assert.Equal(bad.StatusCode, 404);
        Assert.Equal(bad.Value, Constants.MATCH_DOES_NOT_EXIST);

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

        var match=TestMatchFactory.CreateMatch(5);
        match.Stadium=update.Stadium;
        match.Score=update.Score;
        match.Country=update.Country;

        _command.Setup(repo=>repo.UpdateMatch(5,update)).ReturnsAsync(match);

        var result = await _controller.UpdateMatch(5, update);

        var okResult=Assert.IsType<OkObjectResult>(result.Result);

        Assert.Equal(okResult.StatusCode, 200);
        Assert.Equal(okResult.Value, match);

    }
    
    [Fact]
    public async Task Delete_ItemDoesNotExist()
    {

        _command.Setup(repo=>repo.DeleteMatch(2)).ThrowsAsync(new ItemDoesNotExist(Constants.MATCH_DOES_NOT_EXIST));

        var result= await _controller.DeleteMatch(2);

        var notfound= Assert.IsType<NotFoundObjectResult>(result.Result);

        Assert.Equal(notfound.StatusCode, 404);
        Assert.Equal(notfound.Value, Constants.MATCH_DOES_NOT_EXIST);

    }
    
    [Fact]
    public async Task Delete_ValidData()
    {
        var match = TestMatchFactory.CreateMatch(1);

        _command.Setup(repo => repo.DeleteMatch(1)).ReturnsAsync(match);

        var result = await _controller.DeleteMatch(1);

        var okResult=Assert.IsType<AcceptedResult>(result.Result);

        Assert.Equal(202, okResult.StatusCode);
        Assert.Equal(match, okResult.Value);

    }
    
}