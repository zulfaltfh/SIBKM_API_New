using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API.Models
{
    [Table("tb_m_accounts")]
    public class Accounts
    {
        [Key, Column("employee_nik", TypeName = "char(5)")]
        public string EmployeeNIK { get; set; }

        [Column("password", TypeName = "varchar(255)")]
        public string Password { get; set; }

        //cardinality
        [JsonIgnore]
        public Employee? Employees { get; set; }
        [JsonIgnore]
        public ICollection<AccountRoles>? AccountRoles { get; set; }

    }
}
