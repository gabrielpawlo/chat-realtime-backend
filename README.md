# Real-Time Chat — Backend

Real-time chat system built with C#/.NET and SignalR.  
Enables bidirectional communication between connected clients for instant messaging.

---

## 📺 Demo

You can access the project together with the frontend here:  

👉 **[Real-Time Chat — Frontend](https://gabrielpawlo.github.io/chat-realtime-frontend/)**  

⚠️ **Note**: it may take a few seconds for the backend (API) to start running after the deployment.

---

## 📋 Features

- Send and receive real-time messages using WebSockets (SignalR)  
- Supports multiple connected clients  
- Ready for deployment with Docker  
- CORS configuration for separate frontend  

---

## 🛠 Tech Stack

| Technology | Details |
|------------|----------|
| .NET (ASP.NET Core) | main backend framework |
| SignalR | real-time communication |
| Docker | containerization for deployment |
| CORS | allows cross-domain communication |

---

## 🚀 Running locally

1. Clone the repository:

    ```bash
    git clone https://github.com/gabrielpawlo/chat-realtime-backend.git
    cd chat-realtime-backend/backend
    ```

2. Make sure you have the correct .NET SDK version installed (the one used in this project — e.g. .NET 9).

3. Restore dependencies and run the backend:

    ```bash
    dotnet restore
    dotnet run
    ```

4. To run with Docker:

    ```bash
    docker build -t chat-backend .
    docker run -p 5000:80 chat-backend
    ```

    Then access: `http://localhost:5000` (or the port you configured).

---

## 🌐 Frontend Integration

- The frontend must point to the SignalR endpoint (e.g., `http://localhost:5000/chatHub` or your deployed backend URL).  
- Ensure that CORS is properly configured to allow requests from the frontend’s domain.  
- Official frontend of this project: [https://github.com/gabrielpawlo/chat-realtime-frontend](https://github.com/gabrielpawlo/chat-realtime-frontend)

---

## 📦 Deployment

You can deploy this project using Docker on services like Render, Railway, Fly.io, or any cloud provider that supports containers:

1. Use the included Dockerfile.  
2. Push the container to your cloud service.  
3. Configure environment variables (port, URL, CORS, etc.).  
4. Update your frontend to connect to the deployed backend URL.

---

## 🔮 Future Improvements

- ✅ Add database persistence (e.g., PostgreSQL, MongoDB, SQL Server)  
- ✅ Implement user authentication (login, signup, profiles)  
- ✅ Save chat history for later consultation  
- ✅ Integrate with other projects (e.g., admin panel, push notifications)  
- ✅ Create a mobile version of the frontend (React Native, MAUI)  
- ✅ Add automated testing (unit and integration tests)  
- ✅ Automated CI/CD deployment with GitHub Actions  

---

## 🤝 Contributing

Feel free to open issues, suggest new features, or submit pull requests.  
Any feedback is welcome! 🚀

---

## ℹ️ Author

**Gabriel Pawlowski** — Backend Developer

🔗 [LinkedIn Profile](https://www.linkedin.com/in/ggpawlowski/)  
