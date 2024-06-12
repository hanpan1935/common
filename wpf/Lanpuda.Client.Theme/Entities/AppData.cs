using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Lanpuda.Client.Theme.Entities
{
    public class AppData
    {
        public AppDescription AppDescription { get; set; }

        public UserDescription UserDescription { get; set; }

        public List<MenuItem> Menus { get; set; }

        public AppData()
        {
            AppDescription = new AppDescription();
            UserDescription = new UserDescription();
            Menus = new List<MenuItem>();
        }
    }


    public class AppDescription
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public AppDescription()
        {
            Name = string.Empty;
            Description = string.Empty;
        }
    }

    public class UserDescription
    {
        public string Name { get; set; }
        public string Avatar { get; set; }
        public string Email { get; set; }
        public UserDescription()
        {
            Avatar = string.Empty;
            Email = string.Empty;
            Name = string.Empty;
        }
    }
}
