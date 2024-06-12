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
    public interface ISettingsService : ISingletonDependency
    {
        public void SetAppName(string appName);

        public string GetAppName();

        public void SetAppDescription(string description);

        public string GetAppDescription();


        public void SetUserId(Guid? id);

        public Guid GetUserId();

        public void SetUserName(string userName);

        public string GetUserName();


        public void SetSurname(string surname);

        public string GetSurname();



        public void SetName(string name);

        public string GetName();



        public void SetUserEmail(string email);

        public string GetUserEmail();


        public string GetPhoneNumber();

        public void SetPhoneNumber(string phoneNumber);


        public void SetUserAvatar(string avatarUrl);

        public BitmapImage GetUserAvatar();
    }
}
