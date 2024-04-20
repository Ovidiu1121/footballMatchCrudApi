using FootballMatchCrudApi.Matches.Model;

namespace FootballMatchCrudApi.Matches.Service.Interfaces
{
    public interface IMatchQueryService
    {
        Task<IEnumerable<FootballMatch>> GetAllMatches();
        Task<FootballMatch> GetByScore(string score);
        Task<FootballMatch> GetById(int id);
    }
}
