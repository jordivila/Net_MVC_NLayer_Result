﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;

namespace VsixMvcAppResult.Models.Authentication
{
    public interface IAuthenticationBL : IAuthenticationProxy
    {
        FormsAuthenticationTicket SetTicket(string userName);
    }
}
