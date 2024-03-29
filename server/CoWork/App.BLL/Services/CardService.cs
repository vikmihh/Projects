﻿using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using App.DTO;
using Base.BLL;
using Base.Contracts;
using Card = App.BLL.DTO.Card;

namespace App.BLL.Services;

public class CardService
    : BaseEntityService<App.BLL.DTO.Card, App.DTO.Card, ICardRepository>, 
        ICardService
{
    public CardService(ICardRepository repository, IMapper<App.BLL.DTO.Card, App.DTO.Card> mapper) : base(repository, mapper)
    {
    }

    public async Task<IEnumerable<Card>> GetAvailableCardsForUser(Guid userId)
    {
        return (await Repository.GetAvailableCardsForUser(userId)).Select(x => Mapper.Map(x)!);
    }
    
    

    public async Task<IEnumerable<Card>> GetAllAsync(Guid userId, bool noTracking = true)
    {
        return (await Repository.GetAllAsync(userId, noTracking)).Select(x => Mapper.Map(x)!);
    }
}