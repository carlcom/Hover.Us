using Microsoft.Data.Entity;
using Microsoft.Framework.ConfigurationModel;

namespace web.Models
{
    public class DB : DbContext
    {
        public DbSet<Page> Pages { get; set; }

        private IConfiguration configuration { get; }
        public DbSet<TagType> TagTypes { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<ImageTag> ImageTags { get; set; }

        public DB(IConfiguration config)
        {
            configuration = config;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Page>().ToSqlServerTable("Pages");

            builder.Entity<TagType>().ToSqlServerTable("TagTypes");
            builder.Entity<Tag>().ToSqlServerTable("Tags");
            builder.Entity<Image>().ToSqlServerTable("Images");
            builder.Entity<ImageTag>().ToSqlServerTable("ImageTags");

            builder.Entity<TagType>().Collection(tt => tt.Tags).InverseReference(t => t.TagType).ForeignKey("Type_ID");
            builder.Entity<Tag>().Collection(t => t.ImageTags).InverseReference(it => it.Tag).ForeignKey("Tag_ID");
            builder.Entity<Image>().Collection(i => i.ImageTags).InverseReference(it => it.Image).ForeignKey("Image_ID");
            builder.Entity<ImageTag>().Key("Image_ID", "Tag_ID");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            var connectionString = configuration.Get("DB");
            options.UseSqlServer(connectionString);
        }
    }
}