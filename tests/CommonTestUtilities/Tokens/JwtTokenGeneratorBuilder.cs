using MyRecipeBook.Domain.Security.Tokens;
using MyRecipeBook.Infrastructure.Security.Tokens.Generator;

namespace CommonTestUtilities.Tokens;

public class JwtTokenGeneratorBuilder
{
    public static IAccessTokenGenerator Build() => new JwtTokenGenerator(expirationTimeMinutes: 5, signinKey: "WWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWW");
}