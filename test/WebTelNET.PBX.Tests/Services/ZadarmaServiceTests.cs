﻿using WebTelNET.PBX.Services;
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
        public void GetBalanceAsync_Failure_Unauthorized()
        {
            var service = new ZadarmaService(_userKey, _userKey);
            var result = service.GetBalanceAsync().Result;

            Assert.Equal(ZadarmaResponseStatus.Error, result.Status);
            var model = (ErrorResponseModel) result;
            Assert.Equal("Not authorized", model.Message);
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
        public void GetPriceInfoAsync_Failure_ErrorResponseModelIsGot_InvalidNumber()
        {
            var service = new ZadarmaService(_userKey, _secretKey);
            const string testNumber = "InvalidNumber";
            var result = service.GetPriceInfoAsync(testNumber).Result;

            Assert.IsType<ErrorResponseModel>(result);
        }

        [Fact]
        public void GetPriceInfoAsync_Failure_ErrorResponseModelIsGot_EmptyNumber()
        {
            var service = new ZadarmaService(_userKey, _secretKey);
            const string testNumber = "";
            var result = service.GetPriceInfoAsync(testNumber).Result;

            Assert.IsType<ErrorResponseModel>(result);
        }
    }
}