using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebTelNET.PBX.Models
{
    public class CallbackModel
    {
        public string From { get; set; }
        public string To { get; set; }
        public bool IsPredicted { get; set; }

        public CallbackModel()
        {
            IsPredicted = true;
        }
    }
}
