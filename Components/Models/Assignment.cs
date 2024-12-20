using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduTrack.Components.Models
{
    [Table("assignments")]
    public class Assignment
    {
        [Key]
        [Column("assignment_id")]
        public int AssignmentId { get; set; }

        [Column("title")]
        [Required]
        [MaxLength(200)]
        public string AssignmentName { get; set; }

        [Column("description")]
        public string Description { get; set; }

        // Внешний ключ для связи с Course
        [Column("course_id")]
        public int CourseId { get; set; }

        [Column("due_date")]
        [Required]
        public DateTime DueDate { get; set; } // Теперь это DateTime

		[Column("file", TypeName = "bytea")]
		public byte[] File { get; set; }

		[Column("file_name")]
		public string FileName { get; set; }


		// Навигационное свойство для связи с Course
		[ForeignKey("CourseId")]
        public virtual Course Course { get; set; }
    }
}
