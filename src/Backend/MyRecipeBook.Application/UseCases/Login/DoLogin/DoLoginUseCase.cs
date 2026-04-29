using MyRecipeBook.Application.Services.Cryptography;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Communication.Responses;
using MyRecipeBook.Domain.Repositories.User;
using MyRecipeBook.Domain.Security.Tokens;
using MyRecipeBook.Exceptions.ExceptionsBase;

namespace MyRecipeBook.Application.UseCases.Login.DoLogin;

public class DoLoginUseCase : IDoLoginUseCase
{
    private readonly IUserReadOnlyRepository _repository;
    private readonly PasswordEncripter _passwordEncrypter;
    private readonly IAccessTokenGenerator _accessTokenGenerator;

    public DoLoginUseCase(
        IUserReadOnlyRepository repository, 
        IAccessTokenGenerator accessTokenGenerator,
        PasswordEncripter passwordEncrypter)
    {
        _repository = repository;
        _passwordEncrypter = passwordEncrypter;
        _accessTokenGenerator = accessTokenGenerator;
    }
    
    public async Task<ResponseResgisterUserJson> Execute(RequestLoginJson request)
    {
        var encryptedPassword = _passwordEncrypter.Encrypt(request.Password);
        var user = await _repository.GetByEmailAndPassword(request.Email, encryptedPassword);
        if (user is null)
            throw new InvalidLoginException();
        
        return new ResponseResgisterUserJson
        {
            Name = user.Name,
            Tokens = new ResponseTokensJson
            {
                AccessToken = _accessTokenGenerator.Generate(user.UserIdentifier)
            }
        };
    }
}