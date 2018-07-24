using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RbxProxiless
{
    public static class Roblox
    {
        public static Task<string> ProxilessLogin(string Username, string Password)
        {
            return Core.CoreLogin.ProxilessLogin(Username, Password);
        }

    }
}
