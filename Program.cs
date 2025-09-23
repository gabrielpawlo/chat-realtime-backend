using ChatApp.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Adiciona os servi�os essenciais
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Adiciona o servi�o SignalR
builder.Services.AddSignalR();

// Configura a pol�tica de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("MyCorsPolicy",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

// Adiciona o DbContext e a string de conex�o do MySQL (movido para antes do builder.Build())
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ChatDbContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

var app = builder.Build();

// Configura o pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseCors("MyCorsPolicy");

app.UseAuthorization();

// Mapeia os endpoints (Controllers e Hubs)
app.MapControllers();
app.MapHub<ChatApp.Hubs.ChatHub>("/chatHub");

// Configura o uso de arquivos est�ticos (movido para antes do roteamento)
app.UseDefaultFiles();
app.UseStaticFiles();

app.Run();