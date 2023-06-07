using Microsoft.Extensions.DependencyInjection;
using OpenAI;
using SCPHA_GPT.Interfaces;
using SCPHA_GPT.Persistence;
using SCPHA_GPT.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var openAiAuthentication = OpenAIAuthentication.LoadFromEnv();   // implicitly use environment variable OPENAI_API_KEY
builder.Services.AddSingleton<OpenAIClient>(new OpenAIClient(openAiAuthentication));

builder.Services.AddScoped<IChatGPT, SCPHADescriptionGenerator>();
builder.Services.AddScoped<IDallE2, DallE2>();

builder.Services.AddSingleton<SCPHAContext>(new SCPHAContext());

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
