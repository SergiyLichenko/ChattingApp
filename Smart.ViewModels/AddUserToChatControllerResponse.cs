using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.ViewModels
{
public    class AddUserToChatControllerResponse
    {
        public ChatViewModel Chat { get; set; }
        public UserViewModel User { get; set; }
    }
}
