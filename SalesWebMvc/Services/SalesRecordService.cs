using SalesWebMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using SalesWebMvc.Services.Exceptions;

namespace SalesWebMvc.Services
{
    public class SalesRecordService
    {
        private readonly SalesWebMvcContext _context;

        public SalesRecordService(SalesWebMvcContext context)
        {
            _context = context;
        }

        public async Task<List<SalesRecord>> FindAllAsync()
        {
            return await _context.SalesRecords.Include(x => x.Seller).ToListAsync();
        }

        public async Task<SalesRecord> FindByIdAsync(int id)
        {
            return await _context.SalesRecords.Include(s => s.Seller).FirstOrDefaultAsync(salesRecord => salesRecord.Id == id);
        }

        public async Task InsertAsync(SalesRecord salesRecord)
        {
             _context.Add(salesRecord);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(int id)
        {
            try
            {
                var salesRecord = await _context.SalesRecords.FindAsync(id);
                _context.SalesRecords.Remove(salesRecord);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new IntegrityException("Can't delete de Sale Record");
            }
        }

        public async Task UpdateAsync(SalesRecord salesRecord)
        {
            bool hasAny = await _context.SalesRecords.AnyAsync(x => x.Id == salesRecord.Id);
            if (!hasAny)
                throw new NotFoundException("Id not found");

            try
            {
                _context.Update(salesRecord);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }

        public async Task<List<SalesRecord>> FindByDateAsync(DateTime? minDate, DateTime? maxDate)
        {
            var result = from obj in _context.SalesRecords select obj;
            if (minDate.HasValue)
                result = result.Where(x => x.Date >= minDate.Value);

            if (maxDate.HasValue)
                result = result.Where(x => x.Date <= maxDate.Value);

            return await result
                .Include(x => x.Seller)
                .Include(x => x.Seller.Department)
                .OrderByDescending(x => x.Date)
                .ToListAsync();
        }

        public async Task<List<IGrouping<Department,SalesRecord>>> FindByDateGroupingAsync(DateTime? minDate, DateTime? maxDate)
        {
            var result = from obj in _context.SalesRecords select obj;
            if (minDate.HasValue)
                result = result.Where(x => x.Date >= minDate.Value);

            if (maxDate.HasValue)
                result = result.Where(x => x.Date <= maxDate.Value);

            return await result
                .Include(x => x.Seller)
                .Include(x => x.Seller.Department)
                .OrderByDescending(x => x.Date)
                .GroupBy(x => x.Seller.Department)
                .ToListAsync();
        }
    }
}
