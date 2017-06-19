using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BCoreMvc.Models.ViewModels.Blog
{
    public class UserViewModel
    {
        public Guid UserId { set; get; }
        public string UserName { set; get; }
        public string Email { set; get; }
    }
}
