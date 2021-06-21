namespace Manager
{
    public class AWSSQSServiceConfiguration
    {
        public AWSSQS AWSSQS { get; set; }
    }

    public class AWSSQS
    {
        public string QueueUrl { get; set; }
    }
}
