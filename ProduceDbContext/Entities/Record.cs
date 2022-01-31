using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProduceDb.Entities
{
    public record Record
    {
        [Key]
        public int Id { get; set; }

        public DateTime AddedDate { get; set; }

        public string Text { get; set; } = null!;

    }
}
