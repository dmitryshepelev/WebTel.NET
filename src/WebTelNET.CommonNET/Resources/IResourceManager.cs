using System;
using System.Collections.Generic;

namespace WebTelNET.CommonNET.Resources
{
    public interface IResourceManager
    {
        string GetByException(Exception e);

        string GetByString(string str);
    }
}
