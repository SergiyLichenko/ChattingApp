using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Smart.Models.Entities;

namespace Smart.Repository
{
    public interface IRepository<T>:IDisposable
    {
        T Get(string id);
        T Remove(T instance);
        T Update(T instance);
        //bool Add(T instance);
    }
}