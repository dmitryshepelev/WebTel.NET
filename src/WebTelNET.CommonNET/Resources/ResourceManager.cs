using System;
using System.Data.Common;

namespace WebTelNET.CommonNET.Resources
{
    public class WTResourceManager : IResourceManager
    {
        protected IResourceResolver GetResolver(ref Exception e)
        {
            Exception exceptionTyped = null;
            IResourceResolver resolver = null;

            while (e != null)
            {
                exceptionTyped = e as DbException;
                if (exceptionTyped != null)
                {
                    resolver = new DbResourceResolver();
                }
                e = e.InnerException;
            }
            e = exceptionTyped;

            if (resolver == null)
            {
                return new CommonResourceResolver();
            }
            return resolver;
        }

        public virtual string ResolveException(Exception e)
        {
            var resolver = GetResolver(ref e);
            return resolver.ResolveException(e);
        }
    }
}
