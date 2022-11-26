namespace TestLab.Models
{
    public enum MessageType 
    {
        Warning,
        Success,
        Danger,
    }

    public class MessageViewModel
    {
        public MessageType MessageType { get; set; } = MessageType.Warning;
        public string Message { get; set; }

        public bool HasMessage => string.IsNullOrEmpty(Message) is false;
    }
}
