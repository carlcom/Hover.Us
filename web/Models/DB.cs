using Microsoft.Data.Entity;
using Microsoft.Framework.ConfigurationModel;

namespace VTSV.Models
{
    public class DB : DbContext
    {
        public DB(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        private IConfiguration configuration { get; }
        public DbSet<TagType> TagTypes { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<ImageTag> ImageTags { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<TagType>().ForSqlServer().Table("TagTypes");
            builder.Entity<Tag>().ForSqlServer().Table("Tags");
            builder.Entity<Image>().ForSqlServer().Table("Images");
            builder.Entity<ImageTag>().ForSqlServer().Table("ImageTags");

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