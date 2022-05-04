using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logging.API.Repositories
{
    public class DatabaseLogsRepository
    {
        public DatabaseLogs GetModel(int logID)
        {
            DatabaseLogs log;

            using (RemoteLoggerContext dbContext = new RemoteLoggerContext())
            {
                log = dbContext.DatabaseLogs.FirstOrDefault(x => x.LogID == logID);
            }

            return log;
        }

        public List<DatabaseLogs> GetModels()
        {
            List<DatabaseLogs> logs;

            using (RemoteLoggerContext dbContext = new RemoteLoggerContext())
            {
                logs = dbContext.DatabaseLogs.ToList();
            }
            return logs;
        }

        public Boolean SaveModel(DatabaseLogs log)
        {
            try
            {
                using (RemoteLoggerContext dbContext = new RemoteLoggerContext())
                {
                    dbContext.DatabaseLogs.Attach(log);

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

            catch (Exception ex)
            {
                return false;
            }
        }

        public Boolean DeleteModel(int logID)
        {
            DatabaseLogs log;

            try
            {
                using (RemoteLoggerContext dbContext = new RemoteLoggerContext())
                {
                    log = dbContext.DatabaseLogs.FirstOrDefault(x => x.LogID == logID);
                    dbContext.Remove(log);
                    dbContext.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public Boolean ModifyNote(DatabaseLogs log)
        {
            try
            {
                using (RemoteLoggerContext dbContext = new RemoteLoggerContext())
                {
                    dbContext.DatabaseLogs.Update(log);
                    dbContext.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
