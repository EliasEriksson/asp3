using CD.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;


namespace CD.Data
{
    public class CdLibraryContext : DbContext
    {
        public CdLibraryContext(DbContextOptions<CdLibraryContext> options) : base(options)
        {
            
        }
        
        private class MissingCredentials : Exception
        {
        }

        private class DbCredentials
        {
            public DbCredentials(string? role, string? pwd, string? db, string? host)
            {
                Role = role;
                Pwd = pwd;
                Db = db;
                Host = host;
            }

            public string? Role { get; }
            public string? Pwd { get;  }
            public string? Db { get;  }
            public string? Host { get; }
        }

        private static readonly string CredentialsFile = Path.Combine(
            Environment.CurrentDirectory, ".credentials.json"
        );

        private static DbCredentials Load()
        {
            var credentials = JsonConvert.DeserializeObject<DbCredentials>(System.IO.File.ReadAllText(CredentialsFile));
            if (credentials == null)
            {
                throw new MissingCredentials();
            }

            return credentials;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var credentials = Load();
            optionsBuilder.UseNpgsql(
                $"Host={credentials.Host};" +
                $"Username={credentials.Role};" +
                $"Password={credentials.Pwd};" +
                $"Database={credentials.Db}"
            );
        }
        
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Cd> Cds { get; set; }
        public DbSet<User> Users { get; set; }
    }
}