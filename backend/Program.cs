using ChatApp.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configuração do banco MySQL
builder.Services.AddDbContext<ChatDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 36)) // ajuste a versão conforme o seu MySQL
    )
);

// Controllers e Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// SignalR
builder.Services.AddSignalR();

// Configuração de CORS (permitindo acesso do GitHub Pages)
builder.Services.AddCors(options =>
{
    options.AddPolicy("MyCorsPolicy", policy =>
    {
        policy.WithOrigins("https://gabrielpawlo.github.io/chat-realtime") // se for rodar o site em uma subpasta, use ex: "https://gabrielpawlo.github.io/chat-realtime"
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

var app = builder.Build();

// Swagger só em dev
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseRouting();

// CORS precisa vir antes dos endpoints
app.UseCors("MyCorsPolicy");

app.MapControllers();
app.MapHub<ChatApp.Hubs.ChatHub>("/chatHub");

// Arquivos estáticos (se tiver front no mesmo projeto)
app.UseDefaultFiles();
app.UseStaticFiles();

app.Run();
