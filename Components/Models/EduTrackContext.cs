
using EduTrack.Components.Models;
using EduTrack.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System.Diagnostics;

namespace EduTrack.Components.Models
{
    public class EduTrackContext : DbContext
    {
        public EduTrackContext(DbContextOptions<EduTrackContext> options) : base(options) { }
        #region Model configure

        protected override void OnModelCreating(ModelBuilder builder)
        {

        }

        #endregion

        #region DbSets
        public DbSet<Student> Students { get; set; }
  
        public DbSet<Course> Courses { get; set; }
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<Submission> Submissions { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<CourseAssignment> CourseAssignments { get; set;}
        #endregion
    }
}
