using System.ComponentModel.DataAnnotations;

namespace api.Models
{
    public class CidadeIbge
    {
        [Key]
        public int Id { get; set; }
        public string CodIbge { get; set; }
        public string Cidade { get; set; }
        public string Uf { get; set; }
        public string Distrito { get; set; }
        public string SubDistrito { get; set; }
    }
}