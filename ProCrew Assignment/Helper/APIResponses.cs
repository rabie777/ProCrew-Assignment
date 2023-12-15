namespace ProCrew_Assignment.Helper
{
    public class APIResponses<T>
    {
        public int Code { get; set; }
        public string status { get; set; }
        public string Message { get; set; }
        public T Data {  get; set; }
    }
}
