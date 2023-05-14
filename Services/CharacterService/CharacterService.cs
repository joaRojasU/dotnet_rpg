using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_rpg.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        private static List<Character> characters = new List<Character>{
        new Character(),
        new Character{ Id= 1, Name = "Andrew" }
        };
        public async Task<ServiceResponse<List<GetCharacterResponseDTO>>> AddCharacter(AddCharacterRequestDTO newCharacter)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterResponseDTO>>();
            characters.Add(newCharacter);
            serviceResponse.Data = characters;

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterResponseDTO>>> GetAllCharacters()
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterResponseDTO>>();
            serviceResponse.Data = characters;
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterResponseDTO>> GetCharacterById(int id)
        { 
            var serviceResponse = new ServiceResponse<Character>();
            var character = characters.FirstOrDefault(c => c.Id == id);
            serviceResponse.Data = character;
            return serviceResponse;
            // OBSOLETO
            /* if (character is not null)
                return character;

            throw new Exception("Character not found"); */
        }
    }
}