using AppVendas.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppVendas.Services
{
    public class SalesRecordsService
    {
        private readonly AppVendasContext _context;
        public SalesRecordsService(AppVendasContext context)
        {
            _context = context;
        }
        public  async Task <List<SalesRecord>> FindByDateAsync(DateTime?minDate,DateTime?maxDate)
        {
            var result = from obj in _context.SalesRecord select obj;
            if (minDate.HasValue)
            {
                result = result.Where(x => x.Date >= minDate.Value);
            }
            if (maxDate.HasValue)
            {
                result = result.Where(x => x.Date <= maxDate.Value);
            }
            return await result.Include(x => x.Saller)
                .Include(x => x.Saller.Departament)
                .OrderByDescending(x => x.Date).ToListAsync();

        }
        public async Task<List<IGrouping<Departament,SalesRecord>>> FindByDateGroupingAsync(DateTime? minDate, DateTime? maxDate)
        {
            var result = from obj in _context.SalesRecord select obj;
            if (minDate.HasValue)
            {
                result = result.Where(x => x.Date >= minDate.Value);
            }
            if (maxDate.HasValue)
            {
                result = result.Where(x => x.Date <= maxDate.Value);
            }
            return await result.Include(x => x.Saller)
                .Include(x => x.Saller.Departament)
                .OrderByDescending(x => x.Date).GroupBy(x=> x.Saller.Departament)
                .ToListAsync();

        }
    }
}
