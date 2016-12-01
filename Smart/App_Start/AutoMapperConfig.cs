using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity.EntityFramework;
using Smart.Data;
using Smart.Models;
using Smart.Models.Entities;
using Smart.ViewModels;

namespace Smart.App_Start
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