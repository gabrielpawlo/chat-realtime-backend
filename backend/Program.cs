var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Adiciona os servi�os do Swagger/OpenAPI
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
            // O dom�nio do seu site no GitHub Pages
            policy.WithOrigins("https://gabrielpawlo.github.io/chat-realtime")
                  .AllowAnyHeader()
                  .AllowAnyMethod()
                  .AllowCredentials(); // Isso � crucial para o SignalR
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

// Usa a pol�tica de CORS
app.UseCors("MyCorsPolicy");

app.MapControllers();
app.MapHub<ChatApp.Hubs.ChatHub>("/chatHub");

app.UseDefaultFiles();
app.UseStaticFiles();

app.Run();