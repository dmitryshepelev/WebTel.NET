﻿using Microsoft.EntityFrameworkCore;
using WebTelNET.CommonNET.Libs.Repository;
using WebTelNET.PBX.Models.Models;

namespace WebTelNET.PBX.Models.Repository
{
    public interface ICallRepository : IRepository<Call>
    {

    }

    public class CallRepository : RepositoryBase<Call>, ICallRepository
    {
        public CallRepository(PBXDbContext context) : base(context)
        {
        }
    }
}