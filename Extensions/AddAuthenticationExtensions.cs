using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Auth.Extensions
{
    public static class  AddAuthenticationExtensions
    {
        public static WebApplicationBuilder AddAppAuthentication(this WebApplicationBuilder builder)
        {
            builder.Services.AddAuthentication("Bearer").AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new()
                {
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateIssuerSigningKey = true,
                    //what is valid
                    ValidAudience = builder.Configuration["TokenSecurity:Audience"],
                    ValidIssuer = builder.Configuration["TokenSecurity:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["TokenSecurity:SecretKey"]))

                };
            });

            return builder;
        }
    }
}
