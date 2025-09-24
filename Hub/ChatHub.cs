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
            var messages = await _dbContext.Messages.OrderBy(m => m.Timestamp).ToListAsync();
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

            // CORRIGIDO: Agora envia o objeto completo da mensagem
            await Clients.All.SendAsync("ReceiveMessage", message);
        }
    }
}