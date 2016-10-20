using System;
using System.Collections.Generic;
using WebTelNET.CommonNET.Resources;

namespace WebTelNET.Auth.Resources
{
    public class AccountResourceManager : ResourceManager
    {
        public override IDictionary<string, string> ResolveExeption(Exception e)
        {
            return base.ResolveExeption(e);
        }
    }
}
