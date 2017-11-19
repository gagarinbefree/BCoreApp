using System.Collections.Generic;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication;

namespace BCoreIdentity.Models.ManageViewModels
{
    public class ManageLoginsViewModel
    {
        public IList<UserLoginInfo> CurrentLogins { get; set; }

        //public IList<AuthenticationDescription> OtherLogins { get; set; }

        public IList<AuthenticationScheme> OtherLogins { get; set; }
    }
}
