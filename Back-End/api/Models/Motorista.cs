using System.ComponentModel.DataAnnotations;

namespace api.Models
{
    public class Motorista
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Apelido { get; set; }
        public byte Desativado { get; set; }
    }
}