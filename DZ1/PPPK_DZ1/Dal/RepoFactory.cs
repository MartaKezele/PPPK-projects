using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPPK_DZ1.Dal
{
    static class RepoFactory
    {
        private static readonly Lazy<IRepo> repo = new Lazy<IRepo>(() => new SqlRepo());
        public static IRepo GetRepo() => repo.Value;
    }
}
