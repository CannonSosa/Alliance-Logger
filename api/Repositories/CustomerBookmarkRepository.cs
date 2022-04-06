using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logging.API.Repositories
{
    public class CustomerBookmarkRepository
    {
        public Boolean SaveModel(CustomerBookmark bookmark)
        {
            try
            {
                using (RemoteLoggerContext dbContext = new RemoteLoggerContext())
                {
                    dbContext.CustomerBookmarks.Attach(bookmark);

                    if (bookmark.BookmarkID == 0)
                    {
                        dbContext.Entry(bookmark).State = EntityState.Added;
                    }
                    else
                    {
                        dbContext.Entry(bookmark).State = EntityState.Modified;
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

        public Boolean DeleteModel(int bookmarkId)
        {
            CustomerBookmark bookmark;

            try
            {
                using (RemoteLoggerContext dbContext = new RemoteLoggerContext())
                {
                    bookmark = dbContext.CustomerBookmarks.FirstOrDefault(x => x.BookmarkID == bookmarkId);
                    dbContext.Remove(bookmark);
                    dbContext.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                //TODO:Logging
                return false;
            }
        }
    }
}
