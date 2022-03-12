using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApp102.Configrasyon
{
    public class ConnectionString
    {
     public   string m_connectionString { get {

                return @"Server=BATU;Database=TodoDb;Trusted_Connection=True;";
            
            }
        }
    }
}
