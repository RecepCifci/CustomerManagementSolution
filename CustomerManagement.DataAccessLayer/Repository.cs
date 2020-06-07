using CustomerManagement.Core.DataAccess;
using CustomerManagement.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using CustomerManagement.Entities.Base;
using CustomerManagement.Common;
using System.Data.Entity.Validation;

namespace CustomerManagement.DataAccessLayer
{
    public class Repository<T> : RepositoryBase, IDataAccess<T> where T : class
    {
        private DbSet<T> _objectSet;

        public Repository()
        {
            _objectSet = context.Set<T>();
        }

        public List<T> List()
        {
            return _objectSet.ToList();
        }

        public IQueryable<T> ListQueryable()
        {
            return _objectSet.AsQueryable<T>();
        }

        public List<T> List(Expression<Func<T, bool>> where)
        {
            return _objectSet.Where(where).ToList();
        }

        public int Insert(T obj)
        {
            _objectSet.Add(obj);

            if (obj is EntityBase)
            {
                EntityBase o = obj as EntityBase;
                DateTime now = DateTime.Now;

                o.CreatedOn = now;
                o.ModifiedOn = now;
                o.ModifiedUserName = App.Common.GetCurrentUsername();
            }

            return Save();
        }

        public int Update(T obj)
        {
            if (obj is EntityBase)
            {
                EntityBase o = obj as EntityBase;
                DateTime now = DateTime.Now;

                o.ModifiedOn = now;
                o.ModifiedUserName = App.Common.GetCurrentUsername();
            }

            return Save();
        }

        public int Delete(T obj)
        {
            //if (obj is MyEntityBase)
            //{
            //    MyEntityBase o = obj as MyEntityBase;

            //    o.ModifiedOn = DateTime.Now;
            //    o.ModifiedUsername = App.Common.GetUsername();
            //}

            _objectSet.Remove(obj);
            return Save();
        }

        public int Save()
        {
            try
            {
                return context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
        }

        public T Find(Expression<Func<T, bool>> where)
        {
            return _objectSet.FirstOrDefault(where);
        }
    }
}
