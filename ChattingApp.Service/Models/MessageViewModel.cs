using System;
using System.Runtime.Serialization;
using ChattingApp.Repository.Models;

namespace ChattingApp.Service.Models
{
    [DataContract]
    public class MessageViewModel
    {
        [DataMember]
        public Guid id { get; set; }
        [DataMember]
        public ChatViewModel chat { get; set; }
        [DataMember]
        public DateTime createDate { get; set; }
        [DataMember]
        public bool isFavourite { get; set; }
        [DataMember]
        public bool isRead { get; set; }
        [DataMember]
        public string text { get; set; }
        [DataMember]
        public UserDomain user { get; set; }
    }
}
