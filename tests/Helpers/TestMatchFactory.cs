using FootballMatchCrudApi.Dto;

namespace tests.Helpers;

public class TestMatchFactory
{
    public static MatchDto CreateMatch(int id)
    {
        return new MatchDto
        {
            Id = id,
            Stadium="Camo nou"+id,
            Score="2-1"+id,
            Country="romania"+id
        };
    }

    public static ListMatchDto CreateMatches(int count)
    {
        ListMatchDto matches=new ListMatchDto();
            
        for(int i = 0; i<count; i++)
        {
            matches.matchList.Add(CreateMatch(i));
        }
        return matches;
    }
}