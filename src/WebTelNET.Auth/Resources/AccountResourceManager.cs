using System;
using WebTelNET.CommonNET.Resources;

namespace WebTelNET.Auth.Resources
{
    public class AccountResourceManager : WTResourceManager, IAccountResourceManager
    {
        public override string ResolveException(Exception e)
        {
            var message = base.ResolveException(e);
            if (string.IsNullOrEmpty(message))
            {
                var resolver = new AccountResourceResolver();
                return resolver.ResolveException(e);
            }
            return message;
        }
    }
}
