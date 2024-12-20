using EduTrack.Components.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EduTrack.Services
{
    public interface IGradeService
    {
     
        Task<Grade> GetGradeBySubmissionIdAsync(int submissionId);

  
        Task SaveGradeAsync(Grade grade);
  
    }

    public class GradeService : IGradeService
    {
        private readonly EduTrackContext _context;

        public GradeService(EduTrackContext context)
        {
            _context = context;
        }
       
        

    
        public async Task<Grade> GetGradeBySubmissionIdAsync(int submissionId)
        {
            // Убедимся, что запрос выполняется асинхронно
            return await _context.Grades.AsNoTracking()
                .FirstOrDefaultAsync(g => g.SubmissionId == submissionId);
        }

        public async Task SaveGradeAsync(Grade grade)
        {
            var existingGrade = await _context.Grades.FirstOrDefaultAsync(g => g.SubmissionId == grade.SubmissionId);
            if (existingGrade != null)
            {
                existingGrade.GradeValue = grade.GradeValue;
                existingGrade.Comments = grade.Comments;
                _context.Grades.Update(existingGrade);  // Обновление существующей записи
            }
            else
            {
                _context.Grades.Add(grade);  // Добавление новой записи
            }

            await _context.SaveChangesAsync();  // Сохранение изменений в базе данных
        }
    }
}
