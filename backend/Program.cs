var builder = WebApplication.CreateBuilder(args);

// Adiciona os serviços essenciais
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Adiciona o serviço SignalR
builder.Services.AddSignalR();

// Configura a política de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("MyCorsPolicy",
        policy =>
        {
            policy.WithOrigins("https://gabrielpawlo.github.io/chat-realtime")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

var app = builder.Build();

// Configura o pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseRouting();

// Usa a política de CORS
app.UseCors("MyCorsPolicy");

// Mapeia os endpoints
app.MapControllers();
app.MapHub<ChatApp.Hubs.ChatHub>("/chatHub");

// Configura o uso de arquivos estáticos
app.UseDefaultFiles();
app.UseStaticFiles();

app.Run();