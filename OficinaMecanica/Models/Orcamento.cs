namespace OficinaMecanica.Models
{
    public class Orcamento
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public int VeiculoId { get; set; }
        public List<ItemOrcamento> Itens { get; set; } = [];
        public decimal Total { get; set; }
        public DateTime CriadoEm { get; set; }
    }

    public class ItemOrcamento
    {
        public string Descricao { get; set; } = string.Empty;
        public int Quantidade { get; set; }
        public decimal ValorUnitario { get; set; }
        public decimal Subtotal => Quantidade * ValorUnitario;
    }
}
