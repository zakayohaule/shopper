﻿using Microsoft.EntityFrameworkCore;
 using Microsoft.EntityFrameworkCore.Metadata.Builders;
 using ShopperAdmin.Mvc.Entities.Identity;

 namespace ShopperAdmin.Database.ModelBuilders.IdentityBuilders
{
    public class ModuleBuilder : IEntityTypeConfiguration<Module>
    {
        public void Configure(EntityTypeBuilder<Module> builder)
        {
            builder.HasIndex(pg => pg.Name).IsUnique();
        }
    }
}
