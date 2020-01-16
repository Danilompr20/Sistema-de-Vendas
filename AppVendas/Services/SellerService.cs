using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppVendas.Models;
using Microsoft.EntityFrameworkCore;

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

        public void Insert(Saller obj)
        {
            _context.Add(obj);
            _context.SaveChanges();
        }
        public Saller FindById(int id)
        {
            return _context.Saller.Include(obj=>obj.Departament).FirstOrDefault(x => x.Id == id);
        }

        public void Remove(int id)
        {
            var x = _context.Saller.Find(id);
            _context.Saller.Remove(x);
            _context.SaveChanges();
        }
    }
}
