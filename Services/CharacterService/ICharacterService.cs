using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_rpg.Services.CharacterService
{
    public interface ICharacterService
    {
        Task<ServiceResponse<List<GetCharacterResponseDTO>>> GetAllCharacters();
        Task<ServiceResponse<GetCharacterResponseDTO>> GetCharacterById(int id);
        Task<ServiceResponse<List<GetCharacterResponseDTO>>> AddCharacter(AddCharacterRequestDTO newCharacter);
        Task<ServiceResponse<GetCharacterResponseDTO>> UpdateCharacter(UpdateCharacterRequestDTO updateCharacter);
        Task<ServiceResponse<List<GetCharacterResponseDTO>>> DeleteCharacter(int id);
    }
}