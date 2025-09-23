const connection = new signalR.HubConnectionBuilder()
    .withUrl("https://chat-realtime-backend-yu5o.onrender.com/chatHub")
    .build();

const loginScreen = document.getElementById('login-screen');
const chatScreen = document.getElementById('chat-screen');
const usernameInput = document.getElementById('usernameInput');
const joinButton = document.getElementById('joinButton');
const messageInput = document.getElementById('messageInput');
const sendButton = document.getElementById('sendButton');
const messagesList = document.getElementById('messagesList');

let username = "";

// Função para exibir a mensagem na tela
function addMessage(user, message) {
    const li = document.createElement("li");
    li.textContent = `${user}: ${message}`;
    messagesList.appendChild(li);
    messagesList.scrollTop = messagesList.scrollHeight; // Rola para o final
}

connection.on("ReceiveMessageHistory", (messages) => {
    messages.forEach(msg => addMessage(msg.user, msg.text));
});

// Lógica de conexão e mensagens
connection.on("ReceiveMessage", (message) => {
    addMessage(message.user, message.text);
});

joinButton.addEventListener('click', () => {
    username = usernameInput.value.trim();
    if (username) {
        loginScreen.classList.add('hidden');
        chatScreen.classList.remove('hidden');
        connection.start().catch(err => console.error(err.toString()));
    } else {
        alert("Por favor, digite seu nome de usuário.");
    }
});

function sendMessage() {
    const message = messageInput.value.trim();
    if (message) {
        connection.invoke("SendMessage", username, message).catch(err => console.error(err.toString()));
        messageInput.value = "";
    }
}

sendButton.addEventListener("click", sendMessage);

// Permite enviar mensagem ao apertar Enter
messageInput.addEventListener("keypress", (e) => {
    if (e.key === 'Enter') {
        sendMessage();
    }
});

console.log("Chat Frontend Carregado.");