using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DIS_Server.DTO;
using DIS_Server.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DIS_Server.Controllers
{
    public class HistoryController:Controller
    {
        protected readonly IMapper Mapper;
        protected readonly IHistoryService Service;

        public HistoryController(IHistoryService historyService, IMapper mapper)
        {
            Mapper = mapper;
            Service = historyService;
        }

        [HttpGet("/get")]
      //  [Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<HistoryDto>> Get([FromQuery]Guid id)
        {
            var model = await Service.Get(id);
            if (model == null) return NotFound();
            return Mapper.Map<HistoryDto>(model);
        }
      [HttpGet("/getbylogintime")]
      //  [Authorize(Roles = "admin")]
      [ProducesResponseType(StatusCodes.Status401Unauthorized)]
      [ProducesResponseType(StatusCodes.Status404NotFound)]
      [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<HistoryDto>> Get([FromQuery] string login, [FromQuery] DateTime time)
        {
            var model = await Service.Get(login, time);
            if (model == null) return NotFound();
            return Mapper.Map<HistoryDto>(model);
        }

      [HttpGet("/listlogin")]
      //  [Authorize(Roles = "admin")]
      [ProducesResponseType(StatusCodes.Status401Unauthorized)]
      [ProducesResponseType(StatusCodes.Status404NotFound)]
      [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<HistoryDto>>> GetList([FromQuery] string login)
        {
            var model = await Service.GetList(login);
            if (model == null) return NotFound();
            return Mapper.Map<List<HistoryDto>>(model);
        }

      [HttpGet("/listtime")]
      //  [Authorize(Roles = "admin")]
      [ProducesResponseType(StatusCodes.Status401Unauthorized)]
      [ProducesResponseType(StatusCodes.Status404NotFound)]
      [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<HistoryDto>>> GetList([FromQuery] DateTime time)
        {
            var model = await Service.GetList(time);
            if (model == null) return NotFound();
            return Mapper.Map<List<HistoryDto>>(model);
        }

      [HttpGet("/list")]
      //  [Authorize(Roles = "admin")]
      [ProducesResponseType(StatusCodes.Status401Unauthorized)]
      [ProducesResponseType(StatusCodes.Status404NotFound)]
      [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<HistoryDto>>> GetList()
        {
            var model = await Service.GetList();
            if (model == null) return NotFound();
            return Mapper.Map<List<HistoryDto>>(model);
        }
    }
}
