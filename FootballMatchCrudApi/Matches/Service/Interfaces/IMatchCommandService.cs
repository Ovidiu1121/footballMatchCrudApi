using FootballMatchCrudApi.Dto;
using FootballMatchCrudApi.Matches.Model;

namespace FootballMatchCrudApi.Matches.Service.Interfaces
{
    public interface IMatchCommandService
    {
        Task<FootballMatch> CreateMatch(CreateMatchRequest request);
        Task<FootballMatch> UpdateMatch(int id,UpdateMatchRequest request);
        Task<FootballMatch> DeleteMatch(int id);

    }
}
