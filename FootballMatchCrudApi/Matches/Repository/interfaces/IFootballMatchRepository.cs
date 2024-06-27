using FootballMatchCrudApi.Dto;
using FootballMatchCrudApi.Matches.Model;

namespace FootballMatchCrudApi.Matches.Repository.interfaces
{
    public interface IFootballMatchRepository
    {
        Task<ListMatchDto> GetAllAsync();
        Task<MatchDto> GetByIdAsync(int id);
        Task<MatchDto> GetByScoreAsync(string score);
        Task<MatchDto> GetByStadiumAsync(string stadium);
        Task<MatchDto> GetByCountryAsync(string country);
        Task<MatchDto> CreateMatch(CreateMatchRequest request);
        Task<MatchDto> UpdateMatch(int id,UpdateMatchRequest request);
        Task<MatchDto> DeleteMatch(int id);
    }
}
