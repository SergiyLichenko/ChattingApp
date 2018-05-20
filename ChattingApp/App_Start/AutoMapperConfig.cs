using ChattingApp.Repository.Models;
using ChattingApp.Service.Models;
using AutoMapper;

namespace ChattingApp
{
    public class AutoMapperConfig
    {
        public static void Register()
        {
            Mapper.Initialize(config =>
            {
                config.CreateMap<UserViewModel, ApplicationUser>()
                    .ForMember(src => src.PasswordHash, opts => opts.MapFrom(src => src.Password))
                    .ForMember(src => src.UserName, opts => opts.MapFrom(src => src.UserName))
                    .ForMember(src => src.Email, opts => opts.MapFrom(src => src.Email))
                    .ForMember(src => src.Img, opts => opts.MapFrom(src => src.Img))
                    .ForMember(src => src.Id, opts => opts.MapFrom(src => src.Id))
                    .ForAllOtherMembers(x => x.Ignore());
                config.CreateMap<ApplicationUser, UserViewModel>()
                    .ForMember(src => src.UserName, opts => opts.MapFrom(src => src.UserName))
                    .ForMember(src => src.Email, opts => opts.MapFrom(src => src.Email))
                    .ForMember(src => src.Img, opts => opts.MapFrom(src => src.Img))
                    .ForMember(src => src.Id, opts => opts.MapFrom(src => src.Id))
                    .ForMember(src => src.Password, opts => opts.MapFrom(src => src.PasswordHash))
                    .ForAllOtherMembers(x => x.Ignore());

                config.CreateMap<Message, MessageViewModel>().ForAllOtherMembers(x => x.Ignore());
                config.CreateMap<MessageViewModel, Message>().ForAllOtherMembers(x => x.Ignore());
            });

            Mapper.AssertConfigurationIsValid();
        }
    }
}