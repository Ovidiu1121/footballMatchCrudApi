namespace FootballMatchCrudApi.Dto;

public class ListMatchDto
{
    public ListMatchDto()
    {
        matchList = new List<MatchDto>();
    }
    
    public List<MatchDto> matchList { get; set; }
}