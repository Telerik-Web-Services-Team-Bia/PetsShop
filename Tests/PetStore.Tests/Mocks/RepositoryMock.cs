namespace PetStore.Tests.Mocks
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using PetStore.Data.Repositories;

    public class RepositoryMock<T> : IRepository<T>
        where T : class
    {
        private readonly IList<T> data;

        public RepositoryMock()
        {
            this.data = new List<T>();
            this.AttachedEntities = new List<T>();
            this.DetachedEntities = new List<T>();
            this.UpdatedEntities = new List<T>();
        }

        public IList<T> AttachedEntities { get; private set; }

        public IList<T> DetachedEntities { get; private set; }

        public IList<T> UpdatedEntities { get; set; }

        public bool IsDisposed { get; private set; }

        public int SavesCount { get; private set; }

        public void Add(T entity)
        {
            this.data.Add(entity);
        }

        public IQueryable<T> All()
        {
            return this.data.AsQueryable();
        }

        public T GetById(object id)
        {
            if (this.data.Count == 0)
            {
                throw new InvalidOperationException("No data.");
            }

            return this.data[0];
        }

        public T Attach(T entity)
        {
            this.AttachedEntities.Add(entity);
            return entity;
        }
   
        public void Detach(T entity)
        {
            this.DetachedEntities.Add(entity);
        }

        public void Update(T entity)
        {
            this.UpdatedEntities.Add(entity);
        }

        public void Delete(object id)
        {
            if (this.data.Count == 0)
            {
                throw new InvalidOperationException("No data.");
            }

            this.data.RemoveAt(this.data.Count - 1);
        }

        public void Delete(T entity)
        {
            if (!this.data.Contains(entity))
            {
                throw new InvalidOperationException("Entity not found.");
            }

            this.data.Remove(entity);
        }

        public int SaveChanges()
        {
            this.SavesCount += 1;
            return 1;
        }

        public void Dispose()
        {
            this.IsDisposed = true;
        }
    }
}
