using EduTrack.Components.Models;
using Microsoft.EntityFrameworkCore;

public interface ICourseService
{
    Task<IEnumerable<Course>> GetCoursesAsync();
    Task<IEnumerable<Course>> GetCoursesByTeacherAsync(int teacherId);
    Task AddCourseAsync(Course course);
    Task<bool> JoinCourseAsync(int studentId, string joinCode);

}

public class CourseService : ICourseService
{
    private readonly EduTrackContext _context;

    public CourseService(EduTrackContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Course>> GetCoursesAsync()
    {
        return await _context.Courses.ToListAsync();
    }

    public async Task<IEnumerable<Course>> GetCoursesByTeacherAsync(int teacherId)
    {
        return await _context.Courses
            .Where(c => c.StudentId == teacherId)
            .ToListAsync();
    }

    public async Task AddCourseAsync(Course course)
    {
        try
        {
            _context.Courses.Add(course);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            Console.WriteLine($"Error while adding course: {ex.Message}");
            throw new Exception("Failed to add course to the database.");
        }
    }

    // Метод для присоединения студента к курсу по коду
    public async Task<bool> JoinCourseAsync(int studentId, string joinCode)
    {
        var course = await _context.Courses
            .FirstOrDefaultAsync(c => c.JoinCode == joinCode);

        if (course == null)
        {
            return false; // Курс с таким кодом не найден
        }

        var student = await _context.Students
            .FirstOrDefaultAsync(s => s.Id == studentId);

        if (student == null)
        {
            return false; // Студент с таким ID не найден
        }

        // Присоединяем студента к курсу
        course.StudentId = student.Id;
        student.Courses.Add(course); // Добавляем курс в коллекцию курсов студента

        try
        {
            _context.Courses.Update(course);
            _context.Students.Update(student);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error while joining course: {ex.Message}");
            return false;
        }
    }



}
