using System;

namespace Web.Models
{
    public class IncomingMessageViewModel : MessageViewModel
    {
        //Автор
        public Int32 UserFromId { get; set; }
        public String UserFromFirstName { get; set; }
        public String UserFromLastName { get; set; }
    }
}