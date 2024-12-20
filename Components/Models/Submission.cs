using EduTrack.Components.Pages;
using EduTrack.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduTrack.Components.Models
{
	[Table("submissions")]
	public class Submission
	{
		[Key]
		[Column("submission_id")]
		public int SubmissionId { get; set; }

		[Column("assignment_id")]
		public int AssignmentId { get; set; }

		[ForeignKey("Student")]
		[Column("student_id")]
		public int StudentId { get; set; }
		public virtual Student Student { get; set; }

        [Column("submission_date", TypeName = "date")]
        public DateTime SubmissionDate { get; set; }

		[Column("status")]
		public string Status { get; set; }


		[Column("file", TypeName = "bytea")]
		public byte[] File { get; set; }

		[Column("file_name")]
		public string FileName { get; set; }

		[ForeignKey("AssignmentId")]
		public virtual Assignment Assignment { get; set; }

		public virtual Grade Grade { get; set; }

	}
}
