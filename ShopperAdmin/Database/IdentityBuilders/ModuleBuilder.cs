﻿using Microsoft.EntityFrameworkCore;
 using Microsoft.EntityFrameworkCore.Metadata.Builders;
 using Shared.Mvc.Entities.Identity;

 namespace Shared.Database.ModelBuilders.IdentityBuilders
{
    public class ModuleBuilder : IEntityTypeConfiguration<Module>
    {
        public void Configure(EntityTypeBuilder<Module> builder)
        {
            builder.HasIndex(pg => pg.Name).IsUnique();
        }
    }
}