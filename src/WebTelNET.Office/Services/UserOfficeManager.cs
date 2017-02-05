using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.Extensions.Options;
using WebTelNET.Office.Models.Models;
using WebTelNET.Office.Models.Repository;

namespace WebTelNET.Office.Services
{
    public interface IUserOfficeManager
    {
        void AddServiceToUserOffice(UserOffice userOffice, string serviceTypeName);
        UserService GetUserService(UserOffice userOffice, string serviceTypeName);
        IQueryable<UserService> GetUserServices(UserOffice userOffice,
            Expression<Func<UserService, bool>> expression = null);
    }

    public class UserOfficeManager : IUserOfficeManager
    {
        private readonly IUserOfficeRepository _userOfficeRepository;
        private readonly IServiceProviderRepository _serviceProviderRepository;
        private readonly IServiceTypeRepository _serviceTypeRepository;
        private readonly IUserServcieRepository _userServiceRepository;

        public UserOfficeManager(
            IUserOfficeRepository userOfficeRepository,
            IServiceProviderRepository serviceProviderRepository,
            IServiceTypeRepository serviceTypeRepository,
            IUserServcieRepository userServiceRepository
        )
        {
            _userOfficeRepository = userOfficeRepository;
            _serviceProviderRepository = serviceProviderRepository;
            _serviceTypeRepository = serviceTypeRepository;
            _userServiceRepository = userServiceRepository;
        }

        public void AddServiceToUserOffice(UserOffice userOffice, string serviceTypeName)
        {
            var serviceProvider = _serviceProviderRepository.GetByType(serviceTypeName);
            if (serviceProvider != null)
            {
                var service = _userServiceRepository.GetSingle(
                    x => x.UserOfficeId.Equals(userOffice.Id) && x.ServiceProvider.Id.Equals(serviceProvider.Id));
                if (service == null)
                {
                    _userServiceRepository.Create(new UserService
                    {
                        ServiceProviderId = serviceProvider.Id,
                        UserOfficeId = userOffice.Id
                    });
                }
            }

        }

        public UserService GetUserService(UserOffice userOffice, string serviceTypeName)
        {
            return _userServiceRepository.GetSingleWithNavigationProperties(x => x.UserOfficeId.Equals(userOffice.Id) &&
                                                  x.ServiceProvider.ServiceType.Name.Equals(serviceTypeName));
        }

        public IQueryable<UserService> GetUserServices(UserOffice userOffice, Expression<Func<UserService, bool>> expression = null)
        {
            var services = _userServiceRepository.GetAllWithNavigationProperties(x => x.UserOfficeId.Equals(userOffice.Id));
            return expression == null ? services : services.Where(expression);
        }
    }
}