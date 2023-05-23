namespace BlazorShopDemo2.Domain.Models
{
    public class SuccessModelDto
    {
        public int StatusCode { get; set; }
        public string SuccessMessage { get; set; }
        public object Data { get; set; }
    }
}