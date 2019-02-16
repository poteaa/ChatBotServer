using System;
using ChatBot.Model.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Options;

namespace ChatBot.Data.Models
{
    public partial class ChatBotContext : DbContext
    {
        private AppSettings AppSettings { get; set; }
        public ChatBotContext()
        {
        }

        public ChatBotContext(DbContextOptions<ChatBotContext> options, IOptions<AppSettings> settings)
            : base(options)
        {
            AppSettings = settings.Value;
        }

        public virtual DbSet<ChatRoom> ChatRoom { get; set; }
        public virtual DbSet<Message> Message { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserChatRoom> UserChatRoom { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // TODO: get the connections string from the config
                optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=ChatBot;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChatRoom>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.Property(e => e.Body)
                    .IsRequired()
                    .IsUnicode(false);

                entity.HasOne(d => d.ChatRoom)
                    .WithMany(p => p.Message)
                    .HasForeignKey(d => d.ChatRoomId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Message_ChatRoom");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Message)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Message_User");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.Name)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.User)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User");
            });

            modelBuilder.Entity<UserChatRoom>(entity =>
            {
                entity.HasOne(d => d.ChatRoom)
                    .WithMany(p => p.UserChatRoom)
                    .HasForeignKey(d => d.ChatRoomId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserChatRoom2");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserChatRoom)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserChatRoom1");
            });
        }
    }
}
