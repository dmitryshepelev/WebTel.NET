using System;
using System.Data.Common;
using System.Resources;
using Npgsql;
using WebTelNET.CommonNET.Libs.ExceptionResolvers;

namespace WebTelNET.CommonNET.Resources
{
    public class WTResourceManager : IResourceManager
    {
        private readonly IExceptionManager _exceptionManager;
        private readonly ResourceManager _resourceManager;

        public WTResourceManager(IExceptionManager exceptionManager)
        {
            _exceptionManager = exceptionManager;
            _resourceManager = new ResourceManager(typeof(DefaultResource));
        }

        public virtual string GetByException(Exception e)
        {
            var lastExcpetion = _exceptionManager.GetLastException(e);
            var resolver = _exceptionManager.CreateResolver(lastExcpetion);
            var identifier = resolver.GetIdentifier(lastExcpetion);
            return GetByString(identifier);
        }

        public virtual string GetByString(string str)
        {
            return _resourceManager.GetString(str);
        }
    }
}
