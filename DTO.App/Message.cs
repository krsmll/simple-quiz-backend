using System.Collections.Generic;

namespace DTO.App
{
    public class Message
    {
        public IList<string> Messages { get; set; } = new List<string>();
        
        public Message()
        {
            
        }

        public Message(params string[] messages)
        {
            Messages = messages;
        } 
    }
}