using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace todoapi.Models;

public partial class TododbContext : DbContext
{
    public TododbContext()
    {
    }

    public TododbContext(DbContextOptions<TododbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TbTodo> TbTodos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TbTodo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tb_todo__3214EC07922DDC24");

            entity.ToTable("tb_todo");

            entity.Property(e => e.Title).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
