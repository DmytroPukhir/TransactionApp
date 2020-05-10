﻿using System;
using System.IO;
using System.Threading.Tasks;
using TransactionApp.Common.Exceptions;
using TransactionApp.DataAccess.DAL.UnitOfWork;
using TransactionApp.DomainModel.Models;
using TransactionApp.Services.Abstractions;
using TransactionApp.Services.Services.Transactions.Abstractions;

namespace TransactionApp.Services.Services
{
    public class TransactionsService : ITransactionsService
    {
        private readonly ITransactionsParserProvider _partImportParserProvider;
        private readonly IUnitOfWork _unitOfWork;

        public TransactionsService(ITransactionsParserProvider partImportParserProvider, IUnitOfWork unitOfWork)
        {
            _partImportParserProvider = partImportParserProvider;
            _unitOfWork = unitOfWork;
        }

        public async Task AddTransactionsAsync(Stream data)
        {

            using (var parser = await _partImportParserProvider.GetParserFor(data))
            {
                if (parser == null)
                {
                    throw new FormatException("Unknown Format");
                }

                var importedItems = await parser.ParseAllFileAsync(data);
                if (importedItems.ParseErrors.Count > 0)
                {
                  //  throw new BadRequestResponse(importedItems.ParseErrors);
                }
                else
                {
                    foreach (var item in importedItems.Transactions)
                    {
                        _unitOfWork.TransactionRepository.AddAsync(item);
                    }
                    
                }
                
            }
            _unitOfWork.SaveChanges();

        }
    }
}