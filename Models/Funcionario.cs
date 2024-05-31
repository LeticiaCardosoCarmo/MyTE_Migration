using System.ComponentModel.DataAnnotations;

namespace MyTE_Migration.Models
{
    public class Funcionario
    {
        [Key]
        public int Funcionario_ID { get; set; }
        public string? Funcionario_NomeCompleto { get; set; }

        [RegularExpression("[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9])+\\.+[a-zA-Z]{2,6}$")]
        public string? Funcionario_Email { get; set; }
        public DateTime? Funcionario_DataContratacao { get; set; }
        public int Departamento_ID { get; set; }
    }
}
