using System.Data.Entity.Infrastructure;
using System.Linq;

namespace VTSV.Models
{
    public static class ContextExtensions
    {
        public static Image Find(this DbQuery<Image> query, int id)
        {
            return query.FirstOrDefault(i => i.ID == id);
        }

        public static Tag Find(this DbQuery<Tag> query, int id)
        {
            return query.FirstOrDefault(i => i.ID == id);
        }
    }
}