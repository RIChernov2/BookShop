using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Message
    {
        public long MessageId { get; set; }
        public long UserId { get; set; }
        public DateTime Date { get; set; }
        public MessageType Type { get; set; }
        public string Text { get; set; }

        public Message() { }

        public Message(long userId, DateTime date, MessageType type, string text)
        {
            MessageId = -1;
            UserId = userId;
            Date = date;
            Type = type;
            Text = text;
        }

        public Message(long messageId, long userId, DateTime date, MessageType type, string text) : this(userId, date, type, text)
        {
            MessageId = messageId;
        }
    }
}
