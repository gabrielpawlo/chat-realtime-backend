var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSignalR();

builder.Services.AddCors(options =>
{
    options.AddPolicy("MyCorsPolicy",
        policy =>
        {
            policy.WithOrigins("https://gabrielpawlo.github.io/chat-realtime") // CORREÇÃO AQUI
                  .AllowAnyHeader()
                  .AllowAnyMethod();
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
app.UseCors("MyCorsPolicy");

app.MapControllers();
app.MapHub<ChatApp.Hubs.ChatHub>("/chatHub");

app.UseDefaultFiles();
app.UseStaticFiles();

app.Run();