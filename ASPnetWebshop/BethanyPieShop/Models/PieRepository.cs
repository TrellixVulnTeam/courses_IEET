using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace BethanyPieShop.Models
{
    public class PieRepository: IPieRepository
    {
        private readonly AppDbContext _appDbContext;

        public PieRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<Pie> AllPies
        {
            get { return _appDbContext.Pies.Include(c => c.Category); }
        }
        public IEnumerable<Pie> PiesOfTheWeek 
        {
            get { return _appDbContext.Pies.Include(c => c.Category).Where(p => p.IsPieOfTheWeek); }
        }
        public Pie GetPieById(int pieId)
        {
            return _appDbContext.Pies.FirstOrDefault(p => p.PieId == pieId);
        }
    }
}