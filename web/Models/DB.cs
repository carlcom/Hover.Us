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

            builder.Entity<TagType>().Collection(tt => tt.Tags).InverseReference(t => t.TagType).ForeignKey("Type_ID");
            builder.Entity<Tag>().Collection(t => t.ImageTags).InverseReference(it => it.Tag).ForeignKey("Tag_ID");
            builder.Entity<Image>().Collection(i => i.ImageTags).InverseReference(it => it.Image).ForeignKey("Image_ID");
            builder.Entity<ImageTag>().Key("Image_ID", "Tag_ID");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer("Server=server;Database=www;Integrated Security=true;MultipleActiveResultSets=true");
        }
    }
}