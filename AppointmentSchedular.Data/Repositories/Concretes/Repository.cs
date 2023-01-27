using AppointmentSchedular.Core.Entities;
using AppointmentSchedular.Data.Context;
using AppointmentSchedular.Data.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentSchedular.Data.Repositories.Concretes
{
    public class Repository<T>:IRepository<T> where T : class,IEntityBase,new()
    {
        private readonly AppDbContext dbContext;

        public Repository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        private DbSet<T> Table { get=>dbContext.Set<T>(); }

        public async Task<List<T>> GetAllAsync(Expression<Func<T,bool>> predicate=null,params Expression<Func<T, object>>[] includeProperties) //ikinci sorguyu kullanabilmek için pradicate null olarak işaretlenmeli 
        {
            IQueryable<T> query = Table;
            if(predicate!= null)//include ile yeni bir nesne oluşturduğumuzda o nesnenin bağlı olduğu örneğin bir makaledeki bir resim birbrine bağlı iki sınıf ve resmin filename ini çekmek istiyorsak bu filename in nereden geleceğini bildirmemiz gerekir. Image i article in içine include etmem gerekir ki filename ini çekebilelim.
                query=query.Where(predicate);
            if(includeProperties.Any())
                foreach (var item in includeProperties)
                    query=query.Include(item);

            return await query.ToListAsync();                    
            
        }
        public async Task AddAsync(T entity)
        {
            await Table.AddAsync(entity);
        }

        public async Task<T> GetAsync(Expression<Func<T,bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = Table;
            query = query.Where(predicate);
            foreach (var item in includeProperties)
                query = query.Include(item);

            return await query.SingleAsync();
            
        }

        public async Task<T> GetByGuidAsync(Guid id)
        {
            return await Table.FindAsync(id);
        }

        public async Task<T> UpdateAsync(T entity)
        {
            await Task.Run(() => Table.Update(entity));
            return entity;
        }

        public async Task<T> DeleteAsync(T entity)
        {
            await Task.Run(() => Table.Remove(entity));
            return entity;
        }
    }
}
