using E_Vita.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Vita.Interfaces.Repository
{
    class OCRPdfReadRepo : IRepository<OCRPdfRead>
    {
        private readonly ApplicationDbContext _context;

        public OCRPdfReadRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        // Add a new OCRPdfRead entity
        public async Task AddAsync(OCRPdfRead entity)
        {
            await _context.OCRPdfReads.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        // Delete an OCRPdfRead entity by ID
        public async Task DeleteAsync(int id)
        {
            var ocrPdfRead = await _context.OCRPdfReads.FindAsync(id);
            if (ocrPdfRead != null)
            {
                _context.OCRPdfReads.Remove(ocrPdfRead);
                await _context.SaveChangesAsync();
            }
        }

        // Get all OCRPdfRead entities
        public async Task<IEnumerable<OCRPdfRead>> GetAllAsync()
        {
            return await _context.OCRPdfReads.ToListAsync();
        }

        // Get an OCRPdfRead entity by ID
        public async Task<OCRPdfRead> GetByIdAsync(int id)
        {
            try
            {
                return await _context.OCRPdfReads.FindAsync(id);
            }
            catch
            {
                return null;
            }
        }

        // Update an OCRPdfRead entity
        public async Task UpdateAsync(OCRPdfRead updatedEntity, int id)
        {
            var ocrPdfRead = await _context.OCRPdfReads.FindAsync(id);

            if (ocrPdfRead == null)
            {
                throw new ArgumentException("OCRPdfRead entity not found");
            }

            // Update the properties of the retrieved entity
            ocrPdfRead.FileName = updatedEntity.FileName;
            ocrPdfRead.FileData = updatedEntity.FileData;
            ocrPdfRead.ContentType = updatedEntity.ContentType;
            ocrPdfRead.ExtractedText = updatedEntity.ExtractedText;
            ocrPdfRead.UploadedAt = updatedEntity.UploadedAt;

            await _context.SaveChangesAsync();
        }

        // Not implemented as it's not part of OCRPdfRead functionality
        public Task<IEnumerable<Medical_Record>> GetByIdPatientAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
