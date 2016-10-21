using System;
using System.Collections.Generic;
using System.Resources;
using WebTelNET.CommonNET.Resources;

namespace WebTelNET.Auth.Resources
{
    public class AccountResourceResolver : IResourceResolver
    {
        private readonly ResourceManager _resourceManager;

        public AccountResourceResolver()
        {
            _resourceManager = new ResourceManager(typeof(AccountResource));
        }

        public string ResolveException(Exception e)
        {
            throw new NotImplementedException();
        }
    }
}
