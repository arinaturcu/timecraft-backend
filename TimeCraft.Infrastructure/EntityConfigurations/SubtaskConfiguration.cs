﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TimeCraft.Core.Entities;

namespace TimeCraft.Infrastructure.EntityConfigurations;

public class SubtaskConfiguration : IEntityTypeConfiguration<Subtask>
{
    public void Configure(EntityTypeBuilder<Subtask> builder)
    {
        builder.Property(e => e.Id)
            .IsRequired();
        builder.HasKey(x => x.Id);
        builder.Property(e => e.Name)
            .HasMaxLength(255)
            .IsRequired();
        builder.Property(e => e.Description)
            .HasMaxLength(4095)
            .IsRequired(false); // This specifies that this column can be null in the database.
        builder.Property(e => e.CreatedAt)
            .IsRequired();
        builder.Property(e => e.UpdatedAt)
            .IsRequired();

        builder.HasOne(e => e.User) // This specifies a one-to-many relation.
            .WithMany(e => e.Subtasks) // This provides the reverse mapping for the one-to-many relation.
            .HasForeignKey(e => e.UserId) // Here the foreign key column is specified.
            .HasPrincipalKey(e => e.Id) // This specifies the referenced key in the referenced table.
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade); // This specifies the delete behavior when the referenced entity is removed.
        
        builder.HasOne(e => e.Project)
            .WithMany(e => e.Subtasks)
            .HasForeignKey(e => e.ProjectId)
            .HasPrincipalKey(e => e.Id)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}