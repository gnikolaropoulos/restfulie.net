using System.Web.Mvc;
using Restfulie.Server.Extensions;

namespace Restfulie.Server.Results.Decorators
{
	public class ETag : ResultDecorator
	{
		private readonly object _model;

		public ETag(object model)
		{
			_model = model;
		}

		public ETag(object model, ResultDecorator nextDecorator)
			: base(nextDecorator)
		{
			_model = model;
		}

		public override void Execute(ControllerContext context)
		{
			if (_model.GetType().IsAResource())
			{
				context.HttpContext.Response.Cache.SetETag(_model.AsResource().GetEtag());
			}

			Next(context);
		}
	}
}