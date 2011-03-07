using System.Net;
using NUnit.Framework;
using Should;

namespace Restfulie.Server.Tests.Cache
{
	[TestFixture,Explicit("As we still don't have a integration test suites, to run this tests one has to manually start the web server")]
	public class CachingTest : IntegrationTestBase
	{
		[Test]
		public void ShouldSetETagOnResponse()
		{
			var response = ExecuteRequest("http://localhost:8082/Items/1");

			response.Headers[HttpResponseHeader.ETag].ShouldNotBeNull();
		}

		[Test]
		public void ShouldSetLastModifiedOnResponse()
		{
			var response = ExecuteRequest("http://localhost:8082/Items/1");
			response.Headers[HttpResponseHeader.LastModified].ShouldNotBeNull();
		}
		
	}
}