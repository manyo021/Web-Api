using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Web_Api.DTOs.Character;
using Web_Api.Models;
using Web_Api.Services.CharacterService;

namespace Web_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class CharacterController : ControllerBase
    {
        private readonly ICharacterService _characterService;

        public CharacterController(ICharacterService characterService)
        {
            _characterService = characterService;

        }

        //Allows us to send specific HTTP status quotes back to client


        [HttpGet("Get All")]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDTO>>>> Get()
        {
            return Ok(await _characterService.GetAllCharacter());
        }

        [HttpGet("{id}")]



        public async Task<ActionResult<ServiceResponse<GetCharacterDTO>>> getCharacter(int id)
        {
            //FirstOrDefault gets the first character or default, if you put a parameter can grab based on input
            return Ok(await _characterService.GetCharacterById(id));
        }



        [HttpPost]

        public async Task<ActionResult<ServiceResponse<List<GetCharacterDTO>>>> addCharacter(AddCharacterDTO newCharacter)
        {
            var response = await _characterService.AddCharacter(newCharacter);
            if (response.Data == null)
            {
                return NotFound(response);

            }
            return Ok(response);
        }


        [HttpPut]

        public async Task<ActionResult<ServiceResponse<GetCharacterDTO>>> UpdateCharacter(UpdateCharacterDTO UpdatedCharacter)
        {
            return Ok(await _characterService.UpdateCharacter(UpdatedCharacter));
        }



        [HttpDelete("{id}")]



        public async Task<ActionResult<ServiceResponse<List<GetCharacterDTO>>>> deleteCharacter(int id)
        {
            //FirstOrDefault gets the first character or default, if you put a parameter can grab based on input
            var response = await _characterService.DeleteCharacter(id);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }



    }
}