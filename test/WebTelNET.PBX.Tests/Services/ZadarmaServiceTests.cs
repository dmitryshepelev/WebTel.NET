using System;
using System.Linq;
using WebTelNET.PBX.Services;
using Xunit;

namespace WebTelNET.PBX.Tests.Services
{
    public class ZadarmaServiceTests
    {
        private string _userKey = "097e0fc063d054d8d5cb";
        private string _secretKey = "7ad629bd16a93905ded2";

        #region GetBalanceAsync

        [Fact]
        public void GetBalanceAsync_Success()
        {
            var service = new ZadarmaService(_userKey, _secretKey);
            var result = service.GetBalanceAsync().Result;

            Assert.Equal(ZadarmaResponseStatus.Success, result.Status);
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

        #endregion

        #region GetPriceInfoAsync

        [Fact]
        public void GetPriceInfoAsync_Success()
        {
            var service = new ZadarmaService(_userKey, _secretKey);
            const string testNumber = "375259189613";
            var result = service.GetPriceInfoAsync(testNumber).Result;

            Assert.Equal(ZadarmaResponseStatus.Success, result.Status);
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
        public void GetPriceInfoAsync_Success_NumberWithPlus()
        {
            var service = new ZadarmaService(_userKey, _secretKey);
            const string testNumber = "+375259189613";
            var result = service.GetPriceInfoAsync(testNumber).Result;

            Assert.IsType<PriceInfoResponseModel>(result);
        }

        [Fact]
        public void GetPriceInfoAsync_Error_InvalidNumber()
        {
            var service = new ZadarmaService(_userKey, _secretKey);
            const string testNumber = "123456789";
            var result = service.GetPriceInfoAsync(testNumber).Result;

            Assert.IsType<ErrorResponseModel>(result);
        }

        [Fact]
        public void GetPriceInfoAsync_Error_InvalidNumber_2()
        {
            var service = new ZadarmaService(_userKey, _secretKey);
            const string testNumber = "555555555555555";
            var result = service.GetPriceInfoAsync(testNumber).Result;
            Console.WriteLine(result.Status);
            Assert.IsType<ErrorResponseModel>(result);
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

        #endregion

        #region GetOverallStatisticsAsync

        [Fact]
        public void GetOverallStatisticsAsync_Success()
        {
            var service = new ZadarmaService(_userKey, _secretKey);
            var result = service.GetOverallStatisticsAsync().Result;

            Assert.Equal(ZadarmaResponseStatus.Success, result.Status);
        }

        [Fact]
        public void GetOverallStatisticsAsync_Success_OverallStatisticsResponseModelIsGot()
        {
            var service = new ZadarmaService(_userKey, _secretKey);
            var result = service.GetOverallStatisticsAsync().Result;

            Assert.IsType<OverallStatisticsResponseModel>(result);
        }

        [Fact]
        public void GetOverallStatisticsAsync_Success_StartPropertyIsValid()
        {
            var service = new ZadarmaService(_userKey, _secretKey);
            var result = service.GetOverallStatisticsAsync().Result;
            var model = (OverallStatisticsResponseModel) result;

            Assert.Equal(1, model.Start.Day);
        }

        [Fact]
        public void GetOverallStatisticsAsync_Success_EndPropertyIsValid()
        {
            var service = new ZadarmaService(_userKey, _secretKey);
            var result = service.GetOverallStatisticsAsync().Result;
            var model = (OverallStatisticsResponseModel)result;

            Assert.Equal(DateTime.Today.Day, model.End.Day);
        }

        [Fact]
        public void GetOverallStatisticsAsync_Success_DateQuery_StartPropertyIsValid()
        {
            var service = new ZadarmaService(_userKey, _secretKey);
            var result = service.GetOverallStatisticsAsync(DateTime.Now.AddDays(-1)).Result;
            var model = (OverallStatisticsResponseModel)result;

            Assert.Equal(DateTime.Now.AddDays(-1).Day, model.Start.Day);
        }

        [Fact]
        public void GetOverallStatisticsAsync_Success_DateQuery_EndPropertyIsValid()
        {
            var service = new ZadarmaService(_userKey, _secretKey);
            var result = service.GetOverallStatisticsAsync(end: DateTime.Now.AddDays(-1)).Result;
            var model = (OverallStatisticsResponseModel)result;

            Assert.Equal(DateTime.Now.AddDays(-1).Day, model.End.Day);
        }

        [Fact]
        public void GetOverallStatisticsAsync_Success_DateQuery_StartEndPropertiesAreValid()
        {
            var service = new ZadarmaService(_userKey, _secretKey);
            var result = service.GetOverallStatisticsAsync(DateTime.Now.AddDays(-5), DateTime.Now.AddDays(-2)).Result;
            var model = (OverallStatisticsResponseModel)result;

            Assert.Equal(DateTime.Now.AddDays(-5).Day, model.Start.Day);
            Assert.Equal(DateTime.Now.AddDays(-2).Day, model.End.Day);
        }

        [Fact]
        public void GetOverallStatisticsAsync_Success_DateQuery_StartGreaterThanEnd()
        {
            var service = new ZadarmaService(_userKey, _secretKey);
            var result = service.GetOverallStatisticsAsync(DateTime.Now, DateTime.Now.AddDays(-10)).Result;
            var model = (OverallStatisticsResponseModel)result;

            Assert.Equal(DateTime.Now.Day, model.Start.Day);
            Assert.Equal(DateTime.Now.AddDays(+1).Day, model.End.Day);
        }

        #endregion

        #region GetPBXStatisticsAsync

        [Fact]
        public void GetPBXStatisticsAsync_Success()
        {
            var service = new ZadarmaService(_userKey, _secretKey);
            var result = service.GetPBXStatisticsAsync().Result;

            Assert.Equal(ZadarmaResponseStatus.Success, result.Status);
        }

        [Fact]
        public void GetPBXStatisticsAsync_Success_PBXStatisticsResponseModelIsGot()
        {
            var service = new ZadarmaService(_userKey, _secretKey);
            var result = service.GetPBXStatisticsAsync().Result;

            Assert.IsType<PBXStatisticsResponseModel>(result);
        }

        [Fact]
        public void GetPBXStatisticsAsync_Success_ResponseVersionIsValid()
        {
            var service = new ZadarmaService(_userKey, _secretKey);
            var result = service.GetPBXStatisticsAsync().Result;

            var model = (PBXStatisticsResponseModel) result;
            Assert.Equal(ResponseVersion.Old, model.Version);
        }

        [Fact]
        public void GetPBXStatisticsAsync_Success_StartPropertyIsValid()
        {
            var service = new ZadarmaService(_userKey, _secretKey);
            var result = service.GetPBXStatisticsAsync().Result;

            var model = (PBXStatisticsResponseModel) result;
            Assert.Equal(1, model.Start.Day);
        }

        [Fact]
        public void GetPBXStatisticsAsync_Success_EndPropertyIsValid()
        {
            var service = new ZadarmaService(_userKey, _secretKey);
            var result = service.GetPBXStatisticsAsync().Result;

            var model = (PBXStatisticsResponseModel) result;
            Assert.Equal(DateTime.Now.Day, model.End.Day);
        }

        [Fact]
        public void GetPBXStatisticsAsync_Success_DateQuery_StartPropertyIsValid()
        {
            var service = new ZadarmaService(_userKey, _secretKey);
            var result = service.GetPBXStatisticsAsync(DateTime.Now.AddDays(-1)).Result;

            var model = (PBXStatisticsResponseModel) result;
            Assert.Equal(DateTime.Now.AddDays(-1).Day, model.Start.Day);
        }

        [Fact]
        public void GetPBXStatisticsAsync_Success_DateQuery_EndPropertyIsValid()
        {
            var service = new ZadarmaService(_userKey, _secretKey);
            var result = service.GetPBXStatisticsAsync(end: DateTime.Now.AddDays(-5)).Result;

            var model = (PBXStatisticsResponseModel) result;
            Assert.Equal(DateTime.Now.AddDays(-5).Day, model.End.Day);
        }

        [Fact]
        public void GetPBXStatisticsAsync_Success_DateQuery_StartAndEndPropertiesAreValid()
        {
            var service = new ZadarmaService(_userKey, _secretKey);
            var result = service.GetPBXStatisticsAsync(DateTime.Now.AddDays(-8), DateTime.Now.AddDays(-3)).Result;

            var model = (PBXStatisticsResponseModel) result;
            Assert.Equal(DateTime.Now.AddDays(-8).Day, model.End.Day);
            Assert.Equal(DateTime.Now.AddDays(-3).Day, model.End.Day);
        }

        [Fact]
        public void GetPBXStatisticsAsync_Success_DateQuery_StartGreaterThanEnd()
        {
            var service = new ZadarmaService(_userKey, _secretKey);
            var result = service.GetPBXStatisticsAsync(DateTime.Now.AddDays(-2), DateTime.Now.AddDays(-6)).Result;

            var model = (PBXStatisticsResponseModel) result;
            Assert.Equal(DateTime.Now.AddDays(-2).Day, model.End.Day);
            Assert.Equal(DateTime.Now.Day, model.End.Day);
        }

        #endregion

        #region GetCallRecordLinkAsync

        private string GetPBXCallId(IZadarmaService service)
        {
            var statResult = service.GetPBXStatisticsAsync().Result;
            var stat = statResult as PBXStatisticsResponseModel;

            if (stat == null) { throw new NullReferenceException("Stat is null"); }

            return stat.Stats.First().pbx_call_id;
        }

        [Fact]
        public void GetCallRecordLinkAsync_Success()
        {
            IZadarmaService service = new ZadarmaService(_userKey, _secretKey);

            var pbxCallId = GetPBXCallId(service);
            var result = service.GetCallRecordLinkAsync(pbxCallId).Result;

            Assert.IsType<CallRecordLinkResponseModel>(result);
        }

        [Fact]
        public void GetcallRecordLinkAsync_Failure_InvalidPbxCallId()
        {
            IZadarmaService service = new ZadarmaService(_userKey, _secretKey);

            var result = service.GetCallRecordLinkAsync("invalid_pbx_call_id").Result;

            Assert.IsType<ErrorResponseModel>(result);
        }

        #endregion

        #region RequestCallbackAsync

        // No chance to test

        #endregion

        #region ParseNotificationType

        [Fact]
        public void ParseNotificationType_Success()
        {
            var type = ZadarmaService.ParseNotificationType("NOTIFY_START");
            Assert.Equal(CallNotificationType.NotifyStart, type);
        }

        [Fact]
        public void ParseNotificationType_Success_LowerCaseString()
        {
            var type = ZadarmaService.ParseNotificationType("notify_end");
            Assert.Equal(CallNotificationType.NotifyEnd, type);
        }

        [Fact]
        public void ParseNotificationType_Success_CamelCaseString()
        {
            var type = ZadarmaService.ParseNotificationType("notifyInternal");
            Assert.Equal(CallNotificationType.NotifyInternal, type);
        }

        [Fact]
        public void ParseNotificationType_Success_PascalCaseString()
        {
            var type = ZadarmaService.ParseNotificationType("NotifyOutStart");
            Assert.Equal(CallNotificationType.NotifyOutStart, type);
        }

        [Fact]
        public void ParseNotificationType_Success_UpperCaseString()
        {
            var type = ZadarmaService.ParseNotificationType("NOTIFYOUTEND");
            Assert.Equal(CallNotificationType.NotifyOutEnd, type);
        }

        [Fact]
        public void ParseNotificationType_Failure_EmptyArgument()
        {
            Assert.Throws<Exception>(() => ZadarmaService.ParseNotificationType(""));
        }

        [Fact]
        public void ParseNotificationType_Failure_InvalidCast()
        {
            Assert.Throws<InvalidCastException>(() => ZadarmaService.ParseNotificationType("some_string"));
        }

        #endregion

        #region ParseDispositionType

        [Fact]
        public void ParseDispositionType_Success_SimpleString()
        {
            var type = ZadarmaService.ParseDispositionType("answered");
            Assert.Equal(CallDispositionType.Answered, type);
        }

        [Fact]
        public void ParseDispositionType_Success_StringWithSpace()
        {
            var type = ZadarmaService.ParseDispositionType("no answer");
            Assert.Equal(CallDispositionType.NoAnswer, type);
        }

        [Fact]
        public void ParseDispositionType_Success_StringWithSeveralSpaces()
        {
            var type = ZadarmaService.ParseDispositionType("no day limit");
            Assert.Equal(CallDispositionType.NoDayLimit, type);
        }

        [Fact]
        public void ParseDispositionType_Success_StringWithComa()
        {
            var type = ZadarmaService.ParseDispositionType("no money, no limit");
            Assert.Equal(CallDispositionType.NoMoneyNoLimit, type);
        }

        [Fact]
        public void ParseDispositionType_Failure_EmptyArgument()
        {
            Assert.Throws<Exception>(() => ZadarmaService.ParseNotificationType(""));
        }

        [Fact]
        public void ParseDispositionType_Failure_InvalidCast()
        {
            Assert.Throws<InvalidCastException>(() => ZadarmaService.ParseNotificationType("some_string"));
        }

        #endregion
    }
}
