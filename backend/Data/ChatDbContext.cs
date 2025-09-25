using ChatApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Data
{
    public class ChatDbContext : DbContext
    {
        public ChatDbContext(DbContextOptions<ChatDbContext> options)
            : base(options)
        {
        }

        public DbSet<Message> Messages { get; set; }

        // Configuração opcional para garantir que a tabela seja criada corretamente
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Message>(entity =>
            {
                entity.ToTable("Messages"); // Nome da tabela
                entity.HasKey(m => m.Id);   // Chave primária
                entity.Property(m => m.User).HasMaxLength(100);
                entity.Property(m => m.Text).IsRequired();
                entity.Property(m => m.Timestamp).IsRequired();
            });
        }
    }
}
