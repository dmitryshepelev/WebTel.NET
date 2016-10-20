using System;
using System.Collections.Generic;

namespace WebTelNET.CommonNET.Resources
{
    public interface IResourceManager
    {
        IDictionary<string, string> ResolveExeption(Exception e);
    }
}
