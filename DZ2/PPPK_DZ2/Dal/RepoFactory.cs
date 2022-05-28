using System;

namespace PPPK_DZ2.Dal
{
    static class RepoFactory
    {
        private static readonly Lazy<IRepo> repository = new Lazy<IRepo>(() => new SqlRepo());
        public static IRepo GetRepo() => repository.Value;
    }
}
