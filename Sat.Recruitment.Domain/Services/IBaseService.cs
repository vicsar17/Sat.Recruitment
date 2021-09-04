﻿using Sat.Recruitment.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Sat.Recruitment.Domain.Services
{
    public interface IBaseService<T> where T : BaseEntity
    {

        Task<IEnumerable<T>> GetAsync();

        Task<T> GetById(int id);

        IEnumerable<T> Where(Expression<Func<T, bool>> exp);

        void AddOrUpdate(T entry);

        void Remove(int id);

    }
}