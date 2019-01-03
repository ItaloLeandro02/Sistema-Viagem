namespace api.Models
{
    public class Veiculo
    {
        public int Id { get; set; }
        public string Fabricante { get; set; }
        public string Modelo { get; set; }
        public int AnoFabricacao { get; set; }
        public int AnoModelo { get; set; }
        public byte Desativado { get; set; }
    }
}