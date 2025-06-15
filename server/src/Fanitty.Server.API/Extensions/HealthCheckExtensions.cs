﻿using Fanitty.Server.API.Extensions;

namespace Fanitty.Server.API.Extensions;

public static class HealthCheckExtensions
{
    public static void AddHealthChecks(this WebApplicationBuilder builder)
    {
        builder.Services.AddHealthChecks().AddNpgSql(builder.Configuration.GetValue<string>("Database")!);
    }
}
