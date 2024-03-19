using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TokenAuthenticationWEBAPI.Models
{
    public class ClientMasterRepository : IDisposable
    {
        SECURITY_DBEntities context = new SECURITY_DBEntities();
        public ClientMaster ValidateClient(string ClientID, string ClientSecret)
        {
            return context.ClientMasters.FirstOrDefault(user =>
             user.ClientID == ClientID &&
             user.ClientSecret == ClientSecret);
        }
        public void Dispose()
        {
            context.Dispose();
        }
    }
}