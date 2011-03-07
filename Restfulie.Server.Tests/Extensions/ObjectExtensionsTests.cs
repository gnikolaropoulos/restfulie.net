using System.Collections.Generic;
using NUnit.Framework;
using Restfulie.Server.Tests.Fixtures;
using Restfulie.Server.Extensions;
using Should;

namespace Restfulie.Server.Tests.Extensions
{
    [TestFixture]
    public class ObjectExtensionsTests
    {
        [Test]
        public void ShouldConvertToAnArrayOfResources()
        {
            var resource = new SomeResource();
            var list = new List<SomeResource> {resource};

            var array = list.AsResourceArray();

        	array.Length.ShouldEqual(1);
        	array[0].ShouldEqual(resource);
        }

        [Test]
        public void ShouldGetPropertyIfItHasOne()
        {
            var resource = new SomeResource { Id = 123 };

			resource.GetProperty("Id").ShouldEqual(123);
			resource.GetProperty("CrazyProperty").ShouldBeNull();
        }
    }
}
