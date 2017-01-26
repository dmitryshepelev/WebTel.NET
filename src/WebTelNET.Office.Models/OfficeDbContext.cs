using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WebTelNET.Office.Models
{
    public class OfficeDbContext : DbContext
    {
        public OfficeDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}