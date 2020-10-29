using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using AutoMapper;
using CoreCodeCamp.Data;
using CoreCodeCamp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;

namespace CoreCodeCamp.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CampsController : ControllerBase
	{
		private readonly ICampRepository _repository;
		private readonly IMapper _mapper;

		public CampsController(ICampRepository repository, IMapper mapper)
		{
			this._repository = repository;
			this._mapper = mapper;
		}

		[HttpGet]
		public async Task<IActionResult> GetAllCamps(bool includeTalks = false)
		{
			try
			{
				var results = await _repository.GetAllCampsAsync(includeTalks: includeTalks);

				CampModel[] models = _mapper.Map<CampModel[]>(results);

				return this.Ok(models);

			}
			catch (Exception ex)
			{

				return this.StatusCode(StatusCodes.Status500InternalServerError, ex.Message.ToString());
			}
		}

		//nieuwe wijze van implementatie
		[HttpGet("{moniker}")]
		public async Task<ActionResult<CampModel>> GetCamp(string moniker)
		{
			try
			{
				var result = await _repository.GetCampAsync(moniker);

				if (result == null) return NotFound();

				return _mapper.Map<CampModel>(result);
			}
			catch (Exception)
			{
				return this.StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
			}
		}

		[HttpGet("search")]
		public async Task<ActionResult<CampModel[]>> SearchByDate(DateTime theDate, bool includeTalks = false)
		{
			try
			{
				var results = await _repository.GetAllCampsByEventDate(theDate, includeTalks);

				if (!results.Any())
				{
					return this.NotFound();
				}

				return _mapper.Map<CampModel[]>(results);
			}
			catch (Exception)
			{
				return this.StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
			}
		}
	}
}
