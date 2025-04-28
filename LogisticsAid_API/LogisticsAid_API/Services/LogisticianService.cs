using AutoMapper;
using LogisticsAid_API.Context;
using LogisticsAid_API.DTOs;
using LogisticsAid_API.Entities;
using LogisticsAid_API.Repositories.Interfaces;

namespace LogisticsAid_API.Services;

public class LogisticianService
{
    private readonly ILogisticianRepository _logisticianRepository;
    private readonly IContactInfoRepository _contactInfoRepository;
    private readonly PasswordService _passwordService;
    private readonly IMapper _mapper;
    private readonly LogisticsAidDbContext _context;

    public LogisticianService(
        ILogisticianRepository logisticianRepository,
        IContactInfoRepository contactInfoRepository,
        PasswordService passwordService,
        IMapper mapper,
        LogisticsAidDbContext context)
    {
        _logisticianRepository = logisticianRepository;
        _contactInfoRepository = contactInfoRepository;
        _passwordService = passwordService;
        _mapper = mapper;
        _context = context;
    }

    public async Task<IEnumerable<LogisticianDTO>> GetAllUsersAsync(CancellationToken ct)
    {
        var logisticians = await _logisticianRepository.GetAllLogisticianAsync(ct);

        var logisticianDtoList = new List<LogisticianDTO>();
        foreach (var logistician in logisticians)
        {
            var contactInfo = await _contactInfoRepository.GetContactInfoAsync(logistician.ContactId, CancellationToken.None);
            
            var contactInfoDto = _mapper.Map<ContactInfoDTO>(contactInfo);
            var logisticianDto = _mapper.Map<LogisticianDTO>(logistician);
            
            logisticianDto.ContactInfo = contactInfoDto;
            
            logisticianDtoList.Add(logisticianDto);
        }

        return logisticianDtoList;
    }

    public async Task<LogisticianDTO?> GetUserByIdAsync(Guid id, CancellationToken ct)
    {
        var logistician = await _logisticianRepository.GetLogisticianAsync(id, ct);
        if (logistician == null)
            throw new NullReferenceException("User not found");
        
        var contactInfo = await _contactInfoRepository.GetContactInfoAsync(id, ct);
        
        var logisticianDto = _mapper.Map<LogisticianDTO>(logistician);
        logisticianDto.ContactInfo = _mapper.Map<ContactInfoDTO>(contactInfo);
        
        return logisticianDto;
    }
    
    public async Task<LogisticianDTO?> GetUserByEmailAsync(string email, CancellationToken ct)
    {
        var contactInfo = await _contactInfoRepository.GetContactInfoAsync(email, ct);
        if (contactInfo == null)
            throw new NullReferenceException("User not found");
        
        var logistician = await _logisticianRepository.GetLogisticianAsync(contactInfo.Id, ct);
        if (logistician == null)
            throw new NullReferenceException("User not found");
        
        var logisticianDto = _mapper.Map<LogisticianDTO>(logistician);
        logisticianDto.ContactInfo = _mapper.Map<ContactInfoDTO>(contactInfo);
        
        return logisticianDto;
    }

    public async Task<LogisticianDTO> CreateUserAsync(LogisticianDTO logisticianDto, CancellationToken ct)
    {
        var logistician = await _logisticianRepository.GetLogisticianAsync(logisticianDto.ContactInfo.Email!, ct);
        if (logistician != null)
            throw new Exception("User already exists");

        logistician = _mapper.Map<Logistician>(logisticianDto);
        var contactInfo = _mapper.Map<ContactInfo>(logisticianDto.ContactInfo);

        logistician.ContactInfo = contactInfo;

        var (hash, salt) = _passwordService.HashPassword(logisticianDto.Password!);
        logistician.PasswordHash = hash;
        logistician.PasswordSalt = salt;

        await using var transaction = await _context.Database.BeginTransactionAsync(ct);
        try
        {
            await _contactInfoRepository.CreateContactInfoAsync(contactInfo, ct);
            await _logisticianRepository.CreateLogisticianAsync(logistician, ct);

            await transaction.CommitAsync(ct);
        }
        catch (Exception)
        {
            await transaction.RollbackAsync(ct);
            throw;
        }

        return logisticianDto;
    }


    public async Task<LogisticianDTO> VerifyUserAsync(LoginDTO loginInfo, CancellationToken ct)
    {
        var contactInfo = await _contactInfoRepository.GetContactInfoAsync(loginInfo.Email, ct);
        if (contactInfo == null)
            throw new Exception("User doesn't exist");
        
        var logistician = await _logisticianRepository.GetLogisticianAsync(contactInfo.Id, ct);
        if (logistician == null)
            throw new Exception("Logistician doesn't exist");

        if (!_passwordService.VerifyPasswordAsync(logistician, loginInfo.Password, ct))
            throw new Exception("Password does not match");
        
        var logisticianDto = _mapper.Map<LogisticianDTO>(logistician);
        logisticianDto.ContactInfo = _mapper.Map<ContactInfoDTO>(contactInfo);
        
        return logisticianDto;
    }
    
    public async Task<LogisticianDTO> VerifyUserAsync(LogisticianDTO logisticianDto, CancellationToken ct)
    {
        var logistician = await _logisticianRepository.GetLogisticianAsync(logisticianDto.ContactInfo.Id, ct);
        if (logistician == null)
            throw new Exception("User doesn't exist");

        if (!_passwordService.VerifyPasswordAsync(logistician, logisticianDto.Password!, ct))
            throw new Exception("Password does not match");
        
        return _mapper.Map<LogisticianDTO>(logistician);
    }
    
    public async Task DeleteUserAsync(Guid id, CancellationToken ct)
    {
        var logistician = await _logisticianRepository.GetLogisticianAsync(id, ct);
        if (logistician == null)
            throw new NullReferenceException("User not found");

        await _contactInfoRepository.DeleteContactInfoAsync(id, ct);
        await _logisticianRepository.DeleteLogisticianAsync(id, ct);
    }

    public async Task UpdateUserAsync(LogisticianDTO logisticianDto,CancellationToken ct)
    {
        var logistician = await _logisticianRepository.GetLogisticianAsync(logisticianDto.ContactInfo.Id, ct);        
        var contactInfo =  await _contactInfoRepository.GetContactInfoAsync(logisticianDto.ContactInfo.Id, ct);
        if (logistician == null || contactInfo == null)
            throw new NullReferenceException("User not found");

        
        contactInfo.FirstName = logisticianDto.ContactInfo.FirstName;
        contactInfo.LastName = logisticianDto.ContactInfo.LastName;
        contactInfo.BirthDate = logisticianDto.ContactInfo.BirthDate;
        contactInfo.Phone = logisticianDto.ContactInfo.Phone;
        contactInfo.Email = logisticianDto.ContactInfo.Email;
        
        logistician.HasAdminPrivileges = logisticianDto.HasAdminPrivileges;
        
        await _contactInfoRepository.UpdateContactInfoAsync(contactInfo, ct);
        await _logisticianRepository.UpdateLogisticianAsync(logistician, ct);
    }
}