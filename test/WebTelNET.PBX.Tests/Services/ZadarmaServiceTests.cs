using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using WebTelNET.PBX.Services;
using Xunit;

namespace WebTelNET.PBX.Tests.Services
{
    public class ZadarmaServiceTests
    {
        private string _userKey = "097e0fc063d054d8d5cb";
        private string _secretKey = "7ad629bd16a93905ded2";

        [Fact]
        public void GetBalanceAsync_Success()
        {
            var service = new ZadarmaService(_userKey, _secretKey);
            var result = service.GetBalanceAsync().Result;

            Assert.Equal("success", result.Status);
        }

        [Fact]
        public void GetBalanceAsync_Success_BalanceResponseModelIsGot()
        {
            var service = new ZadarmaService(_userKey, _secretKey);
            var result = service.GetBalanceAsync().Result;

            Assert.IsType<BalanceResponseModel>(result);
        }

        [Fact]
        public void GetBalanceAsync_Success_ActualBalanceIsNumeric()
        {
            var service = new ZadarmaService(_userKey, _secretKey);
            var result = service.GetBalanceAsync().Result;
            var successResult = (BalanceResponseModel)result;

            Assert.NotEqual(0, successResult.Balance);
        }

        [Fact]
        public void GetBalanceAsync_Success_ActualCurrencyIsGot()
        {
            var service = new ZadarmaService(_userKey, _secretKey);
            var result = service.GetBalanceAsync().Result;
            var successResult = (BalanceResponseModel)result;

            Assert.Equal("RUB", successResult.Currency);
        }

        [Fact]
        public void GetBalanceAsync_Error_Unauthorized()
        {
            var service = new ZadarmaService(_userKey, _userKey);
            var e = Assert.Throws<AggregateException>(() => service.GetBalanceAsync().Result);

            Assert.Equal(1, e.InnerExceptions.Count);
            var exception = e.InnerExceptions.First();
            Assert.IsType<ZadarmaServiceRequestException>(exception);

            var zadarmaException = (ZadarmaServiceRequestException) exception;
            Assert.Equal(HttpStatusCode.Unauthorized, zadarmaException.StatusCode);
        }

        [Fact]
        public void GetPriceInfoAsync_Success()
        {
            var service = new ZadarmaService(_userKey, _secretKey);
            const string testNumber = "375259189613";
            var result = service.GetPriceInfoAsync(testNumber).Result;

            Assert.Equal("success", result.Status);
        }

        [Fact]
        public void GetPriceInfoAsync_Success_PriceInfoResponseModelIsGot()
        {
            var service = new ZadarmaService(_userKey, _secretKey);
            const string testNumber = "375259189613";
            var result = service.GetPriceInfoAsync(testNumber).Result;

            Assert.IsType<PriceInfoResponseModel>(result);
        }

        [Fact]
        public void GetPriceInfoAsync_Error_ErrorResponseModelIsGot()
        {
            var service = new ZadarmaService(_userKey, _secretKey);
            const string testNumber = "InvalidNumber";
            var result = service.GetPriceInfoAsync(testNumber).Result;

            Assert.IsType<ErrorResponseModel>(result);
        }
    }
}
