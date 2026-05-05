using System.ComponentModel.DataAnnotations;

namespace OficinaMecanica.DTOs
{
    public class CriarOrcamentoRequest
    {
        [Required(ErrorMessage = "clienteId é obrigatório.")]
        [Range(1, int.MaxValue, ErrorMessage = "clienteId deve ser maior que zero.")]
        public int? ClienteId { get; set; }    

        [Required(ErrorMessage = "veiculoId é obrigatório.")]
        [Range(1, int.MaxValue, ErrorMessage = "veiculoId deve ser maior que zero.")]
        public int? VeiculoId { get; set; }   

        [Required(ErrorMessage = "A lista de itens é obrigatória.")]
        [MinLength(1, ErrorMessage = "O orçamento deve ter pelo menos 1 item.")]
        public List<ItemOrcamentoRequest> Itens { get; set; } = [];
    }

    public class ItemOrcamentoRequest
    {
        [Required(ErrorMessage = "A descrição do item é obrigatória.")]
        public string Descricao { get; set; } = string.Empty;

        [Range(1, int.MaxValue, ErrorMessage = "A quantidade deve ser maior que zero.")]
        public int Quantidade { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "O valor unitário deve ser maior que zero.")]
        public decimal ValorUnitario { get; set; }
    }
}
