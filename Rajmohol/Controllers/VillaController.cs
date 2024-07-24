using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rajmohol.Models;
using Rajmohol.Models.DTOs.Villa;
using Rajmohol.Repository.IRepository;
using System.Net;

namespace Rajmohol.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaController : ControllerBase
    {
        private readonly IVillaRepository _villaDb;
        private readonly IMapper _mapper;
        protected readonly APIRespone _response;
        public VillaController(IVillaRepository villaDb, IMapper mapper)
        {
            _villaDb = villaDb;
            _mapper = mapper;
            this._response = new();

        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIRespone>> GetVillas()
        {
            try
            {
                IEnumerable<Villa> villas = await _villaDb.GetAllAsync();
                _response.Result = _mapper.Map<List<VillaDTO>>(villas);
                _response.IsSuccess = true;
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);

            }
            catch (Exception ex)
            {

                _response.ErrorMessage = new List<string> { ex.ToString() };

            }
            return _response;

        }



        [HttpGet("id", Name = "GetVilla")]
        public async Task<ActionResult<APIRespone>> GetVilla(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest("Invaild Id");
                }

                var villa = await _villaDb.GetAsync(u => u.Id == id);
                if (villa == null)
                {
                    return NotFound();
                }
                _response.Result = _mapper.Map<VillaDTO>(villa);
                _response.IsSuccess = true;
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.ErrorMessage = new List<string> { ex.ToString() };

            }
            return _response;

        }

        [HttpPost]
        public async Task<ActionResult<APIRespone>> CreateVilla([FromBody] VillaCreateDTO createDTO)
        {
            try
            {
                if (createDTO == null)
                {
                    return BadRequest();
                }

                Villa villa = _mapper.Map<Villa>(createDTO);

                await _villaDb.CreateAsync(villa);

                _response.Result = _mapper.Map<VillaDTO>(villa);
                _response.IsSuccess = true;
                _response.StatusCode = HttpStatusCode.Created;
                return CreatedAtRoute(nameof(GetVilla), new { id = villa.Id }, _response);
            }
            catch (Exception ex)
            {

                _response.ErrorMessage = new List<string> { ex.ToString() };
            }
            return _response;

        }

        [HttpPut]
        public async Task<ActionResult<APIRespone>> UpdateVilla(int id, [FromBody] VillaUpdateDTO updateDTO)
        {
            try
            {
                if (id == 0 || id != updateDTO.Id)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                
                Villa villa = _mapper.Map<Villa>(updateDTO);
                await _villaDb.UpdateAsync(villa);

                _response.IsSuccess = true;
                _response.StatusCode = HttpStatusCode.NoContent;

                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.ErrorMessage = new List<string> { ex.ToString() };
            }
            return _response;


        }

        [HttpDelete("id")]
        public async Task<ActionResult<APIRespone>> DeleteVilla(int id)
        {
            try
            {
                if(id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

               

                Villa villa = await _villaDb.GetAsync(u => u.Id ==id);
                await _villaDb.Remove(villa);
                _response.IsSuccess = true;
                _response.StatusCode = HttpStatusCode.NoContent;
                return Ok(_response);
               

            }
            catch (Exception ex)
            {
                _response.ErrorMessage = new List<string> { ex.ToString() };
            }
            return _response;

        }
    }
}
