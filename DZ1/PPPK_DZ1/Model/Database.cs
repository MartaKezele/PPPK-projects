using PPPK_DZ1.Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPPK_DZ1.Model
{
    class Database
    {
        private readonly Lazy<IEnumerable<DBEntity>> tables;
        private readonly Lazy<IEnumerable<DBEntity>> views;
        private readonly Lazy<IEnumerable<Procedure>> procedures;
        public Database()
        {
            tables = new Lazy<IEnumerable<DBEntity>>(() => RepoFactory.GetRepo().GetDBEntities(this, DBEntityType.Table));
            views = new Lazy<IEnumerable<DBEntity>>(() => RepoFactory.GetRepo().GetDBEntities(this, DBEntityType.View));
            procedures = new Lazy<IEnumerable<Procedure>>(() => RepoFactory.GetRepo().GetProcedures(this));
        }
        public IList<DBEntity> Tables
        {
            get => new List<DBEntity>(tables.Value);
        }
        public IList<DBEntity> Views
        {
            get => new List<DBEntity>(views.Value);
        }
        public IList<Procedure> Procedures
        {
            get => new List<Procedure>(procedures.Value);
        }
        public string Name { get; set; }
        public override string ToString() => Name;
    }
}
