using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trefoil.Entity
{
    public interface ITrefoilEntity
    {
        void DropAndCreate(string serverName, string databaseName, string userId, string password);

        void Migrate(string serverName, string databaseName, string userId, string password);
    }
}
