using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
        
namespace Domain.Interfaces
{
    public interface IUnitOfWork
    {
        ICity Cities { get; }
        IClient Clients { get; }
        ICountry Countries { get; }
        IEmployee Employees { get; }
        IMethod_Payment Methods { get; }
        IOffice Offices { get; }
        IOrder_detail Details { get; }
        IOrders Orderse { get; }
        IPayment Payments { get; }
        IPerson_Type PTypes { get; }
        IPerson Persons { get; }
        IPostal_Code PCodes { get; }
        IProduct_line PLines { get; }
        IProduct Products { get; }
        IState States { get; }
        IStatus Status { get; }
        ISupplier Suppliers { get; }

        Task<int> SaveAsync();
    }
}
