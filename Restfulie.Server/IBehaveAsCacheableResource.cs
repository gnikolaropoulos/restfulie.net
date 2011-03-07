using System;

namespace Restfulie.Server
{
	public interface IBehaveAsCacheableResource : IBehaveAsResource
	{
		string GetEtag();
		DateTime GetLastModified();
	}
}