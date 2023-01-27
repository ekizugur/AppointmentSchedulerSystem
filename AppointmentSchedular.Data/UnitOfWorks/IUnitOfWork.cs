using AppointmentSchedular.Core.Entities;
using AppointmentSchedular.Data.Repositories.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentSchedular.Data.UnitOfWorks
{
    public interface IUnitOfWork :IAsyncDisposable,IDisposable
    {
        IRepository<T> GetRepository<T>() where T : class,IEntityBase,new();
        Task<int> SaveAsync();
        int Save();//asenkron olarak kullanamazsam diye oluşturdum.
    }
}
