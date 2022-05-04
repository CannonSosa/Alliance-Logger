using Logging.API.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace Logging.API.Controllers
{
    [Route("api/[controller]")]
    public class BookmarkController : ControllerBase
    {
        private BookmarkRepository _logRepo = new BookmarkRepository();
        private ILogger<BookmarkController> _logger;

        public BookmarkController(ILogger<BookmarkController> logger)
        {
            _logger = logger;
        }

    
        [HttpPost("AddBookmark")]
        public IActionResult AddBookmark([FromBody] Bookmark model)
        {
            try
            {
                _logRepo.SaveModel(model);
                return Ok(model);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteBookmark")]
        public IActionResult DeleteBookmark(int bookmarkId)
        {
            try
            {
                _logRepo.DeleteModel(bookmarkId);
                return Ok(true);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
