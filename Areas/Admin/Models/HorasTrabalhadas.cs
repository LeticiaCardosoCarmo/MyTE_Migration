using System.ComponentModel.DataAnnotations;

namespace MyTE_Migration.Areas.Admin.Models
{
    public class HorasTrabalhadas
    {
        [Key]
        public int HorasTrabalhadas_ID { get; set; }
        public int Funcionario_ID { get; set; }
        public int WBS_ID { get; set; }
        public DateTime HorasTabalhadas_Data { get; set; }
        public int HorasTrabalhadas_QtdeHoras { get; set; }
        public Funcionario? funcionario { get; set; }
    }
}
