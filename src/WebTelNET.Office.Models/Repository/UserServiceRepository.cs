using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using WebTelNET.CommonNET.Libs.Repository;
using WebTelNET.Office.Libs.Models;
using WebTelNET.Office.Models.Models;

namespace WebTelNET.Office.Models.Repository
{
    public interface IUserServcieRepository : IRepository<UserService>
    {
        ServiceOperationStatus Activate(UserService userService);
    }
    
    public class UserServiceRepository : RepositoryBase<UserService>, IUserServcieRepository
    {
        public UserServiceRepository(OfficeDbContext context) : base(context)
        {
        }

        private IIncludableQueryable<UserService, ServiceStatus> GetWithNavigationProperties()
        {
            return Context.Set<UserService>()
                .Include(x => x.ServiceProvider).ThenInclude(x => x.ServiceType)
                .Include(x => x.ServiceStatus);
        }

        public override UserService GetSingleWithNavigationProperties(Expression<Func<UserService, bool>> predicate)
        {
            return GetWithNavigationProperties().FirstOrDefault(predicate);
        }

        public override IQueryable<UserService> GetAllWithNavigationProperties(Expression<Func<UserService, bool>> predicate = null)
        {
            var query = GetWithNavigationProperties();
            return predicate != null ? query.Where(predicate) : query;
        }

        public ServiceOperationStatus Activate(UserService userService)
        {
            if (userService.ServiceStatusId == (int) ServiceStatuses.Available)
            {
                userService.ActivationDateTime = DateTime.Now;
                userService.ServiceStatusId = (int) ServiceStatuses.Activated;
                Update(userService);
                return ServiceOperationStatus.ActivationSucceed;
            }
            return ServiceOperationStatus.UnableToActivate;
        }
    }
}