using System;
using System.Security.Cryptography;
using System.Text;

namespace Restfulie.Server.Tests.Fixtures
{
    [Serializable]
    public class SomeResource : IBehaveAsResource
    {
        public string Name { get; set; }
        public double Amount { get; set; }
        public int Id { get; set; }
        public DateTime UpdatedAt { get; set; }

        public void SetRelations(Relations relations)
        {
            relations.Named("pay").Uses<SomeController>().SomeSimpleAction();
        }

    	public string GetEtag()
    	{
    		string value = Name + Amount + Id + UpdatedAt.ToString("yyyyMMddhhmmss");
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
    }
}