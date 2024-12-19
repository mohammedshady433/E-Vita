using E_Vita.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Vita.Interfaces.Repository
{
    class Reset_PassRepo : IRepository<Reset_Pass_Log>
    {
        private readonly ApplicationDbContext _context;
        public Reset_PassRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Reset_Pass_Log entity)
        {
            await _context.Reset_Pass_Logs.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var resett = await _context.Reset_Pass_Logs.FindAsync(id);
            if (resett != null)
            {
                _context.Reset_Pass_Logs.Remove(resett);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Reset_Pass_Log>> GetAllAsync()
        {
            return await _context.Reset_Pass_Logs.ToListAsync();
        }

        public async Task<Reset_Pass_Log> GetByIdAsync(int id)
        {
            try { return await _context.Reset_Pass_Logs.FindAsync(id); }
            catch
            {
                return new Reset_Pass_Log();
            }
        }

        public async Task UpdateAsync(Reset_Pass_Log entity, int id)
        {
            var resett = await _context.Reset_Pass_Logs.FindAsync(id);

            if (resett == null)
            {
                // You can throw an exception or return early depending on your requirements
                throw new ArgumentException("Admin reset row not found");
            }

            // Update the properties of the retrieved patient with the new data
            resett.New_Pass = entity.New_Pass;
            resett.doc_id = entity.doc_id;
            resett.ID = id;
            resett.Date = DateTime.Now;
            await _context.SaveChangesAsync();
        }
    }
}
