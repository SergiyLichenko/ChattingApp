using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChattingApp.Repository.Models;

namespace ChattingApp.Repository
{
    public interface IAuthContext
    {
        DbSet<Message> Messages { get; set; }
        DbSet<Chat> Chats { get; set; }
        DbSet<Client> Clients { get; set; }
    }
}
