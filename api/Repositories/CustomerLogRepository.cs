using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logging.API.Repositories
{
    public class CustomerLogRepository
    {
        public CustomerLog GetModel(int logID)
        {
            CustomerLog log;

            using (RemoteLoggerContext dbContext = new RemoteLoggerContext())
            {
                log = dbContext.CustomerLogs.FirstOrDefault(x => x.LogID == logID);
            }

            return log;
        }

        public List<CustomerLog> GetModels()
        {
            List<CustomerLog> logs;

            using (RemoteLoggerContext dbContext = new RemoteLoggerContext())
            {
                logs = dbContext.CustomerLogs.ToList();
            }

            return logs;
        }

        public Boolean SaveModel(CustomerLog log)
        {
            try
            {
                using (RemoteLoggerContext dbContext = new RemoteLoggerContext())
                {
                    dbContext.CustomerLogs.Attach(log);

                    if (log.LogID == 0)
                    {
                        dbContext.Entry(log).State = EntityState.Added;
                    }
                    else
                    {
                        dbContext.Entry(log).State = EntityState.Modified;
                    }

                    dbContext.SaveChanges();

                    return true;
                }
            }
            catch(Exception ex)
            {
              
                return false;
            }
            
        }

        public Boolean DeleteModel(int logID)
        {
            CustomerLog log;

            try
            {
                using (RemoteLoggerContext dbContext = new RemoteLoggerContext())
                {
                    log = dbContext.CustomerLogs.FirstOrDefault(x => x.LogID == logID);
                    dbContext.Remove(log);
                    dbContext.SaveChanges();
                    return true;
                }
            }
            catch(Exception ex)
            {
                //TODO:Logging
                return false;
            }
        }
    }
}
