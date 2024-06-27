using FootballMatchCrudApi.Dto;
using FootballMatchCrudApi.Matches.Model;

namespace FootballMatchCrudApi.Matches.Service.Interfaces
{
    public interface IMatchCommandService
    {
        Task<MatchDto> CreateMatch(CreateMatchRequest request);
        Task<MatchDto> UpdateMatch(int id,UpdateMatchRequest request);
        Task<MatchDto> DeleteMatch(int id);

    }
}
