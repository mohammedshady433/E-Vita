using E_Vita.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Vita.Interfaces.Repository
{
    class LabTestRepo : IRepository<LabTest>
    {
        private readonly ApplicationDbContext _context;
        public LabTestRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(LabTest entity)
        {
            await _context.LabTests.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var labvar = await _context.LabTests.FindAsync(id);
            if (labvar != null)
            {
                _context.LabTests.Remove(labvar);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<LabTest>> GetAllAsync()
        {
            return await _context.LabTests.ToListAsync();
        }

        public async Task<LabTest> GetByIdAsync(int id)
        {
            try { return await _context.LabTests.FindAsync(id); }
            catch
            {
                return new LabTest();
            }
        }

        public Task<IEnumerable<Medical_Record>> GetByIdPatientAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(LabTest entity, int id)
        {
            var labvar = await _context.LabTests.FindAsync(id);

            if (labvar == null)
            {
                // You can throw an exception or return early depending on your requirements
                throw new ArgumentException("image not found");
            }

            // Update the properties of the retrieved patient with the new data
            labvar.UploadedAt = entity.UploadedAt;
            labvar.FileName = entity.FileName;
            labvar.ImageData = entity.ImageData;
            labvar.ContentType = entity.ContentType;
            labvar.PatientId = entity.PatientId;

            await _context.SaveChangesAsync();
        }
    }
}
