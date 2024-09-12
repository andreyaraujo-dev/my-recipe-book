using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Communication.Responses;

namespace MyRecipeBook.Application.UseCases.User.Register
{
    public interface IRegisterUserUseCase
    {
        public Task<ResponseResgisterUserJson> Execute(RequestRegisterUserJson request);
    }
}
