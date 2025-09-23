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
            // Exemplo: carregar mensagens do banco ou lista em memória
            var history = new List<object>
            {
                new { user = "System", text = "Welcome to the chat!" }
            };
            await Clients.Caller.SendAsync("ReceiveMessageHistory", history);
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

            // Salva a mensagem no banco de dados
            _dbContext.Messages.Add(message);
            await _dbContext.SaveChangesAsync();

            // Envia a mensagem para todos os clientes, incluindo o remetente
            await Clients.All.SendAsync("ReceiveMessage", message);
        }
    }
}