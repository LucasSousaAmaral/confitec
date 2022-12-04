using Api.Middlewares;
using Autofac;
using confitec_ssss.Configuration;
using confitec_ssss.Configuration.AutoFac;

namespace confitec_ssss;

public class Startup
{
    #region Properties
    public IConfiguration Configuration { get; }
    #endregion

    #region Constructors
    public Startup(IConfiguration configuration) =>
        Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
    #endregion

    #region Methods
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddComponents(Configuration);
        services.AddSwaggerGen();
        services.AddControllers();
        services.AddCors(c =>
        {
            c.AddPolicy("MyPolicy", options => options
             .AllowAnyMethod()
             .AllowAnyOrigin()
             .AllowAnyHeader());
        });
    }
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseErrorHandlingMiddleware();

        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseCors("MyPolicy");
        app.UseAuthorization();
        app.UsePathBase("/");
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("swagger/v1/swagger.json", "Confitec");
            c.RoutePrefix = string.Empty;
        });
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapGet("/", async context =>
            {
                await context.Response.WriteAsync("Welcome to running ASP.NET Core on AWS Lambda");
            });
        });
    }
    public void ConfigureContainer(ContainerBuilder builder)
    {
        builder.RegisterModule(new MediatorModule());
        builder.RegisterModule(new ApplicationModule());
    }
    #endregion
}