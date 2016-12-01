using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNet.Identity;

namespace Smart.Service
{
    public interface IService<T>
    {
        T Get(string id);
        T Remove(T instance);
        T Update(T instance);
        //IdentityResult Add(T instance);
    }
}