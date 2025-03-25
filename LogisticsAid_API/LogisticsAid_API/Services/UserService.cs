using System.Globalization;
using AutoMapper;
using LogisticsAid_API.Repositories;
using LogisticsAid_API.DTOs;
using LogisticsAid_API.Entities;
using LogisticsAid_API.Repositories.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace LogisticsAid_API.Services;

public class UserService
{
    private readonly ILogisticianRepository _logisticianRepository;
    private readonly IContactInfoRepository _contactInfoRepository;
    private readonly PasswordService _passwordService;
    private readonly IMapper _mapper;

    public UserService(
        ILogisticianRepository logisticianRepository,
        IContactInfoRepository contactInfoRepository,
        PasswordService passwordService,
        IMapper mapper)
    {
        _logisticianRepository = logisticianRepository;
        _contactInfoRepository = contactInfoRepository;
        _passwordService = passwordService;
        _mapper = mapper;
    }

    public async Task<IEnumerable<UserDTO>> GetAllUsersAsync(CancellationToken ct)
    {
        var logisticians = await _logisticianRepository.GetAllLogisticianAsync(ct);
        var contactInfo = await _contactInfoRepository.GetAllContactInfoAsync(ct);

        // Convert to dictionaries for easy lookup
        var contactInfoDict = contactInfo.ToDictionary(c => c.Id);

        // Map logisticians and merge with contact info
        var userDTOs = logisticians.Select(logistician =>
        {
            var userDto = _mapper.Map<UserDTO>(logistician);

            if (contactInfoDict.TryGetValue(logistician.ContactId, out var contact))
            {
                _mapper.Map(contact, userDto);
            }

            return userDto;
        }).ToList();

        return userDTOs;
    }

    public async Task<UserDTO?> GetUserByIdAsync(Guid id, CancellationToken ct)
    {
        var logistician = await _logisticianRepository.GetLogisticianAsync(id, ct);
        if (logistician == null)
            throw new NullReferenceException("User not found");
        
        var contactInfo = await _contactInfoRepository.GetContactInfoAsync(id, ct);
        
        var userDTO = _mapper.Map<UserDTO>(logistician);
        return _mapper.Map(contactInfo, userDTO);
    }
    
    public async Task<UserDTO?> GetUserByEmailAsync(string email, CancellationToken ct)
    {
        var contactInfo = await _contactInfoRepository.GetContactInfoAsync(email, ct);
        if (contactInfo == null)
            throw new NullReferenceException("User not found");
        
        var logistician = await _logisticianRepository.GetLogisticianAsync(contactInfo.Id, ct);
        if (logistician == null)
            throw new NullReferenceException("User not found");
        
        var userDTO = _mapper.Map<UserDTO>(contactInfo);
        _mapper.Map(logistician, userDTO);
        
        return userDTO;
    }

    public async Task<UserDTO> CreateUserAsync(UserDTO user, CancellationToken ct)
    {
        var logistician = await _logisticianRepository.GetLogisticianAsync(user.Id, ct);
        if (logistician != null)
            throw new Exception("User already exists");
        
        logistician = _mapper.Map<Logistician>(user);
        var contactInfo = _mapper.Map<ContactInfo>(user);

        var (hash, salt) = _passwordService.HashPassword(user.Password!);
        logistician.PasswordHash = hash;
        logistician.PasswordSalt = salt;

        await _contactInfoRepository.CreateContactInfoAsync(contactInfo, ct);
        await _logisticianRepository.CreateLogisticianAsync(logistician, ct);

        return user;
    }

    public async Task<UserDTO> VerifyUserAsync(LoginDTO loginInfo, CancellationToken ct)
    {
        var contactInfo = await _contactInfoRepository.GetContactInfoAsync(loginInfo.Email, ct);
        if (contactInfo == null)
            throw new Exception("User doesn't exist");
        
        var logistician = await _logisticianRepository.GetLogisticianAsync(contactInfo.Id, ct);
        if (logistician == null)
            throw new Exception("Logistician doesn't exist");

        if (!_passwordService.VerifyPasswordAsync(logistician, loginInfo.Password, ct))
            throw new Exception("Password does not match");
        
        var userDTO = _mapper.Map<UserDTO>(contactInfo);
        
        _mapper.Map(contactInfo, userDTO);
        
        return userDTO;
    }
    
    public async Task<UserDTO> VerifyUserAsync(UserDTO user, CancellationToken ct)
    {
        var logistician = await _logisticianRepository.GetLogisticianAsync(user.Id, ct);
        if (logistician == null)
            throw new Exception("User doesn't exist");

        if (!_passwordService.VerifyPasswordAsync(logistician, user.Password!, ct))
            throw new Exception("Password does not match");
        
        return _mapper.Map<UserDTO>(logistician);
    }
    
    public async Task DeleteUserAsync(Guid id, CancellationToken ct)
    {
        var logistician = await _logisticianRepository.GetLogisticianAsync(id, ct);
        if (logistician == null)
            throw new NullReferenceException("User not found");

        await _contactInfoRepository.DeleteContactInfoAsync(id, ct);
        await _logisticianRepository.DeleteLogisticianAsync(id, ct);
    }

    public async Task<UserDTO?> UpdateUserAsync(UserDTO user,CancellationToken ct)
    {
        var logistician = await _logisticianRepository.GetLogisticianAsync(user.Id, ct);        
        var contactInfo =  await _contactInfoRepository.GetContactInfoAsync(user.Id, ct);
        if (logistician == null || contactInfo == null)
            throw new NullReferenceException("User not found");

        
        contactInfo.FirstName = user.FirstName;
        contactInfo.LastName = user.LastName;
        contactInfo.BirthDate = user.BirthDate;
        contactInfo.Phone = user.Phone;
        contactInfo.Email = user.Email;
        
        logistician.HasAdminPrivileges = user.HasAdminPrivileges;
        
        await _contactInfoRepository.UpdateContactInfoAsync(contactInfo, ct);
        await _logisticianRepository.UpdateLogisticianAsync(logistician, ct);
        return user;
    }
}