using ChatApp.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//adiciona os serviços essenciais
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//adiciona o serviço SignalR
builder.Services.AddSignalR();

//configura a política de CORS
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

//adiciona o DbContext e a string de conexão do MySQL (movido para antes do builder.Build())
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ChatDbContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

var app = builder.Build();

//configura o pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseCors("MyCorsPolicy");

app.UseAuthorization();

//mapeia os endpoints (Controllers e Hubs)
app.MapControllers();
app.MapHub<ChatApp.Hubs.ChatHub>("/chatHub");

//configura o uso de arquivos estáticos (movido para antes do roteamento)
app.UseDefaultFiles();
app.UseStaticFiles();

app.Run();