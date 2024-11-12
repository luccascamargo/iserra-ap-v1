using iserra_api.Data;
using iserra_api.Dto;
using iserra_api.Models;
using iserra_api.Responses;
using Microsoft.EntityFrameworkCore;

namespace iserra_api.Services.UserServices;

public class UserService : IUserService
{
    private readonly ApplicatonDbContext _context;

    public UserService(ApplicatonDbContext context)
    {
        _context = context;
    }

    public async Task<ResponseModel<List<UserDto>>> GetAllUsers()
    {
        var response = new ResponseModel<List<UserDto>>();
        var users = await _context.Users.Select(x =>
                new UserDto {
                Id = x.Id,
                StripeId = x.StripeId,
                Nome = x.Nome,
                Sobrenome = x.Sobrenome, 
                Email = x.Email,
                Telefone = x.Telefone, 
                Ativo = x.Ativo,
                Plano = x.Plano,
                Imagem = x.Imagem
                }).ToListAsync();

        response.Dados = users;

        return response;
    }

    public Task<UserDto> Me(Guid userId)
    {
        throw new NotImplementedException();
    }

    public async Task<ResponseModel<UserDto>> CreateUser(UserCreateDto userDto)
    {
        var response = new ResponseModel<UserDto>();
        var userAllreadyExists = await _context.Users.FirstOrDefaultAsync(x => x.Email == userDto.Email);

        if (userAllreadyExists != null) {
            response.Mensagem = "Usuario ja existe na base de dados";
            return response;
        }

        var user = new User(userDto.StripeId, userDto.Nome, userDto.Sobrenome, userDto.Email, userDto.Telefone, userDto.Ativo, userDto.Plano, userDto.Imagem, userDto.Senha, []);

        response.Mensagem = "Usuario criado com sucesso";

        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();

        var userReturnDto = new UserDto {
            Id = user.Id,
            StripeId = user.StripeId,
            Nome = user.Nome,
            Sobrenome = user.Sobrenome,
            Email = user.Email,
            Telefone = user.Telefone,
            Ativo = user.Ativo,
            Plano = user.Plano,
            Imagem = user.Imagem
        };
        response.Dados = userReturnDto;


        return response;
    }

    public async Task<ResponseModel<UserDto>> UpdateUser(string email, UserUpdateDto user) {
        var userAllReadyExists = await _context.Users.FirstOrDefaultAsync(x=> x.Email == email);

        var response = new ResponseModel<UserDto>();

        if (userAllReadyExists == null) {
            response.Mensagem = "Usuario nao encomtrado";
            return response;
        }

        userAllReadyExists.Telefone = user.Telefone;
        userAllReadyExists.Nome = user.Nome;
        userAllReadyExists.Sobrenome = user.Sobrenome;
        userAllReadyExists.Senha = user.Senha;
        userAllReadyExists.Imagem = user.Imagem;
        userAllReadyExists.UpdatedAt = DateTime.UtcNow;

        response.Dados = new UserDto {
            Id = userAllReadyExists.Id,
            StripeId = userAllReadyExists.StripeId,
            Nome = userAllReadyExists.Nome,
            Sobrenome = userAllReadyExists.Sobrenome,
            Email = userAllReadyExists.Email,
            Telefone = userAllReadyExists.Telefone,
            Ativo = userAllReadyExists.Ativo,
            Plano = userAllReadyExists.Plano,
            Imagem = userAllReadyExists.Imagem
        };
        ;
        response.Mensagem = "Usuario alterado com sucesso";


        await _context.SaveChangesAsync();

        return response;

    }
}