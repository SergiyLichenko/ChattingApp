using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.ViewModels
{
    public class ChatViewModel
    {
        public System.Guid Id { get; set; }
        public DateTime CreateDate { get; set; }
        public string Title { get; set; }
        public string AuthorName { get; set; }

        public ICollection<UserViewModel> Users { get; set; }
        public string Img{ get; set; }
    }
}
