using System.ComponentModel.DataAnnotations;

namespace MyTE_Migration.Areas.Admin.Models
{
    public class Departamento
    {
        [Key]
        public int Departamento_ID { get; set; }
        public string? Departamento_Nome { get; set; }
    }
}
