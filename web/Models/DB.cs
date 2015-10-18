using Microsoft.Data.Entity;

namespace Web.Models
{
    public class DB : DbContext
    {
        public DbSet<Page> Pages { get; set; }

        public DbSet<TagType> TagTypes { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<ImageTag> ImageTags { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Page>().ToTable("Pages");

            builder.Entity<TagType>().ToTable("TagTypes");
            builder.Entity<Tag>().ToTable("Tags");
            builder.Entity<Image>().ToTable("Images");
            builder.Entity<ImageTag>().ToTable("ImageTags");

            builder.Entity<TagType>().HasMany(tt => tt.Tags).WithOne(t => t.TagType).ForeignKey("Type_ID");
            builder.Entity<Tag>().HasMany(t => t.ImageTags).WithOne(it => it.Tag).ForeignKey("Tag_ID");
            builder.Entity<Image>().HasMany(i => i.ImageTags).WithOne(it => it.Image).ForeignKey("Image_ID");
            builder.Entity<ImageTag>().HasKey("Image_ID", "Tag_ID");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer("Server=server;Database=www;Integrated Security=true;MultipleActiveResultSets=true");
        }
    }
}