using System.ComponentModel.DataAnnotations;

namespace ControleFacil.Api.Domain.Models
{
    public class Apagar
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public long IdUsuario { get; set; }

        public Usuario Usuario { get; set; }

        [Required]
        public long IdNaturezaDeLancamento { get; set; }

        public NaturezaDeLancamento NaturezaDeLancamento { get; set; }

        [Required(ErrorMessage = "O campo de Descrição é obrigatório")]
        public string Descricao { get; set; } = string.Empty;

        [Required(ErrorMessage = "O campo ValorOriginal é obrigatório")]
        public long ValorOriginal { get; set; }

        [Required(ErrorMessage = "O campo ValorPago é obrigatório")]
        public long ValorPago { get; set; }

        public string? Observacao { get; set; } = string.Empty;

        [Required]
        public DateTime DataCadastro { get; set; }

        [Required(ErrorMessage = "O campo DataVencimento é obrigatório")]
        public DateTime DataVencimento { get; set; }

        public DateTime? DataInativacao { get; set; }
        public DateTime? DataReferencia { get; set; }
        public DateTime? DataPagamento { get; set; }
    }
}