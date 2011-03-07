using System.Web;
using System.Web.Mvc;
using Restfulie.Server.Extensions;

namespace Restfulie.Server.Results.Decorators
{
	public class ETag : ResultDecorator
	{
		private readonly object model;

		public ETag(object model)
		{
			this.model = model;
		}

		public ETag(object model, ResultDecorator nextDecorator)
			: base(nextDecorator)
		{
			this.model = model;
		}

		public override void Execute(ControllerContext context)
		{
			if (model.GetType().IsCacheableResource())
			{
				HttpCachePolicyBase cache = context.HttpContext.Response.Cache;
				cache.SetETag(model.AsCacheableResource().GetEtag());
				cache.SetCacheability(HttpCacheability.ServerAndPrivate);
			}

			Next(context);
		}
	}

	public class LastModified : ResultDecorator
	{
		private readonly object model;

		public LastModified(object model)
		{
			this.model = model;
		}

		public LastModified(object model, ResultDecorator nextDecorator)
			: base(nextDecorator)
		{
			this.model = model;
		}

		public override void Execute(ControllerContext context)
		{
			if (model.GetType().IsCacheableResource())
			{
				HttpCachePolicyBase cache = context.HttpContext.Response.Cache;
				cache.SetLastModified(model.AsCacheableResource().GetLastModified());
				cache.SetCacheability(HttpCacheability.ServerAndPrivate);
			}

			Next(context);
		}
	}
}