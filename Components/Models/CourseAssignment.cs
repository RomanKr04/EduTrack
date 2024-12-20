using EduTrack.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduTrack.Components.Models
{
    [Table("course_assignments")]
    public class CourseAssignment
    {
        [Key]
        [Column("assignment_id")]
        public int AssignmentId { get; set; }        

        // Внешний ключ для связи с Course
        [Column("course_id")]
        public int CourseId { get; set; }

        // Навигационное свойство для связи с Course
        [ForeignKey("CourseId")]
        public virtual Course Course { get; set; }

        // Внешний ключ для связи с Course
        [Column("student_id")]
        public int StudentId { get; set; }

        /// <summary>
        /// Навигационное свойство для связи с <see cref="Student"/>
        /// </summary>
        [ForeignKey("StudentId")]
        public virtual Student Student { get; set; }
    }
}