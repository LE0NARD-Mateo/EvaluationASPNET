using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EvaluationASPNET.Models;

namespace EvaluationASPNET.Data
{
    public class EvaluationASPNETContext : DbContext
    {
        public EvaluationASPNETContext (DbContextOptions<EvaluationASPNETContext> options)
            : base(options)
        {
        }

        public DbSet<EvaluationASPNET.Models.Ticket> Ticket { get; set; } = default!;

        public DbSet<EvaluationASPNET.Models.Home> Home { get; set; } = default!;
    }
}
