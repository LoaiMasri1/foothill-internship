using AutoMapper;
using RestaurantReservation.Contracts.Requests;
using RestaurantReservation.Contracts.Responses;
using RestaurantReservation.Db.Models;

namespace RestaurantReservation;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<CustomerRequest, Customer>();
        CreateMap<CustomerResponse, Customer>().ReverseMap();

        CreateMap<EmployeeRequest, Employee>();
        CreateMap<EmployeeResponse, Employee>().ReverseMap();

        CreateMap<OrderRequest, Order>();
        CreateMap<OrderResponse, Order>().ReverseMap();

        CreateMap<OrderItemRequest, OrderItem>();
        CreateMap<OrderItemResponse, OrderItem>().ReverseMap();

        CreateMap<ReservationRequest, Reservation>();
        CreateMap<ReservationResponse, Reservation>().ReverseMap();

        CreateMap<MenuItemRequest, MenuItem>();
        CreateMap<MenuItemResponse, MenuItem>().ReverseMap();

        CreateMap<ResturantRequest, Resturant>();
        CreateMap<ResturantResponse, Resturant>().ReverseMap();
    }
}
