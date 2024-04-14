using API.AppConfig;
using Application.Mapper;
using IGeekFan.AspNetCore.RapiDoc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.Internal;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var builderconfig = new ConfigurationBuilder()
                           .SetBasePath(Directory.GetCurrentDirectory())
                           .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                           .AddEnvironmentVariables();

IConfigurationRoot configuration = builderconfig.Build();
//builder.Services.AddAutoMapper();
//builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddJaeger();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors();
builder.Services.AddDependency();
builder.Services.Configure<RapiDocOptions>(c => {
    builder.Configuration.Bind("RapiDoc", c);
});






var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    //app.UseSwaggerUI();
//}

//if(app.Environment.IsProduction())
//{
//    app.UseSwagger();
//    //app.UseSwaggerUI();
//}

app.UseSwagger();

app.UseRouting();
app.UseHttpsRedirection();

app.UseAuthorization();
app.UseCors(builder =>
{
    builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader();
});




app.UseRapiDocUI(c =>
{
    //This Config Higher priority
    c.GenericRapiConfig = new GenericRapiConfig()
    {
        
        RenderStyle = "read",//read | view | focused
        Theme = "dark",//light | dark
        SchemaStyle = "table"//tree | table
    };

});

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapSwagger("{documentName}/api-docs");
});

//app.MapControllers();

//var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";

//var url = $"http://0.0.0.0:{port}";
//var target = Environment.GetEnvironmentVariable("TARGET") ?? "World";

//var app = builder.Build();

//app.MapGet("/", () => $"Hello {target}!");

app.Run();

//app.Run();
