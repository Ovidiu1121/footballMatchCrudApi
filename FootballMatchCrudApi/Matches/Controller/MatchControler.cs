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

        public override async Task<ActionResult<FootballMatch>> CreateMatch([FromBody] CreateMatchRequest request)
        {
            try
            {
                var matches = await _matchCommandService.CreateMatch(request);

                return Ok(matches);
            }
            catch (ItemAlreadyExists ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public override async Task<ActionResult<FootballMatch>> DeleteMatch([FromRoute] int id)
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

        public override async Task<ActionResult<IEnumerable<FootballMatch>>> GetAll()
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

        public override async Task<ActionResult<FootballMatch>> GetByIdRoute([FromRoute] int id)
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

        public override async Task<ActionResult<FootballMatch>> UpdateMatch([FromRoute]int id, [FromBody] UpdateMatchRequest request)
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
