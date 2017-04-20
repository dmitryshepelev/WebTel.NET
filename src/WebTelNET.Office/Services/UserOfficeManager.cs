using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.Extensions.Options;
using WebTelNET.Office.Libs.Models;
using WebTelNET.Office.Models.Libs;
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

        ServiceActivationStatus ActivateUserService(UserOffice userOffice, string serviceTypeName, IDictionary<string, string> data = null);
    }

    public class UserOfficeManager : IUserOfficeManager
    {
        private readonly IUserOfficeRepository _userOfficeRepository;
        private readonly IServiceProviderRepository _serviceProviderRepository;
        private readonly IServiceTypeRepository _serviceTypeRepository;
        private readonly IUserServcieRepository _userServiceRepository;
        private readonly IUserServiceDataRepository _userServiceDataRepository;
        private readonly IOptions<AppSettings> _appSettings;

        public UserOfficeManager(
            IUserOfficeRepository userOfficeRepository,
            IServiceProviderRepository serviceProviderRepository,
            IServiceTypeRepository serviceTypeRepository,
            IUserServcieRepository userServiceRepository,
            IOptions<AppSettings> appSettings,
            IUserServiceDataRepository userServiceDataRepository
        )
        {
            _userOfficeRepository = userOfficeRepository;
            _serviceProviderRepository = serviceProviderRepository;
            _serviceTypeRepository = serviceTypeRepository;
            _userServiceRepository = userServiceRepository;
            _appSettings = appSettings;
            _userServiceDataRepository = userServiceDataRepository;
        }

        public void AddServiceToUserOffice(UserOffice userOffice, string serviceTypeName)
        {
            var serviceProvider = _serviceProviderRepository.GetByType(serviceTypeName);
            if (serviceProvider != null)
            {
                var service = _userServiceRepository.GetSingle(
                                  x => x.UserOfficeId.Equals(userOffice.Id) && x.ServiceProvider.Id.Equals(serviceProvider.Id)) ??
                              _userServiceRepository.Create(new UserService
                              {
                                  ServiceProviderId = serviceProvider.Id,
                                  UserOfficeId = userOffice.Id
                              });

                var serviceProviderSettings = (ServiceProviderSettings)_appSettings.Value.ServiceProviderTypeSettings.GetType()
                    .GetProperty(serviceTypeName)
                    .GetValue(_appSettings.Value.ServiceProviderTypeSettings);

                if (serviceProviderSettings.UserData != null)
                {
                    foreach (var pair in serviceProviderSettings.UserData)
                    {
                        var userServiceData =
                            _userServiceDataRepository.GetSingle(
                                x => x.UserServiceId.Equals(service.Id) && x.Key.Equals(pair.Key));
                        if (userServiceData == null)
                        {
                            _userServiceDataRepository.Create(new UserServiceData
                            {
                                Key = pair.Key,
                                Value = string.IsNullOrEmpty(pair.Value) ? null : pair.Value,
                                UserServiceId = service.Id
                            });
                        }
                        else if (userServiceData.Value != pair.Value)
                        {
                            userServiceData.Value = pair.Value;
                            _userServiceDataRepository.Update(userServiceData);
                        }
                    }
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

        public ServiceActivationStatus ActivateUserService(UserOffice userOffice, string serviceTypeName, IDictionary<string, string> data = null)
        {
            var service = GetUserService(userOffice, serviceTypeName);
            if (service == null || service.ServiceStatusId != (int) ServiceStatuses.Available)
            {
                return ServiceActivationStatus.UnableToActivate;
            }
            data = data ?? new Dictionary<string, string>();

            var unfilledData =
                _userServiceDataRepository.GetAll(x => x.UserServiceId.Equals(service.Id) && string.IsNullOrEmpty(x.Value)).ToList();
            bool allDataUpdated = true;

            foreach (var serviceData in unfilledData)
            {
                string value;
                var result = data.TryGetValue(serviceData.Key, out value);
                if (result)
                {
                    serviceData.Value = value;
                    _userServiceDataRepository.Update(serviceData);
                }
                else
                {
                    allDataUpdated = false;
                    break;
                }
            }
            return allDataUpdated ? _userServiceRepository.Activate(service) : ServiceActivationStatus.RequireAdditionData;
        }
    }
}