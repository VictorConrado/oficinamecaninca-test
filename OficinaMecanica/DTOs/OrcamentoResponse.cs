namespace OficinaMecanica.DTOs
{
    public class OrcamentoResponse
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public int VeiculoId { get; set; }
        public List<ItemOrcamentoResponse> Itens { get; set; } = [];
        public decimal Total { get; set; }
        public DateTime CriadoEm { get; set; }
    }

    public class ItemOrcamentoResponse
    {
        public string Descricao { get; set; } = string.Empty;
        public int Quantidade { get; set; }
        public decimal ValorUnitario { get; set; }
        public decimal Subtotal { get; set; }
    }
}
