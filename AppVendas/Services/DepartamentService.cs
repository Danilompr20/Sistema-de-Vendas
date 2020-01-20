using AppVendas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AppVendas.Services
{
    public class DepartamentService
    {
        private readonly AppVendasContext _context;
        public DepartamentService(AppVendasContext context)
        {
            _context = context;
        }

        public async Task<List<Departament>> FindAllAsync()
        {
            return await _context.Departament.OrderBy(x => x.Name).ToListAsync();
        }
    }
}
