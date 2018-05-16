namespace Smart.Models
{
    public class UpdateUserRequest
    {
        public UserViewModel OldUser { get; set; }
        public UserViewModel NewUser { get; set; }
        public string OldPassword { get; set; }
    }
}
