using mohan_CapstoneProject_SDA.LMS.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mohan_CapstoneProject_SDA.LMS.Data.Services
{
    public class MedicinesService : IMedicinesService
    {
        private readonly AppDbContext _context;

        public MedicinesService(AppDbContext context)
        {
            _context = context;
        }
        public async Task Add(Medicine medicine)
        {
          await  _context.Medicines.AddAsync(medicine);
          await  _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var result = await _context.Medicines.FirstOrDefaultAsync(n => n.ID == id);
             _context.Medicines.Remove(result);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Medicine>> GetAll()
        {
            var result = await _context.Medicines.ToListAsync();
            return result;
        }

        public async Task<Medicine> GetByID(int id)
        {
            var result = await _context.Medicines.FirstOrDefaultAsync(n => n.ID == id);
            return result;
        }

        public async Task<Medicine> Update(int id, Medicine newMedicine)
        {
            _context.Update(newMedicine);
            await _context.SaveChangesAsync();
            return newMedicine;
        }
    }
}
