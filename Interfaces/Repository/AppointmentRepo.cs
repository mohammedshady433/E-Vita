using E_Vita.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Vita.Interfaces.Repository
{
    class AppointmentRepo : IRepository<Appointment>
    {
        private readonly ApplicationDbContext _context;
        public AppointmentRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        //tasks
        public async Task AddAsync(Appointment entity)
        {
            await _context.Appointments_DB.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var Appoint = await _context.Appointments_DB.FindAsync(id);
            if (Appoint != null)
            {
                _context.Appointments_DB.Remove(Appoint);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Appointment>> GetAllAsync()
        {
            return await _context.Appointments_DB.ToListAsync();
        }

        public async Task<Appointment> GetByIdAsync(int id)
        {
            try { return await _context.Appointments_DB.FindAsync(id); }
            catch
            {
                return new Appointment(); ;
            }
        }

        public async Task UpdateAsync(Appointment entity, int id)
        {
            var Appoint = await _context.Appointments_DB.FindAsync(id);

            if (Appoint == null)
            {
                // You can throw an exception or return early depending on your requirements
                throw new ArgumentException("Appointment not found");
            }

            // Update the properties of the retrieved patient with the new data
            Appoint.Status = entity.Status;
            await _context.SaveChangesAsync();
        }



    }
}
