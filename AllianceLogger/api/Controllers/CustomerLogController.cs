using Logging.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace Logging.API.Controllers
{
    [Route("api/[controller]")]
    public class CustomerLogController : ControllerBase
    {
        private CustomerLogRepository _logRepo = new CustomerLogRepository();
        private ILogger<CustomerLogController> _logger;

        public CustomerLogController(ILogger<CustomerLogController> logger)
        {
            _logger = logger;
        }

        [HttpPost("SaveCustomerLog")]
        public IActionResult SaveCustomerLog([FromBody] CustomerLog model)
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

        [HttpGet("GetCustomerLog")]
        public IActionResult GetCustomerLog(int logId)
        {
            try
            {
                CustomerLog log = _logRepo.GetModel(logId);
                return Ok(log);
            }

            catch (Exception ex)
            {

                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetAllCustomerLogs")]
        public IActionResult GetAllCustomerLogs([FromBody] CustomerLog model)
        {
            try
            {
                List<CustomerLog> logs = _logRepo.GetModels();
                return Ok(logs);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteCustomerLog")]
        public IActionResult DeleteCustomerLog(int logId)
        {
            try
            {
                _logRepo.DeleteModel(logId);
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
