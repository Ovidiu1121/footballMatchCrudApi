using AutoMapper;
using FootballMatchCrudApi.Data;
using FootballMatchCrudApi.Dto;
using FootballMatchCrudApi.Matches.Model;
using FootballMatchCrudApi.Matches.Repository.interfaces;
using Microsoft.EntityFrameworkCore;

namespace FootballMatchCrudApi.Matches.Repository
{
    public class FootballMatchRepository:IFootballMatchRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public FootballMatchRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<MatchDto> GetByCountryAsync(string country)
        {
            var match = await _context.Matches.Where(f => f.Country.Equals(country)).FirstOrDefaultAsync();
            
            return _mapper.Map<MatchDto>(match);
        }

        public async Task<MatchDto> CreateMatch(CreateMatchRequest request)
        {
            var match = _mapper.Map<FootballMatch>(request);

            _context.Matches.Add(match);

            await _context.SaveChangesAsync();

            return _mapper.Map<MatchDto>(match);
        }

        public async Task<MatchDto> DeleteMatch(int id)
        {
            var match = await _context.Matches.FindAsync(id);

            _context.Matches.Remove(match);

            await _context.SaveChangesAsync();

            return _mapper.Map<MatchDto>(match);
        }

        public async Task<ListMatchDto> GetAllAsync()
        {
            List<FootballMatch> result = await _context.Matches.ToListAsync();
            
            ListMatchDto listMatchDto = new ListMatchDto()
            {
                matchList = _mapper.Map<List<MatchDto>>(result)
            };

            return listMatchDto;
        }

        public async Task<MatchDto> GetByScoreAsync(string score)
        {
            var match = await _context.Matches.Where(f => f.Score.Equals(score)).FirstOrDefaultAsync();
            
            return _mapper.Map<MatchDto>(match);
        }
        
        public async Task<MatchDto> GetByIdAsync(int id)
        {
            var match = await _context.Matches.Where(f => f.Id == id).FirstOrDefaultAsync();
            
            return _mapper.Map<MatchDto>(match);
        }
        
        public async Task<MatchDto> GetByStadiumAsync(string stadium)
        {
            var match = await _context.Matches.Where(f => f.Stadium.Equals(stadium)).FirstOrDefaultAsync();
            
            return _mapper.Map<MatchDto>(match);
        }

        public async Task<MatchDto> UpdateMatch(int id,UpdateMatchRequest request)
        {

            var match = await _context.Matches.FindAsync(id);

            match.Stadium= request.Stadium ?? match.Stadium;
            match.Score= request.Score ?? match.Score;
            match.Country=request.Country ?? match.Country;

            _context.Matches.Update(match);

            await _context.SaveChangesAsync();

            return _mapper.Map<MatchDto>(match);
        }
    }
}
