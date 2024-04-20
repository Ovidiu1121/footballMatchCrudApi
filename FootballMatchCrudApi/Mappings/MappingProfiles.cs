using AutoMapper;
using FootballMatchCrudApi.Dto;
using FootballMatchCrudApi.Matches.Model;

namespace FootballMatchCrudApi.Mappings
{
    public class MappingProfiles:Profile
    {

        public MappingProfiles()
        {
            CreateMap<CreateMatchRequest, FootballMatch>();
            CreateMap<UpdateMatchRequest, FootballMatch>();
        }

    }
}
