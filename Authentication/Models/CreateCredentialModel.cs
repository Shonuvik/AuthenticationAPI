using System;
namespace Authentication.Models
{
	public class CreateCredentialModel
	{
        public string UserName { get; set; }

        public string ClientId { get; set; }

        public string Secret { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}

