using WatchMe.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

public class AuthService
{
    private readonly string _jwtSecretKey;

    public AuthService(string jwtSecretKey)
    {
        _jwtSecretKey = jwtSecretKey;
    }

    public  string AuthenticateUser(User user)
    {
    

        // Generate a JWT token with the user's identity as claims
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtSecretKey);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(
                new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Username)
                }
            ),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature
            )
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var tokenString = tokenHandler.WriteToken(token);

        return tokenString;
    }

    // public static string KeyGen()
    // {
    //     string keygenString = "qwertyuiopasdfghjklzxcnvbm1234567890";
    //     Random rnd = new Random();
    //     string randomKey = "";
    //     for (int i = 0; i < 15; i++)
    //     {
    //         int randomIndex = rnd.Next(keygenString.Length);
    //         randomKey += keygenString[randomIndex];
    //     }
    //     return randomKey;
    // }
    // private LoginUser VerifyUser(string email, string password)
    // {

    // }
}
