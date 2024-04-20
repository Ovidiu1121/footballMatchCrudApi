using FootballMatchCrudApi.Dto;
using FootballMatchCrudApi.Matches.Model;

namespace FootballMatchCrudApi.Matches.Repository.interfaces
{
    public interface IFootballMatchRepository
    {
        Task<IEnumerable<FootballMatch>> GetAllAsync();
        Task<FootballMatch> GetByIdAsync(int id);
        Task<FootballMatch> GetByScoreAsync(string score);
        Task<FootballMatch> CreateMatch(CreateMatchRequest request);
        Task<FootballMatch> UpdateMatch(int id,UpdateMatchRequest request);
        Task<FootballMatch> DeleteMatch(int id);
    }
}
