using System;

namespace ChattingApp.Repository.Interfaces
{
    public interface IRepository<T>
    {
       
        T Remove(T instance);
        T UpdateAsync(T instance);
        //bool Add(T instance);
    }
}