using System;
using System.Resources;
using WebTelNET.CommonNET.Libs.ExceptionResolvers;
using WebTelNET.CommonNET.Resources;

namespace WebTelNET.Auth.Resources
{
    public class AccountResourceManager : WTResourceManager, IAccountResourceManager
    {
        private readonly ResourceManager _resourceManager;

        public AccountResourceManager(IExceptionManager exceptionManager) : base(exceptionManager)
        {
            _resourceManager = new ResourceManager(typeof(AccountResource));
        }

        public override string GetByString(string str)
        {
            var text = _resourceManager.GetString(str);
            if (string.IsNullOrEmpty(text))
            {
                text = base.GetByString(str);
            }
            return text;
        }
    }
}
