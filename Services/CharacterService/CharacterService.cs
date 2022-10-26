using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Web_Api.Data;
using Web_Api.DTOs.Character;
using Web_Api.Models;

namespace Web_Api.Services.CharacterService
{

    public class CharacterService : ICharacterService
    {


        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public CharacterService(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task<ServiceResponse<List<GetCharacterDTO>>> AddCharacter(AddCharacterDTO newCharacter)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDTO>>();
            Character character = _mapper.Map<Character>(newCharacter);
            _context.Characters.Add(character);
            await _context.SaveChangesAsync();
            serviceResponse.Data = await _context.Characters
            .Select(c => _mapper.Map<GetCharacterDTO>(c))
            .ToListAsync();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDTO>>> DeleteCharacter(int id)
        {
            ServiceResponse<List<GetCharacterDTO>> response = new ServiceResponse<List<GetCharacterDTO>>();

            try
            {
                Character character = await _context.Characters.FirstAsync(x => x.Id == id);

                _context.Characters.Remove(character);

                await _context.SaveChangesAsync();

                response.Data = _context.Characters.Select(c => _mapper.Map<GetCharacterDTO>(c)).ToList();

                // character.Name = updateCharacter.Name;
                // character.HitPoints = updateCharacter.HitPoints;
                // character.Strength = updateCharacter.Strength;
                // character.Defense = updateCharacter.Defense;
                // character.Intelligence = updateCharacter.Intelligence;
                // character.Class = updateCharacter.Class;
                // response.Data = _mapper.Map<GetCharacterDTO>(character);


            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;

        }

        public async Task<ServiceResponse<List<GetCharacterDTO>>> GetAllCharacter(int userId)
        {

            var response = new ServiceResponse<List<GetCharacterDTO>>();
            var dbCharacter = await _context.Characters
            .Where(c => c.User.Id == userId)
            .ToListAsync();
            response.Data = dbCharacter.Select(c => _mapper.Map<GetCharacterDTO>(c)).ToList();

            return response;

        }



        public async Task<ServiceResponse<GetCharacterDTO>> GetCharacterById(int id)
        {


            var serviceResponse = new ServiceResponse<GetCharacterDTO>();
            var dbcharacter = await _context.Characters.FirstOrDefaultAsync(x => x.Id == id);
            serviceResponse.Data = _mapper.Map<GetCharacterDTO>(dbcharacter);
            return serviceResponse;

        }

        public async Task<ServiceResponse<GetCharacterDTO>> UpdateCharacter(UpdateCharacterDTO updateCharacter)
        {

            ServiceResponse<GetCharacterDTO> response = new ServiceResponse<GetCharacterDTO>();

            try
            {
                var character = await _context.Characters
                .FirstOrDefaultAsync(x => x.Id == updateCharacter.Id);

                _mapper.Map(updateCharacter, character);

                // character.Name = updateCharacter.Name;
                // character.HitPoints = updateCharacter.HitPoints;
                // character.Strength = updateCharacter.Strength;
                // character.Defense = updateCharacter.Defense;
                // character.Intelligence = updateCharacter.Intelligence;
                // character.Class = updateCharacter.Class;
                // response.Data = _mapper.Map<GetCharacterDTO>(character);

                await _context.SaveChangesAsync();


            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;


        }
    }
}