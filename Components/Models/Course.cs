using EduTrack.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduTrack.Components.Models
{
    [Table("courses")]
    public class Course
    {
        [Key]
        [Column("course_id")]
        public int CourseId { get; set; }

        [Required(ErrorMessage = "Course name is required.")]
        [MaxLength(100, ErrorMessage = "Course name cannot exceed 100 characters.")]
        [Column("course_name")]
        public string CourseName { get; set; }

        [MaxLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        [Column("description")]
        public string Description { get; set; }

        // Внешний ключ и связь со Student
        [ForeignKey("fk_course_student")]
        [Column("student_id")]
        public int StudentId { get; set; }
        public virtual Student Student { get; set; }

        [Column("join_code")]
        public string JoinCode { get; set; } // исправлено

        // Связь с Assignments
        public virtual ICollection<Assignment> Assignments { get; set; }
        

        public Course()
        {
            Assignments = new HashSet<Assignment>();
            
        }
    }
}
