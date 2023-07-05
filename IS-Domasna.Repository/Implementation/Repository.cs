using IS_Domasna.Domain.DomainModels;
using IS_Domasna.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IS_Domasna.Repository.Implementation
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly ApplicationDbContext context;
        private readonly DbSet<T> entities;
        string errorMessage = string.Empty;

        public Repository(ApplicationDbContext context)
        {
            this.context = context;
            this.entities = context.Set<T>();
        }

        public void Delete(T entity)
        {

            if (entity != null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            entities.Remove(entity);
            context.SaveChanges();

            
        }

        public T Get(Guid id)
        {
            return entities.SingleOrDefault(e => e.Id == id);
        }

        public IEnumerable<T> GetAll()
        {
            return entities.AsEnumerable();
        }

        public void Insert(T entity)
        {
            if (entity != null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            entities.Add(entity);
            context.SaveChanges();
        }

        public void Update(T entity)
        {
            if (entity != null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            entities.Update(entity);
            context.SaveChanges();
        }
    }
}
