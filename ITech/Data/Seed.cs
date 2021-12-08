using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITech.Data
{
    public class Seed
    {
        public int Id { get; set; }
        public string NameOfSeedType { get; set; }
        public int DesiredSeed { get; set; }
        public bool Seeded { get; set; }
        public int SeedAttempts { get; set; }
    }
}
