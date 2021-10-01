using Manager.Services.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Manager.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> Create(UserDto user);
        Task<UserDto> Update(UserDto user);
        Task Remove(long id);
        Task<UserDto> Get(long id);
        Task<ICollection<UserDto>> Get();
        Task<UserDto> GetByEmailAsync(string email);
        Task<ICollection<UserDto>> SearchByNameAsync(string name);
        Task<ICollection<UserDto>> SearchByEmailAsync(string email);
    }
}
