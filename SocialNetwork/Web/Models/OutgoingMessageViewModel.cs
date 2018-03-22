using System;

namespace Web.Models
{
    public class OutgoingMessageViewModel : MessageViewModel
    {
        //Получатель
        public Int32 UserToId { get; set; }
        public String UserToFirstName { get; set; }
        public String UserToLastName { get; set; }
    }
}