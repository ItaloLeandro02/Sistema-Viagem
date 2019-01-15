namespace api.Controllers
{
    public class RetornoView <T> where T : class
    {
        public bool sucesso { get; set; }
        public T data { get; set; }
        public string erro { get; set; }
    }
}