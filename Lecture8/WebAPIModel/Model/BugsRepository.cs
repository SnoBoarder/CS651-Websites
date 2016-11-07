using System.Collections.Generic;

namespace WebAPIApplication.Model
{
    public class BugsRepository : IBugsRepository
    {
        private static readonly List<Bug> _repo = new List<Bug>();

        public BugsRepository()
        {
            _repo.Add(new Bug { id = 1, title = "the mother of all bugs",
                                description = "this bug ate my sandwich!",
                                state = "open"});
        }

        public IEnumerable<Bug> GetBugsRepo()
        {
            return _repo;
        }
        public void AddBug(Bug b)
        {
            _repo.Add(b);
        }
        public void DeleteBug(int id)
        {
            _repo.RemoveAt(id);
        }
    }

}