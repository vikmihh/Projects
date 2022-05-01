﻿using App.Domain;
using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface IItemCategoryRepository : IEntityRepository<ItemCategory>
{
    Task<IEnumerable<Card>> GetAllByFirstNameAsync(string firstName, bool noTracking = true);
}