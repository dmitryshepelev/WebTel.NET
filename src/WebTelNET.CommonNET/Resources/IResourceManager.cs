﻿using System;
using System.Collections.Generic;

namespace WebTelNET.CommonNET.Resources
{
    public interface IResourceManager
    {
        string ResolveException(Exception e);
    }
}