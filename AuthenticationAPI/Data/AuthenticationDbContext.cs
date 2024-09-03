﻿using AuthenticationAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationAPI.Data
{
    public class AuthenticationDbContext:DbContext
    {
        public AuthenticationDbContext(DbContextOptions<AuthenticationDbContext> options):base(options)
        {
            
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<User>().HasKey(u => u.UserId);
            modelBuilder.Entity<Role>().HasKey(r => r.RoleId);
            modelBuilder.Entity<UserRole>().HasKey(ur => new { ur.UserId, ur.RoleId });



            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId);

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId);
        }
    }
}
