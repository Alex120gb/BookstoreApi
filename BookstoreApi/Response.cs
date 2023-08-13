namespace BookstoreApi
{
    public class Response<T>
    {
        public bool? IsSuccessful { get; set; }
        public string? Message { get; set; }
        public T? Value { get; set; }
    }
}