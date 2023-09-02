namespace Auth.Extensions
{
    public static class AddAutheExtensions
    {

        public static WebApplicationBuilder addAuthorizationExtension (this WebApplicationBuilder builder)
        {
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("Role", "Admin");
                });
            });
            return builder;
        }
    }
}
