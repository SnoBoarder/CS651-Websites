using System.Collections.Generic;

namespace WebAPIApplication.Model
{
    public interface IBugsRepository
    {
        IEnumerable<Bug> GetBugsRepo();
        void AddBug(Bug b);
        void DeleteBug(int id);
    }
}