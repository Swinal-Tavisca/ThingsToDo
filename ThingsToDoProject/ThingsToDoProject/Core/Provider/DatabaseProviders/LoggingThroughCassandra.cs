using Cassandra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThingsToDoProject.Model;

namespace ThingsToDoProject.Core.Provider.DatabaseProviders
{
    public class LoggingThroughCassandra
    {
        public bool Add(Log Log)
        {
            try
            {
                //--------------------------Cassandra----------------------------

                // Connect to the TicTacToe keyspace on our cluster running at 127.0.0.1
                Cluster cluster = Cluster.Builder().AddContactPoint("127.0.0.1").Build();
                ISession session = cluster.Connect("thingstodo");

                ////Prepare a statement once
                var ps = session.Prepare("INSERT INTO Log (ID, Response, Request, Exception, Date) VALUES (?,?,?,?,?)");

                ////...bind different parameters every time you need to execute
                var statement = ps.Bind(Guid.NewGuid(), Log.Request, Log.Response, Log.Exception, Log.TimeStamp);
                ////Execute the bound statement with the provided parameters
                session.Execute(statement);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
    }
}
