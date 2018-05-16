namespace Smart
{
    public class AutoMapperConfig
    {
        public static void Register()
        {
            AutoMapper.Mapper.Initialize(config =>
            {
                config.CreateMap<UserViewModel, ApplicationUser>().
                ForMember("PasswordHash", opts => opts.MapFrom(src => src.password));
                config.CreateMap<ApplicationUser, UserViewModel>();

                config.CreateMap<ChatViewModel, Chat>();
                config.CreateMap<Chat, ChatViewModel>();



                config.CreateMap<Message, MessageViewModel>();
                config.CreateMap<MessageViewModel, Message>();
            });
        }
    }
}