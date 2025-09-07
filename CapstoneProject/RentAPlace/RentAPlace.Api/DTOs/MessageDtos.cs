namespace RentAPlace.Api.DTOs
{
    public class SendMessageDto
    {
        public int ReceiverId { get; set; }
        public int? PropertyId { get; set; }
        public string MessageText { get; set; } = string.Empty;

    }
    public class MessageReplyDto
    {
        public string ReplyText { get; set; } = string.Empty;
    }
    public class MessageVm
    {
        public int MessageId { get; set; }
        public int SenderId { get; set; }
        public string? SenderName { get; set; }
        public int ReceiverId { get; set; }
        public string? ReceiverName { get; set; }
        public int? PropertyId { get; set; }
        public string MessageText { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public string? ReplyText { get; set; }
        public DateTime? ReplyAt { get; set; }
        public bool IsReadByReceiver { get; set; }
    }
}