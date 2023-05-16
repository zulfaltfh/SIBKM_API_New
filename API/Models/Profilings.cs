using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API.Models
{
    [Table("tb_tr_profilings")]
    public class Profilings
    {
        [Key, Column("employee_nik", TypeName = "char(5)")]
        public string EmployeeNIK { get; set; }

        [Column("education_id")]
        public int EducationId { get; set; }

        //cardinality
        [JsonIgnore]
        public Educations? Educations { get; set; }
        [JsonIgnore]
        public Employee? Employees { get; set; }
    }
}
