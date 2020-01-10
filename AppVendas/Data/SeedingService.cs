using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppVendas.Models;
using AppVendas.Models.Enums;

namespace AppVendas.Data
{
    public class SeedingService
    {
        private AppVendasContext _context;
        
        public SeedingService(AppVendasContext context)
        {
            _context = context;
        }
        public void Seed()
        {
            if (_context.Departament.Any() ||
                _context.SalesRecord.Any() ||
                _context.Saller.Any())
            {
                return;
            }
            Departament d1 = new Departament(new int(),"Eletronics");
            Departament d2 = new Departament(new int(), "Ferramentas");

            Saller s1 = new Saller(new int(), "Danilo", "Danilo@gmail.com", new DateTime(1999, 3, 12), 1234.00, d1);
            Saller s2 = new Saller(new int(), "Tais", "Tais@gmail.com", new DateTime(1999, 3, 12), 1234.00, d2);

            SalesRecord w1 = new SalesRecord(new int(), new DateTime(23, 11, 12),1200.00, SalesStatus.Pending, s1);

            SalesRecord w2= new SalesRecord(new int(), new DateTime(23, 11, 12),1100.00, SalesStatus.Billed, s2);

            _context.Departament.AddRange(d1,d2);
            _context.Saller.AddRange(s1,s2);
            _context.SalesRecord.AddRange(w1,w2);
            _context.SaveChanges();
        }
        

    }
}
