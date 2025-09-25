using Microsoft.AspNetCore.SignalR;

namespace ChatApp.Hubs
{
    public class ChatHub : Hub
    {
        // Lista estática para armazenar as mensagens em memória
        private static List<(string User, string Text, DateTime Timestamp)> _messages
            = new List<(string, string, DateTime)>();

        public override async Task OnConnectedAsync()
        {
            // Envia o histórico apenas para o cliente que acabou de se conectar
            await Clients.Caller.SendAsync("ReceiveMessageHistory", _messages);
            await base.OnConnectedAsync();
        }

        public async Task SendMessage(string user, string text)
        {
            var message = (User: user, Text: text, Timestamp: DateTime.Now);

            // Salva em memória
            _messages.Add(message);

            // Envia para todos os clientes conectados
            await Clients.All.SendAsync("ReceiveMessage", message.User, message.Text, message.Timestamp);
        }
    }
}
