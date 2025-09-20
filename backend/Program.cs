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

// Usa a pol�tica de CORS
app.UseCors("MyCorsPolicy");

// Mapeia os endpoints
app.MapControllers();
app.MapHub<ChatApp.Hubs.ChatHub>("/chatHub");

// Configura o uso de arquivos est�ticos
app.UseDefaultFiles();
app.UseStaticFiles();

app.Run();