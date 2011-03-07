using System;
using System.Security.Cryptography;
using System.Text;
using Restfulie.Server;
using Web.Controllers;

namespace Web.Models
{
    public class Item : IBehaveAsCacheableResource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
		public DateTime UpdatedAt { get; set; }

        public void SetRelations(Relations relations)
        {
            relations.Named("self").Uses<ItemsController>().Get(Id);
            relations.Named("origin").At("http://www.some-fabric.com/");
        }

    public string GetEtag()
    	{
    		string value = Id + Name + Price;
    		var byteArray = Encoding.UTF8.GetBytes(value);
			var prov = new MD5CryptoServiceProvider();
    		var hash = prov.ComputeHash(byteArray);
    		var result = new StringBuilder();
    		foreach (var b in hash)
    		{
    			result.Append(b.ToString("X2"));
    		}
    		return result.ToString();
		}

    	public DateTime GetLastModified()
    	{
    		return UpdatedAt;
    	}
    }
}
