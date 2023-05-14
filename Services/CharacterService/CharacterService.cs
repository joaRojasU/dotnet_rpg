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

        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public CharacterService(IMapper mapper, DataContext context)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<List<GetCharacterResponseDTO>>> AddCharacter(AddCharacterRequestDTO newCharacter)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterResponseDTO>>();
            var character = _mapper.Map<Character>(newCharacter);
            character.Id = characters.Max(c => c.Id) + 1;
            characters.Add(character);
            serviceResponse.Data = characters.Select(c => _mapper.Map<GetCharacterResponseDTO>(c)).ToList();

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterResponseDTO>>> DeleteCharacter(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterResponseDTO>>();
            try {
            var character = characters.First(c => c.Id == id);
            if(character is null)
                throw new Exception($"Character with Id '{id}' not found");

            characters.Remove(character);

            serviceResponse.Data = characters.Select(c => _mapper.Map<GetCharacterResponseDTO>(c)).ToList();
            }
            catch(Exception ex){
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;

            }
            
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterResponseDTO>>> GetAllCharacters()
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterResponseDTO>>();
            var dbCharacters = await _context.Characters.ToListAsync();
            serviceResponse.Data = dbCharacters.Select(c => _mapper.Map<GetCharacterResponseDTO>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterResponseDTO>> GetCharacterById(int id)
        { 
            var serviceResponse = new ServiceResponse<GetCharacterResponseDTO>();
            var dbCharacter = await _context.Characters.FirstOrDefaultAsync(c => c.Id == id);
            serviceResponse.Data = _mapper.Map<GetCharacterResponseDTO>(dbCharacter);
            return serviceResponse;
            // OBSOLETO
            /* if (character is not null)
                return character;

            throw new Exception("Character not found"); */
        }

        public async Task<ServiceResponse<GetCharacterResponseDTO>> UpdateCharacter(UpdateCharacterRequestDTO updateCharacter)
        {
            var serviceResponse = new ServiceResponse<GetCharacterResponseDTO>();
            try {
            var character = characters.FirstOrDefault(c => c.Id == updateCharacter.Id);
            if(character is null)
                throw new Exception($"Character with Id '{updateCharacter.Id}' not found");

            character.Name = updateCharacter.Name;
            character.HitPoints= updateCharacter.HitPoints;
            character.Strength= updateCharacter.Strength;
            character.Defense= updateCharacter.Defense;
            character.Intelligence= updateCharacter.Intelligence;
            character.Class=updateCharacter.Class;

            serviceResponse.Data = _mapper.Map<GetCharacterResponseDTO>(character);
            }
            catch(Exception ex){
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;

            }
            
            return serviceResponse;
        }
    }
}