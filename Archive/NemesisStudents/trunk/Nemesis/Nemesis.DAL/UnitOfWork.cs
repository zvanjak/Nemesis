using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Nemesis.Domain;
using Nemesis.Domain.Security;

namespace Nemesis.DAL
{
  public class UnitOfWork : IDisposable
  {
    private readonly NemesisContext _context = new NemesisContext();
    
    private GenericRepository<Team> _teamRepository;
    private GenericRepository<IterationObjective> _iterationObjectiveRepository;
    private GenericRepository<Role> _roleRepository;
    private GenericRepository<User> _userRepository;
		private GenericRepository<UserAction> _userActionRepository;

    public GenericRepository<Team> TeamRepository
    {
      get 
      {
        if (this._teamRepository == null) 
        {
          this._teamRepository = new GenericRepository<Team>(_context);
        }
        return _teamRepository;
      }
    }

    public GenericRepository<IterationObjective> IterationObjectiveRepository
    {
      get
      {
        if (this._iterationObjectiveRepository == null) 
        {
          this._iterationObjectiveRepository = new GenericRepository<IterationObjective>(_context);
        }
        return this._iterationObjectiveRepository;
      }
    }

    public GenericRepository<Role> RoleRepository
    {
      get
      {
        if (this._roleRepository == null)
        {
          this._roleRepository = new GenericRepository<Role>(_context);
        }
        return this._roleRepository;
      }
    }

    public GenericRepository<User> UserRepository
    {
      get
      {
        if (this._userRepository == null)
        {
          this._userRepository = new GenericRepository<User>(_context);
        }
        return this._userRepository;
      }
    }

		public GenericRepository<UserAction> UserActionRepository
		{
			get
			{
				if (this._userActionRepository == null)
				{
					this._userActionRepository = new GenericRepository<UserAction>(_context);
				}
				return this._userActionRepository;
			}
		}

    public void Save()
    {
      _context.SaveChanges();
    }

    private bool _disposed = false;

    protected virtual void Dispose(bool disposing)
    {
      if (!this._disposed)
      {
        if (disposing)
        {
          _context.Dispose();
        }
      }
      this._disposed = true;
    }

    public void Dispose()
    {
      Dispose(true);
      GC.SuppressFinalize(this);
    }
  }

}
