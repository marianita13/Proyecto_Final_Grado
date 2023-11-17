using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;
using Domain.Entities;

namespace API.profiles
{
    public class MappinProfiles : Profile
    {
        public MappinProfiles()
        {
            CreateMap<City, CityDto>().ReverseMap();

            CreateMap<Client, ClientDto>().ReverseMap();

            CreateMap<Country, CountryDto>().ReverseMap();

            CreateMap<Employee, EmployeeDto>().ReverseMap();

            CreateMap<MethodPayment, MethodPaymentDto>().ReverseMap();

            CreateMap<Office, OfficeDto>().ReverseMap();

            CreateMap<OrderDetail, OrderDetailDto>().ReverseMap();

            CreateMap<Order, OrderDto>().ReverseMap();

            CreateMap<Payment, PaymentDto>().ReverseMap();

            CreateMap<PersonType, PersonTypeDto>().ReverseMap();

            CreateMap<Person, PersonDto>().ReverseMap();

            CreateMap<PostalCode, PostalCodeDto>().ReverseMap();

            CreateMap<ProductLine, ProductLineDto>().ReverseMap();

            CreateMap<Product, ProductDto>().ReverseMap();

            CreateMap<State, StateDto>().ReverseMap();

            CreateMap<Status, StatusDto>().ReverseMap();

            CreateMap<Supplier, SupplierDto>().ReverseMap();

        }
    }
}