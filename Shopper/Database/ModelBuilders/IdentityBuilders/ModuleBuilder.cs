﻿using Microsoft.EntityFrameworkCore;
 using Microsoft.EntityFrameworkCore.Metadata.Builders;
 using Shopper.Mvc.Entities.Identity;

 namespace Shopper.Database.ModelBuilders.IdentityBuilders
{
    public class ModuleBuilder : IEntityTypeConfiguration<Module>
    {
        public void Configure(EntityTypeBuilder<Module> builder)
        {
            builder.HasIndex(pg => pg.Name).IsUnique();
        }
    }
}
