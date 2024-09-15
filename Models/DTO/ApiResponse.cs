namespace BackendService.Models.DTO
{
    public class ApiResponse<T> where T : class
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public object ErrorDetails { get; set; }
    }
}
