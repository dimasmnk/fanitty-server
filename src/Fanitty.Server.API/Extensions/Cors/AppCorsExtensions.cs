namespace Fanitty.Server.API.Extensions.Cors;

public static class AppCorsExtensions
{
    const string MyAllowSpecificOrigins = nameof(MyAllowSpecificOrigins);
    public static IServiceCollection AddAppCors(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy(name: MyAllowSpecificOrigins,
                              builder =>
                              {
                                  builder.WithOrigins("*");
                                  builder.WithMethods("*");
                                  builder.WithHeaders("*");
                              });
        });

        return services;
    }

    public static IApplicationBuilder UserAppCors(this IApplicationBuilder app)
    {
        return app.UseCors(MyAllowSpecificOrigins);
    }
}
