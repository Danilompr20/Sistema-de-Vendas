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
        public async Task<List<Saller>> FindAllAsync()
        {
            return await _context.Saller.ToListAsync();
        }

        public async Task InsertAsync(Saller obj)
        {
            _context.Add(obj);
           await  _context.SaveChangesAsync();
        }
        public async Task<Saller> FindByIdAsync(int id)
        {
            return await _context.Saller.Include(obj => obj.Departament).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task RemoveAsync(int id)
        {
            try
            {
                var x = await _context.Saller.FindAsync(id);
                _context.Saller.Remove(x);
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateException e)
            {
                throw new IntegrityException(e.Message);
            }
        }

        public async Task UpdateAsync(Saller obj)
        {
            bool  hasAny =await _context.Saller.AnyAsync(x => x.Id == obj.Id);
            if (!hasAny)
            {
                throw new NotFoundException("Id não encontrado");
            }
            try
            {

                _context.Update(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }




        }
        }
    }
