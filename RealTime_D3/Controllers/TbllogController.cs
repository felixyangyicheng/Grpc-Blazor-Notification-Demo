using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealTime_D3.Contracts;
using RealTime_D3.Dtos;
using RealTime_D3.Models;

namespace RealTime_D3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TbllogController : ControllerBase
    {
        private readonly ILogger<TbllogController> _logger;
        private readonly ITbllogRepository _TbllogRepository;
        private readonly IRealtimeLogRepository _realtime;

        private readonly IMapper _mapper;
        //private readonly IEmailSender _emailSender;

        public TbllogController(ILogger<TbllogController> logger, ITbllogRepository TbllogRepository, IRealtimeLogRepository realtime , IMapper mapper)
        //, IEmailSender emailSender)
        {
            _TbllogRepository = TbllogRepository;
            _realtime = realtime;
            _mapper = mapper;
            _logger = logger;
            //_emailSender = emailSender;
        }

        [HttpGet("realtime")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetLogRealtime()
        {
            try
            {
                await _realtime.GetLastLog();
                
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetLogRealtime)}");
                //var message = new Message(new string[] { "yicheng.yang@ermo-tech.com", "ermo.automation@ermo-tech.com" }, "API Exception ", $"Something went wrong in the {nameof(GetGroupedStatusByTimeRange)}" + ex.ToString());
                //await _emailSender.SendEmailAsync(message);
                return Problem($"Something went wrong in the {nameof(GetLogRealtime)}", statusCode: 500);
            }
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetLog(int id)
        {
            try
            {
                var result = await _TbllogRepository.GetAsync(id);
                var response = _mapper.Map<TbllogDto>(result);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetLog)}");
                //var message = new Message(new string[] { "yicheng.yang@ermo-tech.com", "ermo.automation@ermo-tech.com" }, "API Exception ", $"Something went wrong in the {nameof(GetGroupedStatusByTimeRange)}" + ex.ToString());
                //await _emailSender.SendEmailAsync(message);
                return Problem($"Something went wrong in the {nameof(GetLog)}", statusCode: 500);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<TbllogCreateDto>> PostLog(TbllogCreateDto dto)
        {
            try
            {
                var log = _mapper.Map<Tbllog>(dto);
                await _TbllogRepository.AddAsync(log);

                return CreatedAtAction(nameof(PostLog), new { id = log.Id }, log);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error Performing POST in {nameof(PostLog)}", dto);
                return Problem($"Something went wrong in the {nameof(PostLog)}", statusCode: 500);

            }

        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PutLog(int id, TbllogDto dto)
        {
            if (id != dto.Id)
            {
                _logger.LogWarning($"Update ID invalid in {nameof(PutLog)} - ID: {id}");
                return BadRequest();
            }

            var log = await _TbllogRepository.GetAsync(id);

            if (log == null)
            {
                _logger.LogWarning($"{nameof(log)} record not found in {nameof(PutLog)} - ID: {id}");
                return NotFound();
            }

            _mapper.Map(dto, log);

            try
            {
                await _TbllogRepository.UpdateAsync(log);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!await _TbllogRepository.Exists(id))
                {
                    return NotFound();
                }
                else
                {
                    _logger.LogError(ex, $"Error Performing GET in {nameof(PutLog)}");
                    return Problem($"Something went wrong in the {nameof(PutLog)}", statusCode: 500);

                }
            }

            return NoContent();
        }
    }
}
