using System.ComponentModel.DataAnnotations;

namespace MyTE_Migration.Areas.Gerente.Models
{
    public class HorasTrabalhadas
    {
        [Key]
        public int HorasTrabalhadas_ID { get; set; }
        public int Funcionario_ID { get; set; }
        public int WBS_ID { get; set; }
        public DateTime HorasTrabalhadas_Data { get; set; }
        public int HorasTrabalhadas_QtdeHoras { get; set; }
    }
}
