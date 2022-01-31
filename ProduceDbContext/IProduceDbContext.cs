using Microsoft.EntityFrameworkCore;
using ProduceDb.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProduceDb
{
    public interface IProduceDbContext
    {
        DbSet<Record> Records { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
