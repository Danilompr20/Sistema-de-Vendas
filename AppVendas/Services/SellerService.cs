using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppVendas.Models;
using Microsoft.EntityFrameworkCore;
using AppVendas.Services.Exceptions;

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
            return _context.Saller.Include(obj => obj.Departament).FirstOrDefault(x => x.Id == id);
        }

        public void Remove(int id)
        {
            var x = _context.Saller.Find(id);
            _context.Saller.Remove(x);
            _context.SaveChanges();
        }

        public void Update(Saller obj)
        {
            if (!_context.Saller.Any(x => x.Id == obj.Id))
            {
                throw new NotFoundException("Id não encontrado");
            }
            try
            {

                _context.Update(obj);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }




        }
        }
    }
