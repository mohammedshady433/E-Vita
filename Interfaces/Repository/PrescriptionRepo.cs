using E_Vita.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Vita.Interfaces.Repository
{
    class PrescriptionRepo : IRepository<Prescription>
    {
        private readonly ApplicationDbContext _context;
        public PrescriptionRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        //tasks
        public async Task AddAsync(Prescription entity)
        {
            await _context.Prescriptions.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var presc = await _context.Prescriptions.FindAsync(id);
            if (presc != null)
            {
                _context.Prescriptions.Remove(presc);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Prescription>> GetAllAsync()
        {
            return await _context.Prescriptions.ToListAsync();
        }

        public async Task<Prescription> GetByIdAsync(int id)
        {
            try { return await _context.Prescriptions.FindAsync(id);}
            catch
            {
                return new Prescription(); ;
            }
            
        }

        public async Task UpdateAsync(Prescription updatedPrescription, int id)
        {
            var presc = await _context.Prescriptions.FindAsync(id);

            if (presc == null)
            {
                // You can throw an exception or return early depending on your requirements
                throw new ArgumentException("Prescription not found");
            }

            // Update the properties of the retrieved patient with the new data
            presc.Date=updatedPrescription.Date;
            presc.Dosage=updatedPrescription.Dosage;

            await _context.SaveChangesAsync();
        }
    }
}
