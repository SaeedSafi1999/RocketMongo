using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IMongoServices
    {
        /// <summary>
        /// connect and get database in one function
        /// </summary>
        /// <param name="Confog"></param>
        /// <param name="DbName"></param>
        /// <returns></returns>
        IMongoDatabase ConnectAndGetDatabase(string Confog, string DbName);
    }
}
