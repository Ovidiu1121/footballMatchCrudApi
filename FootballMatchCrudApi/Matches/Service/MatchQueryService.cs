using FootballMatchCrudApi.Dto;
using FootballMatchCrudApi.Matches.Model;
using FootballMatchCrudApi.Matches.Repository.interfaces;
using FootballMatchCrudApi.Matches.Service.Interfaces;
using FootballMatchCrudApi.System.Constant;
using FootballMatchCrudApi.System.Exceptions;

namespace FootballMatchCrudApi.Matches.Service
{
    public class MatchQueryService: IMatchQueryService
    {

        private IFootballMatchRepository _repository;

        public MatchQueryService(IFootballMatchRepository repository)
        {
            _repository = repository;
        }

        public async Task<ListMatchDto> GetAllMatches()
        {

            ListMatchDto matches = await _repository.GetAllAsync();

            if (matches.matchList.Count().Equals(0))
            {
                throw new ItemDoesNotExist(Constants.NO_MATCHES_EXIST);
            }

            return matches;
        }

        public async Task<MatchDto> GetByScore(string score)
        {
            MatchDto match = await _repository.GetByScoreAsync(score);

            if (match == null)
            {
                throw new ItemDoesNotExist(Constants.MATCH_DOES_NOT_EXIST);
            }

            return match;
        }

        public async Task<MatchDto> GetById(int id)
        {
            MatchDto match = await _repository.GetByIdAsync(id);

            if (match == null)
            {
                throw new ItemDoesNotExist(Constants.MATCH_DOES_NOT_EXIST);
            }

            return match;
        }
        
        public async Task<MatchDto> GetByStadium(string stadium)
        {
            MatchDto match = await _repository.GetByStadiumAsync(stadium);

            if (match == null)
            {
                throw new ItemDoesNotExist(Constants.MATCH_DOES_NOT_EXIST);
            }

            return match;
        }
        
        public async Task<MatchDto> GetByCountry(string country)
        {
            MatchDto match = await _repository.GetByCountryAsync(country);

            if (match == null)
            {
                throw new ItemDoesNotExist(Constants.MATCH_DOES_NOT_EXIST);
            }

            return match;
        }
        
    }
}
