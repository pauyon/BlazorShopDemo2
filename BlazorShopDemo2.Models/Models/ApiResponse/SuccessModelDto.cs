namespace BlazorShopDemo2.Domain.Models.ApiResponse
{
    public class SuccessModelDto
    {
        public int StatusCode { get; set; }
        public string SuccessMessage { get; set; }
        public object Data { get; set; }
    }
}