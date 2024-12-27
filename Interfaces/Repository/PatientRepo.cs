using E_Vita.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Vita.Interfaces.Repository
{
    class PatientRepo : IRepository<Patient>
    {
        private readonly ApplicationDbContext _context;
        public PatientRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        //tasks
        public async Task AddAsync(Patient entity)
        {
            await _context.patient_Datas.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var patient = await _context.patient_Datas.FindAsync(id);
            if (patient != null)
            {
                _context.patient_Datas.Remove(patient);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Patient>> GetAllAsync()
        {
            return await _context.patient_Datas.ToListAsync();
        }

        public async Task<Patient> GetByIdAsync(int id)
        {
            try { return await _context.patient_Datas.FindAsync(id);}
            catch
            {
                return new Patient(); ;
            }
            
        }

        public async Task UpdateAsync(Patient updatedPatient, int id)
        {
            var patient = await _context.patient_Datas.FindAsync(id);

            if (patient == null)
            {
                // You can throw an exception or return early depending on your requirements
                throw new ArgumentException("Patient not found");
            }

            // Update the properties of the retrieved patient with the new data
            patient.name = updatedPatient.name;
            patient.Nationality = updatedPatient.Nationality;
            patient.contact = updatedPatient.contact;
            patient.Birth_Date = updatedPatient.Birth_Date;

            await _context.SaveChangesAsync();
        }

        Task<IEnumerable<Medical_Record>> IRepository<Patient>.GetByIdPatientAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
