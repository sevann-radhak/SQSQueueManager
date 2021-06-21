namespace Manager.Model
{
    public class AllMessage<T> where T : class
    {
        public string MessageId { get; set; }

        public string ReceiptHandle { get; set; }

        public T Body { get; set; }
    }
}
