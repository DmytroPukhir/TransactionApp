﻿using System.Collections.Generic;
using System.Threading.Tasks;
using TransactionApp.DomainModel.Models;

namespace TransactionApp.Services.Abstractions
{
    public interface ITransactionsProvider
    {
        Task<List<Transaction>> GetAllAsync();

        Task<List<Transaction>> GetFilteredAsync(string currencyCode,
                                            string startDate,
                                            string endDate,
                                            string status);
    }
}