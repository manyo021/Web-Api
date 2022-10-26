using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CharacterService(IMapper mapper, DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }


        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext.User
        .FindFirstValue(ClaimTypes.NameIdentifier));
        public async Task<ServiceResponse<List<GetCharacterDTO>>> AddCharacter(AddCharacterDTO newCharacter)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDTO>>();
            Character character = _mapper.Map<Character>(newCharacter);
            character.User = await _context.Users.FirstOrDefaultAsync(u => u.Id == GetUserId());

            _context.Characters.Add(character);
            await _context.SaveChangesAsync();
            serviceResponse.Data = await _context.Characters
            .Where(c => c.User.Id == GetUserId())
            .Select(c => _mapper.Map<GetCharacterDTO>(c))
            .ToListAsync();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDTO>>> DeleteCharacter(int id)
        {
            ServiceResponse<List<GetCharacterDTO>> response = new ServiceResponse<List<GetCharacterDTO>>();

            try
            {
                Character character = await _context.Characters
                .FirstOrDefaultAsync(x => x.Id == id && x.User.Id == GetUserId());
                if (character != null)
                {
                    _context.Characters.Remove(character);

                    await _context.SaveChangesAsync();
                    response.Data = _context.Characters
                    .Where(c => c.User.Id == GetUserId())
                    .Select(c => _mapper.Map<GetCharacterDTO>(c)).ToList();
                }
                else
                {
                    response.Success = false;
                    response.Message = "Character not found";
                }




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

        public async Task<ServiceResponse<List<GetCharacterDTO>>> GetAllCharacter()
        {

            var response = new ServiceResponse<List<GetCharacterDTO>>();
            var dbCharacter = await _context.Characters
            .Where(c => c.User.Id == GetUserId())
            .ToListAsync();
            response.Data = dbCharacter.Select(c => _mapper.Map<GetCharacterDTO>(c)).ToList();

            return response;

        }



        public async Task<ServiceResponse<GetCharacterDTO>> GetCharacterById(int id)
        {


            var serviceResponse = new ServiceResponse<GetCharacterDTO>();
            var dbcharacter = await _context.Characters
            .FirstOrDefaultAsync(x => x.Id == id && x.User.Id == GetUserId());
            serviceResponse.Data = _mapper.Map<GetCharacterDTO>(dbcharacter);
            return serviceResponse;

        }

        public async Task<ServiceResponse<GetCharacterDTO>> UpdateCharacter(UpdateCharacterDTO updateCharacter)
        {

            ServiceResponse<GetCharacterDTO> response = new ServiceResponse<GetCharacterDTO>();

            try
            {
                var character = await _context.Characters
                .Include(c => c.User)
                .FirstOrDefaultAsync(x => x.Id == updateCharacter.Id);
                if (character.User.Id == GetUserId())
                {
                    //_mapper.Map(updateCharacter, character);

                    character.Name = updateCharacter.Name;
                    character.HitPoints = updateCharacter.HitPoints;
                    character.Strength = updateCharacter.Strength;
                    character.Defense = updateCharacter.Defense;
                    character.Intelligence = updateCharacter.Intelligence;
                    character.Class = updateCharacter.Class;


                    await _context.SaveChangesAsync();
                    response.Data = _mapper.Map<GetCharacterDTO>(character);
                }
                else
                {
                    response.Success = false;
                    response.Message = "Character not found";
                }




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