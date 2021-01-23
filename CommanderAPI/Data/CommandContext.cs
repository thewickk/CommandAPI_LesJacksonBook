using Microsoft.EntityFrameworkCore;
using CommanderAPI.Models;

namespace CommanderAPI.Data
{
    public class CommandContext : DbContext
    {
        public CommandContext(DbContextOptions<CommandContext> options) : base(options)
        {
        }

        public DbSet<Command> CommandItems { get; set; }
    }
}
