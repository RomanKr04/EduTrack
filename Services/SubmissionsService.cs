using EduTrack.Components.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public interface ISubmissionService
{
    Task AddSubmissionAsync(Submission submission);
    Task<IEnumerable<Assignment>> GetAssignmentsByStudentAsync(int studentId);
    Task<IEnumerable<Submission>> GetSubmissionsByStudentAsync(int studentId);
    Task<IEnumerable<Submission>> GetSubmissionsByAssignmentAsync(int assignmentId);
    Task<IEnumerable<Submission>> GetSubmissionsByStudentIdAsync(int studentId);
    Task SaveGradeAsync(Grade grade);
    Task UpdateSubmissionStatusAsync(Submission submission);
    Task<Submission> GetSubmissionByIdAsync(int submissionId);
  
}

namespace EduTrack.Services
{
    public class SubmissionService : ISubmissionService
    {
        private readonly EduTrackContext _context;

        public SubmissionService(EduTrackContext context)
        {
            _context = context;
        }

        public async Task AddSubmissionAsync(Submission submission)
        {
            _context.Submissions.Add(submission);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Assignment>> GetAssignmentsByStudentAsync(int studentId)
        {
            var assignments = await _context.Submissions
                .Where(s => s.StudentId == studentId)
                .Include(s => s.Assignment)
                .Select(s => s.Assignment)
                .Distinct()
                .ToListAsync();

            return assignments;
        }

        public async Task<IEnumerable<Submission>> GetSubmissionsByStudentIdAsync(int studentId)
        {
            return await _context.Submissions
                .Where(s => s.StudentId == studentId)
                .Include(s => s.Assignment)
                .Include(s => s.Grade) // Включаем оценки в результат
                .ToListAsync();
        }
        public async Task<IEnumerable<Submission>> GetSubmissionsByStudentAsync(int studentId)
        {
            var submissions = await _context.Submissions
                .Where(s => s.StudentId == studentId)
                .Include(s => s.Assignment)
                .ToListAsync();

            return submissions;
        }
      





        public async Task<IEnumerable<Submission>> GetSubmissionsByAssignmentAsync(int assignmentId)
        {
            var submissions = await _context.Submissions
                .Where(s => s.AssignmentId == assignmentId)
                .Include(s => s.Student)
                .Include(s=>s.Grade)
                .ToListAsync();

           
            return submissions;
        }

        public async Task SaveGradeAsync(Grade grade)
        {
            var existingGrade = await _context.Grades
                .FirstOrDefaultAsync(g => g.SubmissionId == grade.SubmissionId);

            if (existingGrade != null)
            {
                existingGrade.GradeValue = grade.GradeValue;
                existingGrade.Comments = grade.Comments;
            }
            else
            {
                await _context.Grades.AddAsync(grade);
            }

            await _context.SaveChangesAsync();
        }

		public async Task UpdateSubmissionStatusAsync(Submission submission)
		{
			_context.Entry(submission).Property(s => s.Status).IsModified = true;
			await _context.SaveChangesAsync();
		}


		public async Task<Submission> GetSubmissionByIdAsync(int submissionId)
        {
            var submission = await _context.Submissions
                                            .Include(s => s.Grade)
                                            .FirstOrDefaultAsync(s => s.SubmissionId == submissionId);

            if (submission == null)
            {
                throw new KeyNotFoundException($"Submission with ID {submissionId} not found.");
            }

            return submission;
        }
    }
}
