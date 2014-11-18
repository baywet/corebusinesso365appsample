using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnowProCorp.DAL
{
    public class ProductionContext : DbContext
    {
        public ProductionContext():base("SnowProCorpDb")
        {
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
        }

        public DbSet<ProducedItem> ProducedItems { get; set; }
        public DbSet<ProducedItemType> ProducedItemsTypes { get; set; }
        public DbSet<ProductionFactory> ProductionFactories { get; set; }
        public DbSet<Shipment> Shipments { get; set; }
        public ProductionContext(string nameOrConnectionString) : base(nameOrConnectionString) { }
        public ProductionContext(DbConnection existingConnection, bool contextOwnsConnection) : base(existingConnection, contextOwnsConnection) { }
        public ProductionContext(ObjectContext objectContext, bool dbContextOwnsObjectContext) : base(objectContext, dbContextOwnsObjectContext) { }
        public ProductionContext(string nameOrConnectionString, DbCompiledModel model) : base(nameOrConnectionString, model) { }
        public ProductionContext(DbConnection existingConnection, DbCompiledModel model, bool contextOwnsConnection) : base(existingConnection, model, contextOwnsConnection) { }
    }
}
