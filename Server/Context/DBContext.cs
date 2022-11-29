using LCPMauiWebApi.Server.Classes;
using Microsoft.EntityFrameworkCore;

namespace LCPMauiWebApi.Server.Context
{
    public class DBContext : DbContext
    {
        private readonly IConfiguration iconfig;
        public int DBMode = 0;
        public virtual DbSet<MyUsers> MyUsers { get; set; }
        public virtual DbSet<MyUsersAuth> MyUsersAuth { get; set; }
        public virtual DbSet<Posts> Posts { get; set; }
        public virtual DbSet<Attachments> Attachments { get; set; }
        public virtual DbSet<Comments> Comments { get; set; }
        public virtual DbSet<Replies> Replies { get; set; }
        public virtual DbSet<Reactions> Reactions { get; set; }
        public virtual DbSet<Feedback> Feedbacks { get; set; }
        public virtual DbSet<Todo> Todo { get; set; }

        public DBContext(
            DbContextOptions<DBContext> options, 
            IConfiguration _iconfig) : base(options)
        {
            iconfig = _iconfig;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfiguration constrings = iconfig.GetSection("ConnectionStrings");

                if (DBMode == 0)
                {
                    optionsBuilder.UseSqlServer(constrings["lcpdb_sqlserver"]);
                }
                else
                {
                    optionsBuilder.UseSqlite(constrings["lcpdb_sqlite"]);
                }
            }
        }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            mb.Seed();
        }
    }
}
