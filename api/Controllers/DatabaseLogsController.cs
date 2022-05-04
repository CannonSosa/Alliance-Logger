using Logging.API.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;

namespace Logging.API.Controllers
{
    [Route("api/[controller]")]
    public class DatabaseLogsController : ControllerBase
    {
        private DatabaseLogsRepository _logRepo = new DatabaseLogsRepository();
        private ILogger<DatabaseLogsController> _logger;

        public DatabaseLogsController(ILogger<DatabaseLogsController> logger)
        {
            _logger = logger;
        }

        [Authorize(AuthenticationSchemes = "JwtBearer", Roles = "User, Admin")]
        [HttpPost("SaveDatabaseLog")]
        public IActionResult SaveDatabaseLog([FromBody] DatabaseLogs model)
        {
            _logger.LogInformation("Save Database Log Endpoint Hit");

            try
            {
                _logRepo.SaveModel(model);
                return Ok(model);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [Authorize(AuthenticationSchemes = "JwtBearer", Roles = "User, Admin")]
        [HttpGet("GetDatabaseLog")]
        public IActionResult GetDatabaseLog(int logId)
        {
            _logger.LogInformation("Get Database Log Endpoint Hit");

            try
            {
                DatabaseLogs log = _logRepo.GetModel(logId);
                return Ok(log);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [Authorize(AuthenticationSchemes = "JwtBearer", Roles = "User, Admin")]
        [HttpGet("GetAllDatabaseLogs")]
        public IActionResult GetAllDatabaseLogs([FromBody] DatabaseLogs model)
        {
            _logger.LogInformation("Get All Database Logs Endpoint Hit");

            try
            {
                List<DatabaseLogs> logs = _logRepo.GetModels();
                return Ok(logs);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [Authorize(AuthenticationSchemes = "JwtBearer", Roles = "Admin")]
        [HttpDelete("DeleteDatabaseLog")]
        public IActionResult DeleteDatabaseLog(int logId)
        {
            _logger.LogInformation("Delete Database Log Endpoint Hit");

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

        [Authorize(AuthenticationSchemes = "JwtBearer", Roles = "User, Admin")]
        [HttpPost("ModifyDatabaseLogNote")]
        public IActionResult ModifyDatabaseLogNote([FromBody] DatabaseLogs model)
        {
            _logger.LogInformation("Modify Database Log Note Endpoint Hit");

            try
            {
                _logRepo.ModifyNote(model);
                return Ok(model);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
