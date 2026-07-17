namespace FantasyFootball.Infrastructure.DependencyInjection
{
    public static class DependencyInjection
    {
        // we but this key word because that method is exstension method
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration,
            bool isTesting)
        {
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration.GetConnectionString("RedisConnection") ?? "localhost:6379";
                options.InstanceName = "FantasyFootball_";
            });

            services.AddDbContext<AppDbContext>(option => option.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IPlayerRepository, PlayerRepository>();
            services.AddScoped<IFantasyPlayerRepository, FantasyPlayerRepository>();
            services.AddScoped<IFantasyTeamRepository, FantasyTeamRepository>();
            services.AddScoped<IFixtureRepository, FixtureRepository>();
            services.AddScoped<IGameweekRepository, GameweekRepository>();
            services.AddScoped<IGameweekScoreRepository, GameweekScoreRepository>();
            services.AddScoped<ILeagueMemberRepository, LeagueMemberRepository>();
            services.AddScoped<ILeagueRepository, LeagueRepository>();
            services.AddScoped<IManagerRepository, ManagerRepository>();
            services.AddScoped<IPlayerEventRepository, PlayerEventRepository>();
            services.AddScoped<ITeamRepository, TeamRepository>();
            services.AddScoped<ITransferRepository, TransferRepository>();
            services.AddScoped<IChatMessageRepository, ChatMessageRepository>();
            services.AddScoped<INewsArticleRepository, NewsArticleRepository>();
            services.AddScoped<IPlayerNewsRepository, PlayerNewsRepository>();
            services.AddScoped<FantasyFootball.Application.Common.Interfaces.Repositories.IPaymentTransactionRepository, FantasyFootball.Infrastructure.Persistence.Repositories.PaymentTransactionRepository>();

            services.Configure<FantasyFootball.Application.Configuration.PaymentSettings>(
                configuration.GetSection(FantasyFootball.Application.Configuration.PaymentSettings.SectionName));
            services.Configure<FantasyFootball.Infrastructure.Configuration.StripeSettings>(
                configuration.GetSection(FantasyFootball.Infrastructure.Configuration.StripeSettings.SectionName));

            services.AddScoped<IPaymentProviderService, FantasyFootball.Infrastructure.Services.StripePaymentProviderService>();
            
            services.AddIdentity<ApplicationUser, IdentityRole<Guid>>()
                    .AddEntityFrameworkStores<AppDbContext>()
                    .AddDefaultTokenProviders();
            
                services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
            services.Configure<ApiFootballOptions>(configuration.GetSection(ApiFootballOptions.SectionName));

            services.AddHttpClient<ApiFootballClient>();
            services.AddScoped<TeamMapper>();
            services.AddScoped<PlayerMapper>();
            services.AddScoped<FixtureMapper>();
            services.AddScoped<IFootballDataProvider, ApiFootballProvider>();

            // Register Identity Services
            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<IJwtService, JwtService>();
            services.AddTransient<IEmailService, EmailService>();

            services.AddScoped<IAutoSubJob, AutoSubBackgroundJob>();
            services.AddSingleton<ICacheService, FantasyFootball.Infrastructure.Services.RedisCacheService>();

            services.AddHostedService<GameweekProcessorBackgroundService>();

            // --- MOVED FROM PROGRAM.CS ---
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\""
                });

                c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
                {
                    {
                        new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                        {
                            Reference = new Microsoft.OpenApi.Models.OpenApiReference
                            {
                                Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });

            services.AddSignalR();
            services.AddScoped<FantasyFootball.Application.Common.Interfaces.Services.IMatchNotificationService, FantasyFootball.Infrastructure.Hubs.MatchNotificationService>();
            services.AddScoped<FantasyFootball.Application.Common.Interfaces.Services.ISmartLiveSchedulerService, FantasyFootball.Infrastructure.BackgroundJobs.SmartLiveSchedulerService>();
            services.AddScoped<FantasyFootball.Application.Common.Interfaces.Services.ILeaderboardService, FantasyFootball.Infrastructure.Services.LeaderboardService>();

            if (!isTesting)
            {
                services.AddHangfire(config => config
                    .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
                    .UseSimpleAssemblyNameTypeSerializer()
                    .UseRecommendedSerializerSettings()
                    .UseSqlServerStorage(configuration.GetConnectionString("DefaultConnection")));

                services.AddHangfireServer();
            }

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = configuration["Jwt:Issuer"],
                        ValidAudience = configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!))
                    };
                });
            services.AddAuthorization();
            services.AddControllers();

            services.AddExceptionHandler<FantasyFootball.Infrastructure.Exceptions.GlobalExceptionHandler>();
            services.AddProblemDetails();
            
            // Configure Health Checks
            var healthChecks = services.AddHealthChecks()
                .AddUrlGroup(new Uri("https://v3.football.api-sports.io/status"), name: "external-api");

            if (!isTesting)
            {
                healthChecks.AddSqlServer(configuration.GetConnectionString("DefaultConnection")!, name: "database");
                var redisConfig = configuration.GetConnectionString("Redis") ?? "localhost:6379";
                healthChecks.AddRedis(redisConfig, name: "redis");
            }

            Microsoft.AspNetCore.Builder.RateLimiterServiceCollectionExtensions.AddRateLimiter(services, options =>
            {
                options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
                options.AddFixedWindowLimiter("fixed", limiterOptions =>
                {
                    limiterOptions.PermitLimit = 100; // Max 100 requests
                    limiterOptions.Window = TimeSpan.FromMinutes(1); // Per 1 minute
                    limiterOptions.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                    limiterOptions.QueueLimit = 0; // Don't queue, reject immediately
                });
            });

            // Security: CORS Configuration
            services.AddCors(options =>
            {
                options.AddPolicy("StrictPolicy", policy =>
                {
                    policy.WithOrigins("http://localhost:3000", "http://localhost:5173", "https://yourproductionfrontend.com")
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowCredentials(); // Required for SignalR
                });
            });

            // Security: Configure Forwarded Headers for Nginx
            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
                // These two lines are needed if Nginx runs in a different container on the same network
                options.KnownNetworks.Clear();
                options.KnownProxies.Clear();
            });

            return services;
        }

        public static async Task<WebApplication> UseInfrastructureAsync(this WebApplication app, bool isTesting)
        {
            app.UseForwardedHeaders(); // Must be first to resolve real IPs from Nginx
            app.UseExceptionHandler();
            
            // Security: Use Custom Security Headers Middleware
            app.UseMiddleware<FantasyFootball.Infrastructure.Middlewares.SecurityHeadersMiddleware>();

            if (!isTesting)
            {
                SerilogApplicationBuilderExtensions.UseSerilogRequestLogging(app);
            }
            app.UseRateLimiter(); // Enable the middleware
            app.UseCors("StrictPolicy");

            app.MapHub<FantasyFootball.Infrastructure.Hubs.MatchHub>("/hubs/match");
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            if (!isTesting)
            {
                app.UseHangfireDashboard("/hangfire"); // Adds a UI at http://localhost:5000/hangfire
                Hangfire.GlobalJobFilters.Filters.Add(new FantasyFootball.Infrastructure.Configuration.HangfireLogFilter());
                FantasyFootball.Infrastructure.Configuration.HangfireJobsScheduler.ScheduleRecurringJobs();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();

            app.MapHealthChecks("/health");
            if (!isTesting)
            {
                app.MapHealthChecks("/health/database", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
                {
                    Predicate = (check) => check.Name == "database"
                });
                app.MapHealthChecks("/health/redis", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
                {
                    Predicate = (check) => check.Name == "redis"
                });
            }
            app.MapHealthChecks("/health/external-api", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
            {
                Predicate = (check) => check.Name == "external-api"
            });

            // Apply Database Migrations and Seed
            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<FantasyFootball.Infrastructure.Persistence.AppDbContext.AppDbContext>();
                await context.Database.MigrateAsync();
            }

            using (var scope = app.Services.CreateScope())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<Microsoft.AspNetCore.Identity.UserManager<FantasyFootball.Infrastructure.Identity.ApplicationUser>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<Microsoft.AspNetCore.Identity.RoleManager<Microsoft.AspNetCore.Identity.IdentityRole<Guid>>>();
                await FantasyFootball.Infrastructure.Identity.DataSeeder.SeedRolesAndAdminAsync(userManager, roleManager);
            }

            return app;
        }
    }
}
