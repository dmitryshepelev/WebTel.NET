using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Threading.Tasks;

namespace WebTelNET.CommonNET.Resources
{
    public class CommonResourceResolver : IResourceResolver
    {
        private readonly ResourceManager _resourceManager;

        public CommonResourceResolver()
        {
            _resourceManager = new ResourceManager(typeof(CommonResource));
        }

        public string ResolveException(Exception e)
        {
            throw new NotImplementedException();
        }
    }
}
