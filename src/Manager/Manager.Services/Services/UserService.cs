using AutoMapper;
using Manager.Core.Exceptions;
using Manager.Domain.Entities;
using Manager.Infra.Interfaces;
using Manager.Services.Dtos;
using Manager.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Manager.Services.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public UserService(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<UserDto> Create(UserDto dto)
        {
            var userExists = await _userRepository.GetByEmailAsync(dto.Email);

            if (userExists is not null)
                throw new DomainException("Email in use");

            var user = _mapper.Map<User>(dto);

            user.Validate();

            var userCreated = await _userRepository.CreateAsync(user);

            return _mapper.Map<UserDto>(userCreated);
        }

        public async Task<UserDto> Update(UserDto dto)
        {
            var userExists = await _userRepository.GetAsNoTrackingAsync(x => x.Id == dto.Id);

            if (userExists is null)
                throw new DomainException("User not found");

            var user = _mapper.Map<User>(dto);

            user.Validate();

            var userUpdated = await _userRepository.UpdateAsync(user);

            return _mapper.Map<UserDto>(userUpdated);
        }

        public async Task Remove(long id)
        {
            await _userRepository.RemoveAsync(id);
        }

        public async Task<UserDto> Get(long id)
        {
            var user = await _userRepository.GetAsync(id);

            return _mapper.Map<UserDto>(user);
        }

        public async Task<ICollection<UserDto>> Get()
        {
            var users = await _userRepository.GetAsync();

            return _mapper.Map<ICollection<UserDto>>(users);
        }

        public async Task<UserDto> GetByEmailAsync(string email)
        {
            var user = await _userRepository.GetByEmailAsync(email);

            return _mapper.Map<UserDto>(user);
        }

        public async Task<ICollection<UserDto>> SearchByEmailAsync(string email)
        {
            var users = await _userRepository.SearchByEmailAsync(email);

            return _mapper.Map<ICollection<UserDto>>(users);
        }

        public async Task<ICollection<UserDto>> SearchByNameAsync(string name)
        {
            var users = await _userRepository.SearchByNameAsync(name);

            return _mapper.Map<ICollection<UserDto>>(users);
        }
    }
}
