﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace Abc.Domain
{
    public interface IRepository<T>
    {
        Task<List<T>> Get();
        Task<T> Get(string id);
        Task Delete(string id);
        Task Add(T obj);
        Task Update(T obj);
        string SortOrder { get; set; }
        string SearchString { get; set; }
    }
}