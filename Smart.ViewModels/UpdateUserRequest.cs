using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.ViewModels
{
    public class UpdateUserRequest
    {
        public UserViewModel OldUser { get; set; }
        public UserViewModel NewUser { get; set; }
        public string OldPassword { get; set; }
    }
}
