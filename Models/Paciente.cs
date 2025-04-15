using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoClinica.Models
{
    [Table("TB_PACIENTES", Schema = "clinica")]
    public class Paciente
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public Guid Id { get; set; } = Guid.NewGuid();

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
