using System;

namespace ChattingApp.Repository.Interfaces
{
    public interface IRepository<T>
    {
       
        T Remove(T instance);
       
        //bool AddAsync(T instance);
    }
}