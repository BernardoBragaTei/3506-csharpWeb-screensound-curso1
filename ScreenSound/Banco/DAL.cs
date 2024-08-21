using Microsoft.EntityFrameworkCore;
using ScreenSound.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenSound.Banco
{
    internal class DAL<T> where T : class
    {
        protected readonly ScreenSoundContext _context;
        protected DbSet<T> _dbSet => _context.Set<T>();

        public DAL(ScreenSoundContext context)
        {
            _context = context;
        }

        public IEnumerable<T> List()
        {
            return _dbSet.ToList();
        }

        public void Add(T objeto)
        {
            _dbSet.Add(objeto);
            _context.SaveChanges();
        }

        public void Delete(T objeto)
        {
            _dbSet.Remove(objeto);
            _context.SaveChanges();
        }

        public void Update(T objeto)
        {
            _dbSet.Update(objeto);
            _context.SaveChanges();
        }

        public virtual T? RecoverBy(Func<T, bool> condition) 
        {
            return _dbSet.FirstOrDefault(condition);
        }

        public IEnumerable<T> SelectBy(Func<T, bool> condition)
        {
            return _dbSet.Where(condition);
        }
    }
}
