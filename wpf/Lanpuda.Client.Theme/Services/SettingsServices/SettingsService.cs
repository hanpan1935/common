using Lanpuda.Client.Theme.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Volo.Abp.DependencyInjection;

namespace Lanpuda.Client.Theme.Services.SettingsServices
{
    public class SettingsService : ISettingsService
    {
        private string _appName { get; set; }
        private string _appDescription { get; set; }
        private Guid _userId { get; set; }
        private string _userName { get; set; }
        private string _userEmail { get; set; }
        private BitmapImage _userAvatar { get; set; }

        private string _surname { get; set; }
        private string _name { get; set; }

        private string _phoneNumber { get; set; }

        public SettingsService()
        {
            _appName = string.Empty;
            _appDescription = string.Empty;
            _userId = Guid.Empty;
            _userName = string.Empty;
            _userEmail = string.Empty;
            var bitmapImage = new BitmapImage(new Uri("pack://application:,,,/Lanpuda.Client.Theme;component/Assets/Images/DefaultAvatar.png"));
            _userAvatar = bitmapImage;
            _surname = string.Empty;
            _name = string.Empty;
            _phoneNumber = string.Empty;
        }

        public void SetAppName(string appName)
        {
            _appName = appName;
        }

        public string GetAppName()
        {
            return _appName;
        }


        public void SetAppDescription(string description)
        {
            _appDescription = description;
        }

        public string GetAppDescription()
        {
            return _appDescription;
        }

        public void SetUserId(Guid? id)
        {
            if (id == null)
            {
                _userId = Guid.Empty;
            }
            else
            {
                _userId = (Guid)id;
            }
        }

        public Guid GetUserId()
        {
            return _userId;
        }



        public void SetUserName(string userName)
        {
            _userName = userName;
        }

        public string GetUserName()
        {
            return _userName;
        }


        public void SetUserEmail(string email)
        {
            _userEmail = email;
        }

        public string GetUserEmail()
        {
            return _userEmail;
        }


        public void SetUserAvatar(string avatarUrl)
        {
            _userAvatar = new BitmapImage(new Uri(avatarUrl));
        }


        public BitmapImage GetUserAvatar()
        {
            return _userAvatar;
        }


        public string GetSurname()
        {
            return _surname;
        }

        public void SetSurname(string surname)
        {
            _surname = surname;
        }


        public string GetName()
        {
            return _name;
        }

        public void SetName(string name)
        {
            _name = name;
        }



        public string GetPhoneNumber()
        {
            return _phoneNumber;
        }

        public void SetPhoneNumber(string phoneNumber)
        {
            _phoneNumber = phoneNumber;
        }
    }
}
