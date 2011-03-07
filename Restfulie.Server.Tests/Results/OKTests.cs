using Moq;
using NUnit.Framework;
using Restfulie.Server.MediaTypes;
using Restfulie.Server.Results;
using Restfulie.Server.Results.Decorators;
using Should;

namespace Restfulie.Server.Tests.Results
{
    [TestFixture]
    public class OKTests
    {
        private OK result;

        [SetUp]
        public void SetUp()
        {
            var mediaType = new Mock<IMediaType>();
            mediaType.SetupGet(mt => mt.Synonyms).Returns(new[] { "media-type" });

            result = new OK
            {
                MediaType = mediaType.Object
            };
        }

        [Test]
        public void ShouldSetStatusCode()
        {
            result.GetDecorators().Contains(typeof(StatusCode)).ShouldBeTrue();
        }

        [Test]
        public void ShouldSetContent()
        {
            result.GetDecorators().Contains(typeof(Content)).ShouldBeTrue();
        }

        [Test]
        public void ShouldSetContentType()
        {
            result.GetDecorators().Contains(typeof(ContentType)).ShouldBeTrue();
        }

		[Test]
		public void ShouldSetETag()
		{
			result.GetDecorators().Contains(typeof(ETag)).ShouldBeTrue();
		}
    }
}
