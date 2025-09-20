var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Adiciona os serviços do Swagger/OpenAPI
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
            // O domínio do seu site no GitHub Pages
            policy.WithOrigins("https://gabrielpawlo.github.io/chat-realtime")
                  .AllowAnyHeader()
                  .AllowAnyMethod()
                  .AllowCredentials(); // Isso é crucial para o SignalR
        });
});

var app = builder.Build();

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

app.MapControllers();
app.MapHub<ChatApp.Hubs.ChatHub>("/chatHub");

app.UseDefaultFiles();
app.UseStaticFiles();

app.Run();