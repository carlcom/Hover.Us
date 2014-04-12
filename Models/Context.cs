using System.Data.Entity;

namespace VTSV.Models
{
    public sealed class DB : DbContext
    {
        public DB()
            : base("name=DB")
        {
        }

        // ReSharper disable UnusedAutoPropertyAccessor.Global
        public DbSet<Image> Images { get; set; }
        public DbSet<TagType> TagTypes { get; set; }
        public DbSet<Tag> Tags { get; set; }
        // ReSharper restore UnusedAutoPropertyAccessor.Global

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TagType>().HasMany(t => t.Tags).WithRequired(t => t.Type);
            modelBuilder.Entity<Image>().HasMany(i => i.Tags).WithMany(t => t.Images).Map(m => m.ToTable("ImageTags"));
        }
    }
}
