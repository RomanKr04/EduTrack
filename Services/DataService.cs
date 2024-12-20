using AngleSharp.Dom;
using EduTrack.Components.Models;
using Microsoft.EntityFrameworkCore;

namespace EduTrack.Services
{
    public interface IDataService<TEntity>
    {
        IQueryable<TEntity> Query();

        Task AddEntity(TEntity item);
        Task RemoveEntity(TEntity item);
        Task UpdateEntity(TEntity item);
    }

    public class DataService<TEntity> : IDataService<TEntity> where TEntity : class
    {
        private readonly EduTrackContext _context;

        public DataService(EduTrackContext context)
        {
            _context = context;
        }

        public async Task AddEntity(TEntity item)
        {
            try
            {
                await _context.AddAsync(item);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log the exception (add logging mechanism)
                throw new InvalidOperationException("Error adding entity.", ex);
            }
        }

        public IQueryable<TEntity> Query()
        {
            return _context.Set<TEntity>();
        }

        public async Task RemoveEntity(TEntity item)
        {
            try
            {
                _context.Remove(item);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log the exception (add logging mechanism)
                throw new InvalidOperationException("Error removing entity.", ex);
            }
        }

        // Optional: Add Update method if needed
        public async Task UpdateEntity(TEntity item)
        {
            try
            {
                _context.Update(item);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log the exception (add logging mechanism)
                throw new InvalidOperationException("Error updating entity.", ex);
            }
        }
    }

    
}
