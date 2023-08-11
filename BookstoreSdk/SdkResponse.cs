namespace BookstoreSdk
{
    public class SdkResponse<T>
    {
        public bool? IsSuccessful { get; set; }
        public string? Message { get; set; }
        public T? Value { get; set; }
    }
}
