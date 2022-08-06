using Microsoft.EntityFrameworkCore;
using MyLetter.EF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Identity;
using System.Reflection.Emit;
using System.Data;

namespace MyLetter.EF
{
    public class ApplicationDbContext : IdentityDbContext<AppUser, AppRole, int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<PublicMessage> PublicMessages { get; set; }
        public DbSet<AppUser> AppUser { get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<Home> Home { get; set; }
        public DbSet<Age> Age { get; set; }
        public DbSet<Height> Height { get; set; }
        public DbSet<Gender> Gender { get; set; }
        public DbSet<Sector> Sector { get; set; }
        public DbSet<Education> Education { get; set; }
        public DbSet<Relationship> Relationship { get; set; }
        public DbSet<Zodiac> Zodiac { get; set; }
        public DbSet<Salary> Salary { get; set; }
        public DbSet<GroupMessages> GroupMessages { get; set; }

        public DbSet<FamilyValues> FamilyValues { get; set; }
        public DbSet<Driver> Driver { get; set; }
        public DbSet<Smoking> Smoking { get; set; }
        public DbSet<Work> Work { get; set; }
        public DbSet<Book> Book { get; set; }

        public DbSet<Hobbies> Hobbies { get; set; }
        public DbSet<UserHobbies> UserHobbies { get; set; }

        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Questions> Question { get; set; }

        public DbSet<Personality> Personality { get; set; }
        public DbSet<Message> Messages { get; set; }

        public DbSet<Stamp> Stamp { get; set; }
 
        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.Entity<AppUser>()
              .Property(b => b.Image)
              .HasDefaultValue("_default-male.svg");

            base.OnModelCreating(builder);
            builder.Entity<Message>()
                .HasOne(u => u.Recipient)
                .WithMany(m => m.MessagesReceived)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<GroupMessages>()
               .HasOne(u => u.Sender)
               .WithMany(m => m.GroupMessagesSender)
               .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<GroupMessages>()
               .HasOne(u => u.Recipient)
               .WithMany(m => m.GroupMessagesRecipient)
               .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Notification>()
               .HasOne(u => u.AppUser)
               .WithMany(m => m.NotificationsUsers)
               .OnDelete(DeleteBehavior.Cascade);


            builder.Entity<Notification>()
               .HasOne(u => u.Source)
               .WithMany(m => m.NotificationsSourceUsers)
               .OnDelete(DeleteBehavior.Cascade);


            builder.Entity<PublicMessage>()
               .HasOne(u => u.Sender)
               .WithMany(m => m.publicMessagesUser)
               .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Message>()
                .HasOne(u => u.Sender)
                .WithMany(m => m.MessagesSent)
                .OnDelete(DeleteBehavior.Cascade);

            builder.ApplyUtcDateTimeConverter();
        }

        private  string GetTableName(Type type)
        {
            var result = Regex.Replace(type.Name, ".[A-Z]", m => m.Value[0] + "_" + m.Value[1]);

            return result.ToLower();
        }
    }
    
    public static class UtcDateAnnotation
    {
        private const String IsUtcAnnotation = "IsUtc";
        private static readonly ValueConverter<DateTime, DateTime> UtcConverter =
          new ValueConverter<DateTime, DateTime>(v => v, v => DateTime.SpecifyKind(v, DateTimeKind.Utc));

        private static readonly ValueConverter<DateTime?, DateTime?> UtcNullableConverter =
          new ValueConverter<DateTime?, DateTime?>(v => v, v => v == null ? v : DateTime.SpecifyKind(v.Value, DateTimeKind.Utc));

        public static PropertyBuilder<TProperty> IsUtc<TProperty>(this PropertyBuilder<TProperty> builder, Boolean isUtc = true) =>
          builder.HasAnnotation(IsUtcAnnotation, isUtc);

        public static Boolean IsUtc(this IMutableProperty property) =>
          ((Boolean?)property.FindAnnotation(IsUtcAnnotation)?.Value) ?? true;

        /// <summary>
        /// Make sure this is called after configuring all your entities.
        /// </summary>
        public static void ApplyUtcDateTimeConverter(this ModelBuilder builder)
        {
            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                foreach (var property in entityType.GetProperties())
                {
                    if (!property.IsUtc())
                    {
                        continue;
                    }

                    if (property.ClrType == typeof(DateTime))
                    {
                        property.SetValueConverter(UtcConverter);
                    }

                    if (property.ClrType == typeof(DateTime?))
                    {
                        property.SetValueConverter(UtcNullableConverter);
                    }
                }
            }
        }
    }

}
