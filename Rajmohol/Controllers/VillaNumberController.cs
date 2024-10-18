using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Rajmohol.Models;
using Rajmohol.Models.DTOs.Villa;
using Rajmohol.Models.DTOs.VillaNumber;
using Rajmohol.Repository.IRepository;
using System.Net;

namespace Rajmohol.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaNumberController : ControllerBase
    {
        private readonly IVillaNumberRepository _villaNumberDb;
        private readonly IVillaRepository _villaDb;
        private readonly IMapper _mapper;
        protected readonly APIRespone _response;
        public VillaNumberController(IVillaNumberRepository villaNumberDb, IVillaRepository villaDb, IMapper mapper)
        {
            _villaNumberDb = villaNumberDb;
            _villaDb = villaDb;
            _mapper = mapper;
            this._response = new();

        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIRespone>> GetVillaNumber()
        {
            try
            {
                IEnumerable<VillaNumber> villaNumbers = await _villaNumberDb.GetAllAsync(includeProperties: "Villa");
                _response.Result = _mapper.Map<List<VillaNumberDTO>>(villaNumbers);
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



        [HttpGet("id", Name = "GetVillaNumber")]
        public async Task<ActionResult<APIRespone>> GetVillaNumber(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest("Invaild Id");
                }

                var villaNumber = await _villaNumberDb.GetAsync(u => u.VillaNo == id, includeProperties: "Villa");
                if (villaNumber == null)
                {
                    return NotFound();
                }
                _response.Result = _mapper.Map<VillaNumberDTO>(villaNumber);
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
        public async Task<ActionResult<APIRespone>> CreateVilla([FromBody] VillaNumberCreateDTO createDTO)
        {
            try
            {
               
                if (createDTO == null || createDTO.VillaNo < 1)
                {
                    return BadRequest();
                }

                if( await _villaDb.GetAsync(u=> u.Id == createDTO.VillaId) == null)
                {
                    ModelState.AddModelError("CustomeError", "Invalid Villa Id");
                    return BadRequest();
                }

                if( await _villaNumberDb.GetAsync(u=> u.VillaNo == createDTO.VillaNo) != null)
                {
                    ModelState.AddModelError("CustomeError","Villa Already Exits!");
                    return BadRequest(ModelState);
                }

                VillaNumber villa = _mapper.Map<VillaNumber>(createDTO);

                await _villaNumberDb.CreateAsync(villa);

                _response.Result = _mapper.Map<VillaNumberDTO>(villa);
                _response.IsSuccess = true;
                _response.StatusCode = HttpStatusCode.Created;
                return CreatedAtRoute(nameof(GetVillaNumber), new { id = villa.VillaNo }, _response);
            }
            catch (Exception ex)
            {

                _response.ErrorMessage = new List<string> { ex.ToString() };
            }
            return _response;

        }

        [HttpPut]
        public async Task<ActionResult<APIRespone>> UpdateVillaNumber(int id, [FromBody] VillaNumberUpdateDTO updateDTO)
        {
            try
            {
                if (id == 0 || id != updateDTO.VillaNo)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }


                if (await _villaDb.GetAsync(u => u.Id == updateDTO.VillaId) == null)
                {
                    ModelState.AddModelError("CustomeError", "Invalid Villa Id");
                    return BadRequest();
                }

                VillaNumber villaNumber = _mapper.Map<VillaNumber>(updateDTO);
                await _villaNumberDb.UpdateAsync(villaNumber);

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
        public async Task<ActionResult<APIRespone>> DeleteVillaNumber(int id)
        {
            try
            {
                if(id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

               

                VillaNumber villaNumber = await _villaNumberDb.GetAsync(u => u.VillaNo ==id);
                await _villaNumberDb.Remove(villaNumber);
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
