using E_Vita.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Vita.Interfaces.Repository
{
    class DoctorRepo : IRepository<Doctor>
    {
        private readonly ApplicationDbContext _context;
        public DoctorRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        //tasks
        public async Task AddAsync(Doctor entity)
        {
            await _context.Doctors.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var Doctor = await _context.Doctors.FindAsync(id);
            if (Doctor != null)
            {
                _context.Doctors.Remove(Doctor);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Doctor>> GetAllAsync()
        {
            return await _context.Doctors.ToListAsync();
        }

        public async Task<Doctor> GetByIdAsync(int id)
        {
            try { return await _context.Doctors.FindAsync(id);}
            catch
            {
                return new Doctor(); ;
            }
            
        }

        public async Task UpdateAsync(Doctor updatedDoctor, int id)
        {
            var Doctor = await _context.Doctors.FindAsync(id);

            if (Doctor == null)
            {
                // You can throw an exception or return early depending on your requirements
                throw new ArgumentException("Doctor not found");
            }

            // Update the properties of the retrieved Doctor with the new data
            Doctor.Speciality = updatedDoctor.Speciality;
            Doctor.Name = updatedDoctor.Name;
            Doctor.User_Name = updatedDoctor.User_Name;
            Doctor.Pass = updatedDoctor.Pass;

            await _context.SaveChangesAsync();
        }

        Task<IEnumerable<Medical_Record>> IRepository<Doctor>.GetByIdPatientAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
