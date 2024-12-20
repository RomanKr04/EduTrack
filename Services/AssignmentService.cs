using EduTrack.Components.Models;

using Microsoft.EntityFrameworkCore;

public interface IAssignmentService
{
    Task<IEnumerable<Assignment>> GetAssignmentsAsync();
    Task<IEnumerable<Assignment>> GetAssignmentsByTeacherAsync(int teacherId);
    Task<Assignment> GetAssignmentByIdAsync(int assignmentId);

    Task AddAssignmentAsync(Assignment assignment);
}


namespace EduTrack.Services
{
    public class AssignmentService : IAssignmentService
    {
        private readonly EduTrackContext _context;

        public AssignmentService(EduTrackContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Assignment>> GetAssignmentsAsync()
        {
            return await _context.Assignments.Include(a => a.Course).ToListAsync();
        }
        public async Task<IEnumerable<Assignment>> GetAssignmentsByTeacherAsync(int teacherId)
        {
            // Фильтруем курсы преподавателя
            var courses = await _context.Courses
                .Where(c => c.StudentId == teacherId)
                .Select(c => c.CourseId)
                .ToListAsync();

            // Возвращаем задания для этих курсов
            return await _context.Assignments
                .Include(a => a.Course)
                .Where(a => courses.Contains(a.CourseId))
                .ToListAsync();
        }
        public async Task<Assignment> GetAssignmentByIdAsync(int assignmentId)
        {
            if (_context == null)
            {
                throw new InvalidOperationException("Контекст базы данных не инициализирован.");
            }

            return await _context.Assignments
                                 .FirstOrDefaultAsync(a => a.AssignmentId == assignmentId);
        }


        public async Task AddAssignmentAsync(Assignment assignment)
        {
            _context.Assignments.Add(assignment);
            await _context.SaveChangesAsync();
        }
    }

}
