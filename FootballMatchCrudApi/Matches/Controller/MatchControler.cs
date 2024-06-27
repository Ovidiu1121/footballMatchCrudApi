using FootballMatchCrudApi.Dto;
using FootballMatchCrudApi.Matches.Controller.Interfaces;
using FootballMatchCrudApi.Matches.Model;
using FootballMatchCrudApi.Matches.Repository.interfaces;
using FootballMatchCrudApi.Matches.Service.Interfaces;
using FootballMatchCrudApi.System.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace FootballMatchCrudApi.Matches.Controller
{
    public class MatchControler: MatchApiController
    {
        private IMatchCommandService _matchCommandService;
        private IMatchQueryService _matchQueryService;

        public MatchControler(IMatchCommandService matchCommandService, IMatchQueryService matchQueryService)
        {
            _matchCommandService=matchCommandService;
            _matchQueryService=matchQueryService;
        }

        public override async Task<ActionResult<MatchDto>> CreateMatch([FromBody] CreateMatchRequest request)
        {
            try
            {
                var matches = await _matchCommandService.CreateMatch(request);

                return Created("Mathc-ul a fost adaugat",matches);
            }
            catch (ItemAlreadyExists ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public override async Task<ActionResult<MatchDto>> DeleteMatch([FromRoute] int id)
        {
            try
            {
                var matches = await _matchCommandService.DeleteMatch(id);

                return Accepted("", matches);
            }
            catch (ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }

        public override async Task<ActionResult<ListMatchDto>> GetAll()
        {

            try
            {
                var matches = await _matchQueryService.GetAllMatches();
                return Ok(matches);
            }
            catch (ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }

        public override async Task<ActionResult<MatchDto>> GetByIdRoute([FromRoute] int id)
        {
            try
            {
                var matches = await _matchQueryService.GetById(id);
                return Ok(matches);
            }
            catch (ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }
        
        public override async Task<ActionResult<MatchDto>> GetByScoreRoute([FromRoute] string score)
        {
            try
            {
                var matches = await _matchQueryService.GetByScore(score);
                return Ok(matches);
            }
            catch (ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }
        
        public override async Task<ActionResult<MatchDto>> GetByStadiumRoute([FromRoute] string stadium)
        {
            try
            {
                var matches = await _matchQueryService.GetByStadium(stadium);
                return Ok(matches);
            }
            catch (ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }
        
        public override async Task<ActionResult<MatchDto>> GetByCountryRoute([FromRoute] string country)
        {
            try
            {
                var matches = await _matchQueryService.GetByCountry(country);
                return Ok(matches);
            }
            catch (ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }

        public override async Task<ActionResult<MatchDto>> UpdateMatch([FromRoute]int id, [FromBody] UpdateMatchRequest request)
        {
            try
            {
                var matches = await _matchCommandService.UpdateMatch(id,request);

                return Ok(matches);
            }
            catch (ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
