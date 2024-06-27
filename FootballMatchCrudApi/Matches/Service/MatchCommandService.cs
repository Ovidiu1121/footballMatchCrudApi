using FootballMatchCrudApi.Dto;
using FootballMatchCrudApi.Matches.Model;
using FootballMatchCrudApi.Matches.Repository.interfaces;
using FootballMatchCrudApi.Matches.Service.Interfaces;
using FootballMatchCrudApi.System.Constant;
using FootballMatchCrudApi.System.Exceptions;

namespace FootballMatchCrudApi.Matches.Service
{
    public class MatchCommandService: IMatchCommandService
    {

        private IFootballMatchRepository _repository;

        public MatchCommandService(IFootballMatchRepository repository)
        {
            _repository = repository;
        }

        public async Task<MatchDto> CreateMatch(CreateMatchRequest request)
        {
            MatchDto match = await _repository.GetByScoreAsync(request.Score);

            if (match!=null)
            {
                throw new ItemAlreadyExists(Constants.MATCH_ALREADY_EXIST);
            }

            match=await _repository.CreateMatch(request);
            return match;
        }

        public async Task<MatchDto> DeleteMatch(int id)
        {
            MatchDto match = await _repository.GetByIdAsync(id);

            if (match==null)
            {
                throw new ItemDoesNotExist(Constants.MATCH_DOES_NOT_EXIST);
            }

            await _repository.DeleteMatch(id);
            return match;
        }

        public async Task<MatchDto> UpdateMatch(int id,UpdateMatchRequest request)
        {
            MatchDto match = await _repository.GetByIdAsync(id);

            if (match==null)
            {
                throw new ItemDoesNotExist(Constants.MATCH_DOES_NOT_EXIST);
            }

            match = await _repository.UpdateMatch(id,request);
            return match;
        }
    }
}
