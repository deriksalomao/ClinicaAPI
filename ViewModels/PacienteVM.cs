using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProjetoClinica.ViewModels
{
    public class PacienteVM
    {
        [Required]
        [Column("nome")]
        [Length(20, 140)]
        public string? Nome { get; set; }

        [Required]
        [Column("idade")]
        public int Idade { get; set; }

        [Required]
        [Column("cpf")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "CPF deve ter 11 dígitos.")]
        public string? Cpf { get; set; }
    }
}
