using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public abstract class MongoServiceBase : IMongoServices
    {
        public virtual IMongoDatabase ConnectAndGetDatabase(string Confog, string DbName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Use this method for connect and get database
        /// </summary>
        /// <param name="Config"></param>
        /// <param name="DbName"></param>
        /// <returns></returns>
        //public IMongoDatabase ConnectAndGetDB(string Config, string DbName);
       

        //public void MakeCollectionQueryAble();
       


    }
}
