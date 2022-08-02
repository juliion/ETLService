using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using ETLService.Models;
  
namespace ETLService.Transformers
{
    public class TransactionTransformer
    {
        public List<TransactionsByCity> Transform(List<Transaction> transactions)
        {
            List<TransactionsByCity> result = new List<TransactionsByCity>();
            List<string> cityNames = GetUniqueCities(transactions);
            foreach (var cityName in cityNames)
            {
                List<Transaction> transactionsByCityName = GetTransactionsByCity(transactions, cityName);
                List<string> serviceNames = GetUniqueServices(transactionsByCityName);
                Services services = GetServices(transactionsByCityName, serviceNames);
                TransactionsByCity transactionsByCity = new TransactionsByCity()
                {
                    City = cityName,
                    Services = services,
                    Total = CountTotalCity(services)
                };
                result.Add(transactionsByCity);
            }
            return result;
        }
        private List<string> GetUniqueCities(List<Transaction> transactions)
        {
            return transactions.Select(tr => tr.City).Distinct().ToList();
        }
        private List<string> GetUniqueServices(List<Transaction> transactions)
        {
            return transactions.Select(tr => tr.Service).Distinct().ToList();
        }
        private List<Transaction> GetTransactionsByCity(List<Transaction> transactions, string cityName)
        {
            return transactions.Where(tr => tr.City == cityName).ToList();
        }
        private List<Transaction> GetTransactionsByService(List<Transaction> transactions, string serviceName)
        {
            return transactions.Where(tr => tr.Service == serviceName).ToList();
        }
        private Services GetServices(List<Transaction> transactions, List<string> serviceNames)
        {
            Services services = new Services();
            foreach (var serName in serviceNames)
            {
                List<Transaction> transactionsByService = GetTransactionsByService(transactions, serName);
                Payers payers = GetPayers(transactionsByService);
                Service service = new Service()
                {
                    Name = serName,
                    Payers = payers,
                    Total = CountTotalService(payers)
                };
                services.Add(service);
            }
            return services;
        }
        private Payers GetPayers(List<Transaction> transactions)
        {
            Payers payers = new Payers();
            foreach (var transaction in transactions)
            {
                Payer payer = new Payer()
                {
                    Name = transaction.Name,
                    Payment = transaction.Payment,
                    Date = transaction.Date,
                    AccountNumber = transaction.AccountNumber
                };
                payers.Add(payer);
            }
            return payers;
        }
        private decimal CountTotalService(Payers payers)
        {
            return payers.Sum(p => p.Payment);
        }
        private decimal CountTotalCity(Services services)
        {
            return services.Sum(s => s.Total);
        }
    }
}
