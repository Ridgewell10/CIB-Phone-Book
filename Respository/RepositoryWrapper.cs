using Contracts;
using Infustructure.Models;

namespace Respository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private readonly CIBDbContext _repoContext;
        private IContactRepository _contact;
        public RepositoryWrapper(CIBDbContext repositoryContext)
        {
            _repoContext = repositoryContext;
        }
        public IContactRepository Contact
        {
            get
            {
                if (_contact == null)
                {
                    _contact = new ContactRepository(_repoContext);
                }

                return _contact;
            }
        }

    }
}
