using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading.Tasks;
using System.Web;
using ChattingApp.Repository.Interfaces;
using ChattingApp.Repository.Models;
using ChattingApp.Service.Models;

namespace ChattingApp.Service
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepository;
        private readonly IMappingService _mappingSerivce;
        private const int UserImageSize = 300;
        public UserService(IUserRepository userRepository, IMappingService mappingSerivce)
        {
            this._userRepository = userRepository;
            _mappingSerivce = mappingSerivce;
        }

        public UserViewModel Get(string id)
        {
            //if (id == null)
                return null;

            //ApplicationUser user = _userRepository.GetByIdAsync(id);
            //UserViewModel userViewModel = _mappingSerivce.Map<ApplicationUser, UserViewModel>(user);
            //return userViewModel;
        }

        public UserViewModel Remove(UserViewModel instance)
        {
            if (instance == null || instance.Id.ToString().Equals(Guid.Empty.ToString()))
                return null;
            ApplicationUser toRemove = _mappingSerivce.Map<UserViewModel, ApplicationUser>(instance);
            ApplicationUser removed = _userRepository.Remove(toRemove);

            return _mappingSerivce.Map<ApplicationUser, UserViewModel>(removed);
        }

        /* public async Task<ApplicationUser> FindAsync(UserLoginInfo userLoginInfo)
         {
             ApplicationUser user = await _userRepository.FindAsync(userLoginInfo);
             return user;
         }*/
        public UserViewModel GetByUserNameAndPassword(string username, string password)
        {
            if (username == null || password == null)
                return null;
            return null;
            //ApplicationUser result = ((IUserRepository)_userRepository).
            //    FindAsync(new username, password);

            //UserViewModel userViewModel = _mappingSerivce.Map<ApplicationUser, UserViewModel>(result);
            //return userViewModel;
        }

        public bool AddImage(string userId, byte[] imageByteArray)
        {
            return _userRepository.AddImage(userId, imageByteArray);
        }

        public UserViewModel GetUserByName(string name)
        {
            if (name == null)
                return null;
            return _mappingSerivce.Map<ApplicationUser, UserViewModel>(_userRepository.GetUserByName(name));
        }

        public UserViewModel Update(UserViewModel instance)
        {
            if (instance == null)
                return instance;
            ApplicationUser user = _mappingSerivce.Map<UserViewModel, ApplicationUser>(instance);

            ApplicationUser result = _userRepository.UpdateAsync(user);
            return _mappingSerivce.Map<ApplicationUser, UserViewModel>(result);
        }

        public UserViewModel Update(UserViewModel oldInstance, UserViewModel newInstance, string oldPassword)
        {
            ApplicationUser oldUser = _mappingSerivce.Map<UserViewModel, ApplicationUser>(oldInstance);
            ApplicationUser newUser = _mappingSerivce.Map<UserViewModel, ApplicationUser>(newInstance);

            ApplicationUser result = null;
            if (!String.IsNullOrEmpty(oldPassword) && newInstance.Password == newInstance.ConfirmPassword)
            {
                /* result = _userRepository.GetByUserNameAndPassword(oldUser.UserName, oldPassword);
                 if (result != null)
                 {
                     _userRepository.ChangePassword(oldInstance.UserName, oldPassword, newInstance.Password);
                 }*/
            }
            if (oldInstance.Email != newInstance.Email)
            {
                result = _userRepository.ChangeEmail(oldInstance.UserName, newInstance.Email);
            }
            if (oldInstance.UserName != newInstance.UserName)
            {
                result = _userRepository.ChangeUsername(oldInstance.UserName, newInstance.UserName);
            }
            if (oldInstance.Img != newInstance.Img)
            {
                if (newInstance.Img == null)
                {
                    newInstance.Img = GetDefaultImage();
                }
                var img = ImageResizer.ProcessImage(newInstance.Img, UserImageSize);
                img = img.Insert(0, "data:image/jpg;base64,");

                // result = _userRepository.ChangeImage(result != null ?
                //     result.UserName : oldInstance.UserName, Img);
            }

            return _mappingSerivce.Map<ApplicationUser, UserViewModel>(result);
        }

        public string GetDefaultImage()
        {
            var path = HttpContext.Current.Server.MapPath("~/Content/Default.png");
            var image = Image.FromFile(path);
            return ImageResizer.ImageToBase64(image, ImageFormat.Png);
        }


        private void SetDefaultImage()
        {
            throw new NotImplementedException();
        }

        public void Add(UserViewModel instance)
        {
            if (instance == null) return;

            instance.Img = GetDefaultImage();
            instance.Img = instance.Img.Insert(0, "data:image/png;base64,");
            ApplicationUser user = _mappingSerivce.Map<UserViewModel, ApplicationUser>(instance);
            _userRepository.Add(user);
        }
        public bool AddUserToChat(string username, string chatId)
        {
            if (String.IsNullOrWhiteSpace(username) || String.IsNullOrWhiteSpace(chatId)) return false;
            return _userRepository.AddUserToChat(username, chatId);
        }
    }
}
