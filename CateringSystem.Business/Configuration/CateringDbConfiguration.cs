using CateringSystem.Business.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CateringSystem.Business.Configuration
{
    public class CateringDbConfiguration : EntityTypeConfiguration<Package>
    {
        public CateringDbConfiguration()
        {

    }
}
}
