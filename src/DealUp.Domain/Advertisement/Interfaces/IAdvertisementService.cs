﻿using DealUp.Domain.Common;

namespace DealUp.Domain.Advertisement.Interfaces;

public interface IAdvertisementService
{
    public Task<PagedResponse<Advertisement>> GetAllAdvertisementsAsync(PaginationParameters pagination);
    public Task<Advertisement> CreateAdvertisementAsync(Guid userId, CreateAdvertisementRequest creationRequest);
}