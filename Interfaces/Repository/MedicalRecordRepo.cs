using E_Vita.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Vita.Interfaces.Repository
{
    class MedicalRecordRepo : IRepository<Medical_Record>
    {
        private readonly ApplicationDbContext _context;
        public MedicalRecordRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        //tasks
        public async Task AddAsync(Medical_Record entity)
        {
            await _context.Medical_Records.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var MedicalRecord = await _context.Medical_Records.FindAsync(id);
            if (MedicalRecord != null)
            {
                _context.Medical_Records.Remove(MedicalRecord);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Medical_Record>> GetAllAsync()
        {
            return await _context.Medical_Records.ToListAsync();
        }

        public async Task<Medical_Record> GetByIdAsync(int id)
        {
            try { return await _context.Medical_Records.FindAsync(id); }
            catch
            {
                return new Medical_Record(); ;
            }
        }
        public async Task<IEnumerable<Medical_Record>> GetByIdPatientAsync(int id)
        {
            try
            {
                return await _context.Medical_Records
                                     .Where(record => record.Patient_ID == id)
                                     .ToListAsync();
            }
            catch
            {
                return new List<Medical_Record>();
            }
        }

        public async Task UpdateAsync(Medical_Record entity, int id)
        {
            var MedicalRecord = await _context.Medical_Records.FindAsync(id);

            if (MedicalRecord == null)
            {
                // You can throw an exception or return early depending on your requirements
                throw new ArgumentException("Medical Record not found");
            }
            MedicalRecord.Date = entity.Date;
            MedicalRecord.Future_Plan = entity.Future_Plan;
            MedicalRecord.Disease = entity.Disease;
            MedicalRecord.Medication = entity.Medication;
            MedicalRecord.Surgery = entity.Surgery;
            MedicalRecord.Family_History = entity.Family_History;
            MedicalRecord.reason_for_visit = entity.reason_for_visit;
            MedicalRecord.Patient_ID = entity.Patient_ID;
            await _context.SaveChangesAsync();
        }

    }
}
