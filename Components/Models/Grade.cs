using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduTrack.Components.Models
{
    [Table("grades")]
    public class Grade
    {
        [Key]
        [Column("grade_id")]
        public int GradeId { get; set; }

        [Column("submission_id")]
        public int SubmissionId { get; set; }

        [Column("grade")]
        public decimal? GradeValue { get; set; }

        [Column("comments")]
        public string Comments { get; set; }

        [ForeignKey("SubmissionId")]    
        public virtual Submission Submission { get; set; }
    }
}
