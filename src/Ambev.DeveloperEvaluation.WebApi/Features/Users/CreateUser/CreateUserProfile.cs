using Ambev.DeveloperEvaluation.Domain.Users;
using Ambev.DeveloperEvaluation.WebApi.Contracts.Users;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Users.CreateUser;

/// <summary>
/// Profile for mapping between User entity and CreateUserResponse
/// </summary>
public class CreateUserProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for CreateUser operation
    /// </summary>
    public CreateUserProfile()
    {
        CreateMap<CreateUserRequest, CreateUserCommand>();
        CreateMap<CreateUserCommand, User>();
        CreateMap<User, CreateUserResult>();
        CreateMap<CreateUserResult, CreateUserResponse>();
    }
}
