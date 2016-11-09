using System;
using System.Data.Common;
using WebTelNET.CommonNET.Resources;
using Xunit;
using Moq;
using WebTelNET.CommonNET.Libs.ExceptionResolvers;

namespace WebTelNET.CommonNET.Tests
{
    internal class TestDbException : DbException {}

    public class ResourceManager
    {
        [Fact]
        public void ResolveException_AnyException()
        {
            var mockExceptionResolverFactory = new Mock<IExceptionManager>();
            mockExceptionResolverFactory
                .Setup(mockManager => mockManager.GetLastException(It.IsAny<Exception>()))
                .Returns(new Exception());
            mockExceptionResolverFactory
                .Setup(mockManager => mockManager.CreateResolver(It.IsAny<Exception>()))
                .Returns(new DefaultExceptionResolver());
            IResourceManager manager = new WTResourceManager(mockExceptionResolverFactory.Object);
            Assert.Equal(DefaultResource.DefaultError, manager.GetByException(new Exception()));
        }

        [Fact]
        public void ResolveException_DbException()
        {
            var mockExceptionResolverFactory = new Mock<IExceptionManager>();
            mockExceptionResolverFactory
                .Setup(mockManager => mockManager.GetLastException(It.IsAny<Exception>()))
                .Returns(new TestDbException());
            mockExceptionResolverFactory
                .Setup(mockManager => mockManager.CreateResolver(It.IsAny<Exception>()))
                .Returns(new DbExceptionResolver());
            IResourceManager manager = new WTResourceManager(mockExceptionResolverFactory.Object);
            var exception = new TestDbException();
            Assert.Equal(string.Empty, manager.GetByException(exception));
        }

        [Fact]
        public void ResolveException_InnerException_AnyException()
        {
            var mockExceptionResolverFactory = new Mock<IExceptionManager>();
            mockExceptionResolverFactory
                .Setup(mockManager => mockManager.GetLastException(It.IsAny<Exception>()))
                .Returns(new Exception());
            mockExceptionResolverFactory
                .Setup(mockManager => mockManager.CreateResolver(It.IsAny<Exception>()))
                .Returns(new DefaultExceptionResolver());
            IResourceManager manager = new WTResourceManager(mockExceptionResolverFactory.Object);
            Assert.Equal(DefaultResource.DefaultError, manager.GetByException(new Exception("test message", new TestDbException())));
        }

        [Fact]
        public void ResolveException_InnerException_DbException()
        {
            var mockExceptionResolverFactory = new Mock<IExceptionManager>();
            mockExceptionResolverFactory
                .Setup(mockManager => mockManager.GetLastException(It.IsAny<Exception>()))
                .Returns(new TestDbException());
            mockExceptionResolverFactory
                .Setup(mockManager => mockManager.CreateResolver(It.IsAny<Exception>()))
                .Returns(new DbExceptionResolver());
            IResourceManager manager = new WTResourceManager(mockExceptionResolverFactory.Object);
            Assert.Equal(string.Empty, manager.GetByException(new Exception("test message", new TestDbException())));
        }
    }
}
