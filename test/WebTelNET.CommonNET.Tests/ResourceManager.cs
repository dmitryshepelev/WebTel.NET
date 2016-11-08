using System;
using System.Data.Common;
using Npgsql;
using WebTelNET.CommonNET.Resources;
using Xunit;

namespace WebTelNET.CommonNET.Tests
{
    internal class TestDbException : DbException {}

    public class ResourceManager
    {

        [Fact]
        public void ResolveException_AnyException()
        {
            IResourceManager manager = new WTResourceManager();
            Assert.Throws<NotImplementedException>(() => manager.ResolveException(new Exception()));
        }

        [Fact]
        public void ResolveException_DbException()
        {
            IResourceManager manager = new WTResourceManager();
            var exception = new TestDbException();
            Assert.Equal(string.Empty, manager.ResolveException(exception));
        }

        [Fact]
        public void ResolveException_InnerException_DbException()
        {
            IResourceManager manager = new WTResourceManager();
            var exception = new Exception("test message", new TestDbException());
            Assert.Equal(string.Empty, manager.ResolveException(exception));
        }

        [Fact]
        public void ResolveException_InnerException_AnyException()
        {
            IResourceManager manager = new WTResourceManager();
            var exception = new Exception("test message", new Exception());
            Assert.Throws<NotImplementedException>(() => manager.ResolveException(exception));
        }
    }
}
