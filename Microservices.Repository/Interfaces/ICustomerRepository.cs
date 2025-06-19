using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microservices.Core.Models;
namespace Microservices.Repository.Interfaces
{
    public interface ICustomerRepository
    {
        Task <IEnumerable<Customer>> GetAllCustomersAsync();
        Task <Customer?> GetCustomerByIdAsync(int id);
        Task AddCustomerAsync(Customer customer);
        Task UpdateCustomerAsync(Customer customer);
        Task DeleteCustomerAsync(int id);
    }
}
