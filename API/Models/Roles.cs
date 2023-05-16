using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API.Models
{
    [Table("tb_m_roles")]
    public class Roles
    {
        [Key, Column("id")]
        public int Id { get; set; }

        [Column("name", TypeName = "varchar(50)")]
        public string Name { get; set; }

        //cardinality
        [JsonIgnore]
        public ICollection<AccountRoles> AccountRoles { get; set; }
    }
}
