﻿using API.Models;

namespace API.Repository
{
    public interface IGeneralRepository<TEntity, TKey>
    {
        IEnumerable<TEntity> GetAll();
        TEntity GetByKey(TKey key);
        int Insert(TEntity entity);
        int Update(TEntity entity);
        int Delete(TKey key);
    }
}
