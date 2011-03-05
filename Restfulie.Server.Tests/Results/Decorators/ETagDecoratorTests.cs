using System;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;
using Moq;
using NUnit.Framework;
using Restfulie.Server.Results.Decorators;
using Restfulie.Server.Tests.Fixtures;
using Should;

namespace Restfulie.Server.Tests.Results.Decorators
{
	[TestFixture]
	public class ETagDecoratorTests
	{
		private Mock<ControllerContext> context;
		private Mock<HttpContextBase> httpContext;
		private Mock<HttpResponseBase> httpResponseBase;
		private Mock<HttpCachePolicyBase> cache;
		private SomeResource someResource;

		[SetUp]
		public void SetUp()
		{
			httpResponseBase = new Mock<HttpResponseBase>();
			httpContext = new Mock<HttpContextBase>();
			context = new Mock<ControllerContext>();
			cache = new Mock<HttpCachePolicyBase>();

			context.Setup(c => c.HttpContext).Returns(httpContext.Object);
			httpContext.Setup(h => h.Response).Returns(httpResponseBase.Object);
			httpResponseBase.Setup(h => h.Cache).Returns(cache.Object);


			someResource = new SomeResource
			               	{
			               		Amount = 10.54,
			               		Id = 2,
			               		Name = "Some Name",
			               		UpdatedAt = new DateTime(2010, 1, 1)

			               	};
		}

		[Test]
		public void ShouldSetETag()
		{
			new ETag(someResource).Execute(context.Object);

			cache.Verify(c => c.SetETag(someResource.GetEtag()));
		}

		[Test]
		public void ShouldNotSetETagWhenIsNotAResource()
		{
			var notAResource = new NonResourceEntity();
			new ETag(notAResource).Execute(context.Object);
			cache.Verify(c => c.SetETag(notAResource.GetETag()),Times.Never());
		}

		[Test]
		public void ShouldCallNextDecorator()
		{
			var nextDecorator = new Mock<ResultDecorator>();

			new ETag(someResource,nextDecorator.Object).Execute(context.Object);

			nextDecorator.Verify(nd => nd.Execute(context.Object));
		}

		class NonResourceEntity
		{
			public string GetETag()
			{
				return "foo";
			}
		}
	}
}