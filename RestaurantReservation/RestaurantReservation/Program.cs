﻿using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Contracts.Requests;
using RestaurantReservation.Db;
using RestaurantReservation.Repositories;
using RestaurantReservation.Services;

class Program
{
    private static readonly RestaurantReservationDbContext context = new();

    private static readonly MenuItemRepository menuItemRepository = new(context);
    private static readonly ResturantRepository resturantRepository = new(context);
    private static readonly CustomerRepository customerRepository = new(context);
    private static readonly ReservationRepository reservationRepository = new(context);
    private static readonly TableRepository tableRepository = new(context);
    private static readonly EmployeeRepository employeeRepository = new(context);
    private static readonly OrderRepository orderRepository = new(context);
    private static readonly OrderItemRepository orderItemRepository = new(context);


    private static readonly MenuItemService menuItemService = new(menuItemRepository);
    private static readonly ResturantService resturantService = new(resturantRepository);
    private static readonly CustomerService customerService = new(customerRepository);
    private static readonly ReservationService reservationService = new(reservationRepository);
    private static readonly TableService tableService = new(tableRepository);
    private static readonly EmployeeService employeeService = new(employeeRepository);
    private static readonly OrderService orderService = new(orderRepository);
    private static readonly OrderItemService orderItemService = new(orderItemRepository);

    public async static Task Main()
    {
        bool wantToTest = false;
        if (wantToTest)
        {
            await TestCreatedService();
        }

        #region List All Managers

        var managers = await employeeService.ListManagersAsync();

        managers.ToList().ForEach(x => Console.WriteLine($"{x.Id},{x.FirstName},{x.LastName}"));

        #endregion

        #region Get Reservations By Customer
        var reservations = await reservationService.GetReservationsByCustomerAsync(1);

        reservations.ToList().ForEach(x => Console.WriteLine(
            $"{x.Id},{x.CustomerId},{x.RestaurantId},{x.TableId},{x.ReservationDate},{x.PartySize}"));

        #endregion

        #region List Orders And MenuItems
        var orders = await orderItemService.ListOrdersAndMenuItemsAsync(1);

        orders.ToList().ForEach(x => Console.WriteLine(
                       $@"
                          OrderId: {x.OrderId},
                          MenuItems: {x.MenuItems
                          .Aggregate("", (acc, x) => acc + $@"({x.Id},{x.Name},{x.RestaurantId})")}
                          Quantity: {x.Quantity}"));
        #endregion

        #region List Ordered MenuItems
        var orderedItems = await orderItemService.ListOrderedMenuItemsAsync(1);

        orderedItems.ToList().
            ForEach(x => Console.WriteLine($@"{x.Id},{x.Name},{x.RestaurantId},{x.Price}"));
        #endregion

        #region Calculate Average Order Amount
        var averageOrderAmount = await orderService.CalculateAverageOrderAmountAsync(1);
        Console.WriteLine($"Average: {averageOrderAmount}");
        #endregion

        #region Reservations View
        var view = context.ReservationsViews
            .FromSqlRaw("SELECT * FROM ReservationsView")
            .ToList();

        view.ForEach(x => Console.WriteLine(
            $@"{x.ReservationId},{x.ReservationDate},{x.PartySize},{x.CustomerId},{x.CustomerFirstName},{x.CustomerLastName},{x.CustomerEmail},{x.RestaurantId},{x.RestaurantName},{x.RestaurantAddress},{x.RestaurantPhoneNumber},{x.RestaurantOpeningHours}"
            ));
        #endregion


        #region Database Functions - Calculate Restaurant Revenue
        var revenue = resturantService.CalculateRestaurantRevenueAsync(1);
        Console.WriteLine($"Revenue: {revenue}");
        #endregion

        #region Stored Procedures - Find Customers With Large Parties
        var customers = customerService.GetCustomersWithLargeParties(3);

        customers.ToList().ForEach(x => Console.WriteLine(
                       $@"{x.Id},{x.FirstName},{x.LastName},{x.Email},{x.PhoneNumber}"));
        #endregion
    }

    private static async Task TestCreatedService()
    {
        var customerId = await CreateCustomerAsync();
        var resturantId = await CreateResturantAsync();
        var tableId = await CreateTableAsync(resturantId);
        var menuItemId = await CreateMenuItemAsync(resturantId);
        var employeeId = await CreateEmployeeAsync(resturantId);
        var reservationId = await CreateReservationAsync(customerId, tableId, resturantId);
        var orderId = await CreateOrderAsync(reservationId, employeeId);
        var orderItemId = await CreateOrderItemAsync(menuItemId, orderId);

        await Console.Out.WriteLineAsync(
            $@"
            Customer Id: {customerId}
            Resturant Id: {resturantId}
            Table Id: {tableId}
            Menu Item Id: {menuItemId}
            Employee Id: {employeeId}
            Reservation Id: {reservationId}
            Order Id: {orderId}
            Order Item Id: {orderItemId}
            ");
    }
    private async static Task<int> CreateOrderItemAsync(int menuItemId, int orderId)
    {
        var orderItemRequest = new OrderItemRequest(
            orderId,
            menuItemId,
            1);

        var orderItem = await orderItemService.CreateOrderItemAsync(orderItemRequest);

        return orderItem.Id;
    }
    private async static Task<int> CreateOrderAsync(int reservationId, int employeeId)
    {
        var orderRequest = new OrderRequest(
            employeeId,
            reservationId,
            DateTime.Now,
            10);

        var order = await orderService.CreateOrderAsync(orderRequest);

        return order.Id;

    }
    private async static Task<int> CreateReservationAsync(int customerId, int tableId, int resturantId)
    {
        var reservationRequest = new ReservationRequest(
            customerId,
            resturantId,
            tableId,
            DateTime.Now,
            4);

        var reservation = await reservationService.CreateReservationAsync(reservationRequest);

        return reservation.Id;
    }
    private async static Task<int> CreateEmployeeAsync(int resturantId)
    {
        var employeeRequest = new EmployeeRequest(
             resturantId,
             "Employee",
             "Console Employee Email",
             "Console");

        var employee = await employeeService.CreateEmployeeAsync(employeeRequest);
        return employee.Id;
    }
    private async static Task<int> CreateMenuItemAsync(int resturantId)
    {
        var menuItemRequest = new MenuItemRequest(
            resturantId,
            "Console Menu Item",
            "Console Menu Item Description",
            10.99m
            );

        var menuItem = await menuItemService.CreateMenuItemAsync(menuItemRequest);

        return menuItem.Id;

    }
    private async static Task<int> CreateTableAsync(int resturantId)
    {
        var tableRequest = new TableRequest(resturantId, 4);

        var table = await tableService.CreateTableAsync(tableRequest);

        return table.Id;
    }
    private async static Task<int> CreateResturantAsync()
    {
        var resturantRequest = new ResturantRequest(
            "Console Resturant",
            "Console Resturant Address",
            1234456789,
            "Console Resturant Email"
            );

        var resturant = await resturantService.CreateResturantAsync(resturantRequest);

        return resturant.Id;
    }
    private async static Task<int> CreateCustomerAsync()
    {
        var customerRequest = new CustomerRequest(
            "John",
            "Console",
            "console@test.com",
            1234567890
            );

        var customer = await customerService.CreateCustomerAsync(customerRequest);

        return customer.Id;
    }

}