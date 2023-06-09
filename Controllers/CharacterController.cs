using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;


namespace dotnet_rpg.Controllers
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

        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterResponseDTO>>>> Get()
        {
            return Ok(await _characterService.GetAllCharacters());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<Character>>> GetSingle(int id)
        {
            return Ok(await _characterService.GetCharacterById(id));
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterResponseDTO>>>> AddCharacter(AddCharacterRequestDTO newChar)
        {
            return Ok(await _characterService.AddCharacter(newChar));
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterResponseDTO>>>> UpdateCharacter(UpdateCharacterRequestDTO updateChar)
        {
            var response = await _characterService.UpdateCharacter(updateChar);
            if (response.Data is null){
                return NotFound(response);
            }
            return Ok(response);

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<Character>>> DeleteCharacter(int id)
        {
            var response = await _characterService.DeleteCharacter(id);
            if (response.Data is null){
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}