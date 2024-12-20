using EduTrack.Components.Enums;
using EduTrack.Components.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduTrack.Models
{
    [Table("students")]
    public class Student
    {
        [Key]
        [Column("student_id")]
        public int Id { get; set; }

        [Column("username")]
        public string Username { get; set; }

        [Column("password")]
        public string Password { get; set; }

        [Column("first_name")]
        public string first_name { get; set; }

        [Column("last_name")]
        public string last_name { get; set; } // Изменено на string

        [Column("email")]
        public string Email { get; set; }

        [Column ("role")]
        public RoleTypes role { get; set; }

        [NotMapped]
        public bool IsAdmin { get; set; }
        // Навигационное свойство для курсов
        public virtual ICollection<Course> Courses { get; set; }

        public Student()
        {
            Courses = new HashSet<Course>(); // Инициализация коллекции
        }
    }
}
