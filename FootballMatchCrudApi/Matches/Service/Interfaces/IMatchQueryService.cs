using FootballMatchCrudApi.Dto;
using FootballMatchCrudApi.Matches.Model;

namespace FootballMatchCrudApi.Matches.Service.Interfaces
{
    public interface IMatchQueryService
    {
        Task<ListMatchDto> GetAllMatches();
        Task<MatchDto> GetByScore(string score);
        Task<MatchDto> GetById(int id);
        Task<MatchDto> GetByStadium(string stadium);
        Task<MatchDto> GetByCountry(string country);
    }
}
