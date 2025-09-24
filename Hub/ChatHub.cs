using Microsoft.AspNetCore.SignalR;
using ChatApp.Data;
using ChatApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Hubs
{
    public class ChatHub : Hub
    {
        private readonly ChatDbContext _dbContext;

        public ChatHub(ChatDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public override async Task OnConnectedAsync()
        {
            var messages = await _dbContext.Messages
                                           .OrderBy(m => m.Timestamp)
                                           .ToListAsync();

            // Envia o histórico apenas para o usuário que acabou de entrar
            await Clients.Caller.SendAsync("ReceiveMessageHistory", messages);

            await base.OnConnectedAsync();
        }

        public async Task SendMessage(string user, string text)
        {
            var message = new Message
            {
                User = user,
                Text = text,
                Timestamp = DateTime.Now
            };

            _dbContext.Messages.Add(message);
            await _dbContext.SaveChangesAsync();

            // Envia só os dados relevantes para todos os clientes
            await Clients.All.SendAsync("ReceiveMessage", message.User, message.Text, message.Timestamp);
        }

    }
}