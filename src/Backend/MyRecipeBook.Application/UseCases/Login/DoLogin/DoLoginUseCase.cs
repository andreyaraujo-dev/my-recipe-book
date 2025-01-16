using MyRecipeBook.Application.Services.Cryptography;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Communication.Responses;
using MyRecipeBook.Domain.Repositories.User;
using MyRecipeBook.Exceptions.ExceptionsBase;

namespace MyRecipeBook.Application.UseCases.Login.DoLogin;

public class DoLoginUseCase : IDoLoginUseCase
{
    private readonly IUserReadOnlyRepository _repository;
    private readonly PasswordEncripter _passwordEncrypter;

    public DoLoginUseCase(IUserReadOnlyRepository repository, PasswordEncripter passwordEncrypter)
    {
        _repository = repository;
        _passwordEncrypter = passwordEncrypter;
    }
    
    public async Task<ResponseResgisterUserJson> Execute(RequestLoginJson request)
    {
        var encryptedPassword = _passwordEncrypter.Encrypt(request.Password);
        var user = await _repository.GetByEmailAndPassword(request.Email, encryptedPassword);
        if (user is null)
            throw new InvalidLoginException();
        
        return new ResponseResgisterUserJson
        {
            Name = user.Name
        };
    }
}