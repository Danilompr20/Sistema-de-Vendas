using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppVendas.Models;

namespace AppVendas.Services
{
    public class SellerService
    {
        private readonly AppVendasContext _context;
        public SellerService(AppVendasContext context)
        {
            _context = context;
        }

        public List<Saller> FindAll()
        {
            return _context.Saller.ToList();
        }
    }
}
