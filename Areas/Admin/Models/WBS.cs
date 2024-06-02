using System.ComponentModel.DataAnnotations;

namespace MyTE_Migration.Areas.Admin.Models
{
    public class WBS
    {
        [Key]
        public int WBS_ID { get; set; }
        public string? WBS_Codigo { get; set; }
        public string? WBS_Descricao { get; set; }
        public bool WBS_Tipo { get; set; }
    }
}
