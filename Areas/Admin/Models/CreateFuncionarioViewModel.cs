using System.Runtime.InteropServices;

namespace MyTE_Migration.Areas.Admin.Models
{
    public class CreateFuncionarioViewModel
    {
        public string? Funcionario_NomeCompleto { get; set; }

        public string? Funcionario_Email { get; set; }

        public DateTime? Funcionario_DataContratacao { get; set; }

        public int Departamento_ID { get; set; }

        public string? Password { get; set; }

        public string? Role { get; set; }
    }
}
