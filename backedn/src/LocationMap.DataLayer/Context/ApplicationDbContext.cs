using LocationMap.DomainClasses;
using Microsoft.EntityFrameworkCore;

namespace LocationMap.DataLayer.Context
{
    public class ApplicationDbContext : DbContext, IUnitOfWork
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        { }

       
         public virtual DbSet<Location> Location { get; set; }
        public virtual DbSet<LocationType> LocationType { get; set; }
        

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // it should be placed here, otherwise it will rewrite the following settings!
            base.OnModelCreating(builder);

            
            builder.Entity<Location>(entity =>
            {
                entity.Property(e => e.Draggable).HasColumnName("draggable");

                entity.HasOne(d => d.LocationType)
                    .WithMany(p => p.Location)
                    .HasForeignKey(d => d.LocationTypeId)
                    .HasConstraintName("FK_Location_LocationType");
            });
            
        
        }
    }
}