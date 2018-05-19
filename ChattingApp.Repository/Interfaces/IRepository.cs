﻿using System;

namespace ChattingApp.Repository.Interfaces
{
    public interface IRepository<T>
    {
        T Get(string id);
        T Remove(T instance);
        T Update(T instance);
        //bool Add(T instance);
    }
}