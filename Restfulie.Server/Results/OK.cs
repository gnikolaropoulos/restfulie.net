using System.Net;
using System.Linq;
using Restfulie.Server.Results.Decorators;

namespace Restfulie.Server.Results
{
    public class OK : RestfulieResult
    {
    	private readonly object model;
    	public OK() { }
        public OK(object model) : base(model)
        {
        	this.model = model;
        }

    	public override ResultDecorator GetDecorators()
        {
            return new StatusCode((int)HttpStatusCode.OK,
				new ETag(model,
				new LastModified(model,
				new ContentType(MediaType.Synonyms.First(),
				new Content(BuildContent())))));
        }
    }
}