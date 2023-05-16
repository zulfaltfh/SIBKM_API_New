using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API.Models
{
    [Table("tb_m_employees")]
    public class Employee
    {
        [Key, Column(name: "nik", TypeName = "char(5)")]
        public string NIK { get; set; }

        [Column(name: "first_name", TypeName = "varchar(50)")]
        public string FirstName { get; set; }

        [Column(name: "last_name", TypeName = "varchar(50)")]
        public string? LastName { get; set; }

        [Column(name: "birthdate", TypeName = "datetime")]
        public DateTime BirthDate { get; set; }

        [Column(name: "gender")]
        public Gender Gender { get; set; }

        [Column(name: "hiringdate", TypeName = "datetime")]
        public DateTime HiringDate { get; set; }

        [Column(name: "email", TypeName = "varchar(50)")]
        public string Email { get; set; }

        [Column(name: "phone_number", TypeName = "varchar(20)")]
        public string PhoneNumber { get; set; }


        //cardinality
        [JsonIgnore]
        public Profilings? Profilings { get; set; }
        [JsonIgnore]
        public Accounts? Accounts { get; set; }
    }

    public enum Gender
    {
        Male,
        Female
    }
}
