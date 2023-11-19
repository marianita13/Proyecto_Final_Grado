using Application.Repository;
using Domain.Entities;
using Domain.Interfaces;
using Persistence;
using Persistence.Data;

namespace Application.UnitOfWork;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly GardeningContext _context;
    private IUser _user;
    private IRol _rol;
    private ICity _city;
    private IClient _client;
    private ICountry _country;
    private IEmployee _employee;
    private IMethod_Payment _methodpay;
    private IOffice _office;
    private IOrder_detail _orderdetail;
    private IOrders _order;
    private IPayment _payment;
    private IPerson_Type _personType;
    private IPerson _person;
    private IPostal_Code _postalCode;
    private IProduct_line _productline;
    private IProduct _product;
    private IState _state;
    private IStatus _status;
    private ISupplier _supplier;
    private IBoss _Boss;
    public ICity Cities 
    {
        get
        {
            if (_city == null)
            {
                _city = new CityRepository(_context);
            }
            return _city;
        }
    }

    public IClient Clients 
    {
        get
        {
            if (_client == null)
            {
                _client = new ClientRepository(_context);
            }
            return _client;
        }
    }

    public ICountry Countries 
    {
        get
        {
            if (_country == null)
            {
                _country = new CountryRepository(_context);
            }
            return _country;
        }
    }

    public IEmployee Employees 
    {
        get
        {
            if (_employee == null)
            {
                _employee = new EmployeeRepository(_context);
            }
            return _employee;
        }
    }

    public IMethod_Payment Methods 
    {
        get
        {
            if (_methodpay == null)
            {
                _methodpay = new Method_PaymentRepository(_context);
            }
            return _methodpay;
        }
    }

    public IOffice Offices 
    {
        get
        {
            if (_office == null)
            {
                _office = new OfficeRepository(_context);
            }
            return _office;
        }
    }

    public IOrder_detail Details 
    {
        get
        {
            if (_orderdetail == null)
            {
                _orderdetail = new Order_detailRepository(_context);
            }
            return _orderdetail;
        }
    }

    public IOrders Orderse 
    {
        get
        {
            if (_order == null)
            {
                _order = new OrdersRepository(_context);
            }
            return _order;
        }
    }

    public IPayment Payments 
    {
        get
        {
            if (_payment == null)
            {
                _payment = new PaymentRepository(_context);
            }
            return _payment;
        }
    }

    public IPerson_Type PTypes 
    {
        get
        {
            if (_personType == null)
            {
                _personType = new Person_TypeRepository(_context);
            }
            return _personType;
        }
    }

    public IPerson Persons 
    {
        get
        {
            if (_person == null)
            {
                _person = new PersonRepository(_context);
            }
            return _person;
        }
    }

    public IPostal_Code PCodes 
    {
        get
        {
            if (_postalCode == null)
            {
                _postalCode = new Postal_CodeRepository(_context);
            }
            return _postalCode;
        }
    }

    public IProduct_line PLines 
    {
        get
        {
            if (_productline == null)
            {
                _productline = new Product_lineRepository(_context);
            }
            return _productline;
        }
    }

    public IProduct Products 
    {
        get
        {
            if (_product == null)
            {
                _product = new ProductRepository(_context);
            }
            return _product;
        }
    }

    public IState States 
    {
        get
        {
            if (_state == null)
            {
                _state = new StateRepository(_context);
            }
            return _state;
        }
    }

    public IStatus Status 
    {
        get
        {
            if (_status == null)
            {
                _status = new StatusRepository(_context);
            }
            return _status;
        }
    }

    public ISupplier Suppliers 
    {
        get
        {
            if (_supplier == null)
            {
                _supplier = new SupplierRepository(_context);
            }
            return _supplier;
        }
    }

    public IBoss Bosses 
    {
        get
        {
            if (_Boss == null)
            {
                _Boss = new BossRepository(_context);
            }
            return _Boss;
        }
    }

    public IUser Users {
        get
        {
            if (_user == null)
            {
                _user = new UserRepository(_context);
            }
            return _user;
        }
    }
    public IRol Roles {
        get
        {
            if (_rol == null)
            {
                _rol = new RolRepository(_context);
            }
            return _rol;
        }
    }


    public UnitOfWork(GardeningContext context)
    {
        _context = context;
    }
    


    public async Task<int> SaveAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
