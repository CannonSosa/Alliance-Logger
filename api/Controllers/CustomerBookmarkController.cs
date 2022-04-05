using Logging.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace Logging.API.Controllers
{
    [Route("api/[controller]")]
    public class CustomerBookmarkController : ControllerBase
    {
        private CustomerBookmarkRepository _logRepo = new CustomerBookmarkRepository();
        private ILogger<CustomerBookmarkController> _logger;

        public CustomerBookmarkController(ILogger<CustomerBookmarkController> logger)
        {
            _logger = logger;
        }

        [HttpPost("AddCustomerBookmark")]
        public IActionResult AddCustomerBookmark([FromBody] CustomerBookmark model)
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

        [HttpDelete("DeleteCustomerBookmark")]
        public IActionResult DeleteCustomerBookmark(int bookmarkId)
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
