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

        public async Task<IEnumerable<FootballMatch>> GetAllMatches()
        {

            IEnumerable<FootballMatch> matches = await _repository.GetAllAsync();

            if (matches.Count().Equals(0))
            {
                throw new ItemDoesNotExist(Constants.NO_MATCHES_EXIST);
            }

            return matches;
        }

        public async Task<FootballMatch> GetByScore(string score)
        {
            FootballMatch match = await _repository.GetByScoreAsync(score);

            if (match == null)
            {
                throw new ItemDoesNotExist(Constants.MATCH_DOES_NOT_EXIST);
            }

            return match;
        }

        public async Task<FootballMatch> GetById(int id)
        {
            FootballMatch match = await _repository.GetByIdAsync(id);

            if (match == null)
            {
                throw new ItemDoesNotExist(Constants.MATCH_DOES_NOT_EXIST);
            }

            return match;
        }
    }
}
