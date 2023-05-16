using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API.Models
{
    [Table("tb_m_education")]
    public class Educations
    {
        [Key, Column("id")]
        public int Id { get; set; }

        [Column("major", TypeName = "varchar(100)")]
        public string Major { get; set; }

        [Column("degree", TypeName = "varchar(10)")]
        public string Degree { get; set; }

        [Column("gpa", TypeName = "varchar(5)")]
        public string GPA { get; set; }

        [Column("university_id")]
        public int UniversityId { get; set; }

        //Cardinality
        [JsonIgnore]
        public Universities? Universities { get; set; }
        [JsonIgnore]
        public Profilings Profilings { get; set; }
    }
}
