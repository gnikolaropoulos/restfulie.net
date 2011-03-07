using System.Collections.Generic;
using NUnit.Framework;
using Restfulie.Server.Extensions;
using Restfulie.Server.Tests.Fixtures;
using Should;

namespace Restfulie.Server.Tests.Extensions
{
    [TestFixture]
    public class TypeExtensionsTests
    {
        [Test]
        public void ShouldKnowIfATypeIsAResource()
        {
            new SomeResource().GetType().IsAResource().ShouldBeTrue();
            123.GetType().IsAResource().ShouldBeFalse();
        }

		[Test]
		public void ShouldKnowIfATypeIsACacheableResource()
		{
			new SomeCacheableResource().GetType().IsCacheableResource().ShouldBeTrue();
			new SomeResource().GetType().IsCacheableResource().ShouldBeFalse();
		}

        [Test]
        public void SholdKnowIfATypeIsAListOfResources()
        {
            var list = new List<IBehaveAsResource>();
            var array = new[] {new SomeResource()};

            var notAResourceList = new List<int>();
            var notAResourceArray = new[] {1, 2};

            list.GetType().IsAListOfResources().ShouldBeTrue();
            array.GetType().IsAListOfResources().ShouldBeTrue();

        	notAResourceList.GetType().IsAListOfResources().ShouldBeFalse();
			notAResourceArray.GetType().IsAListOfResources().ShouldBeFalse();
        }
    }
}
