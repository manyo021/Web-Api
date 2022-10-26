using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web_Api.DTOs.Character;
using Web_Api.DTOs.Weapon;
using Web_Api.Models;

namespace Web_Api.Services.WeaponService
{
    public interface IWeaponService
    {
        Task<ServiceResponse<GetCharacterDTO>> AddWeapon(AddWeaponDto newWeapon);

    }
}