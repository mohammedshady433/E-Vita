using E_Vita.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Vita.Interfaces.Repository
{
    class NurseRepo : IRepository<Nurse>
    {
        private readonly ApplicationDbContext _context;
        public NurseRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        //tasks
        public async Task AddAsync(Nurse entity)
        {
            await _context.Nurses.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var nur = await _context.Nurses.FindAsync(id);
            if (nur != null)
            {
                _context.Nurses.Remove(nur);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Nurse>> GetAllAsync()
        {
            return await _context.Nurses.ToListAsync();
        }

        public async Task<Nurse> GetByIdAsync(int id)
        {
            try { return await _context.Nurses.FindAsync(id);}
            catch
            {
                return new Nurse(); ;
            }
            
        }

        public async Task UpdateAsync(Nurse updatedNurse, int id)
        {
            var Nur = await _context.Nurses.FindAsync(id);

            if (Nur == null)
            {
                // You can throw an exception or return early depending on your requirements
                throw new ArgumentException("Nurse not found");
            }

            // Update the properties of the retrieved patient with the new data
            Nur.password= updatedNurse.password;
            Nur.user_name= updatedNurse.user_name;
            await _context.SaveChangesAsync();
        }

        Task<IEnumerable<Medical_Record>> IRepository<Nurse>.GetByIdPatientAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
