using System.Net;
using NUnit.Framework;
namespace Restfulie.Server.Tests
{
	[TestFixture]
	public class IntegrationTestBase
	{
		private string accept = "application/xml";
		private string method = "GET";

		public HttpWebResponse ExecuteRequest(string uri)
		{
			var webRequest = (HttpWebRequest)WebRequest.Create(uri);
			webRequest.Accept = accept;
			webRequest.Method = method;
			var response = (HttpWebResponse)webRequest.GetResponse();
			return response;
		}

		public HttpWebResponse ExecuteRequest(string uri, string accept)
		{
			this.accept = accept;
			return ExecuteRequest(uri);
		}

		public HttpWebResponse ExecuteRequest(string uri, string accept, string method)
		{
			this.accept = accept;
			this.method = method;
			return ExecuteRequest(uri);
		}
	}
}