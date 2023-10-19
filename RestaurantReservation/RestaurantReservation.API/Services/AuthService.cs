using AutoMapper;
using RestaurantReservation.API.Services.Interfaces;
using RestaurantReservation.Db.Models;

namespace RestaurantReservation.API.Services;

public class AuthService : IAuthService
{
    private readonly ICustomerService _customerService;
    private readonly IJwtProvider _jwtProvider;
    private readonly IMapper _mapper;

    public AuthService(
        ICustomerService customerService,
        IJwtProvider jwtProvider,
        IMapper mapper)
    {
        _customerService = customerService;
        _jwtProvider = jwtProvider;
        _mapper = mapper;
    }

    public async Task<AuthResponse> LoginAsync(LoginRequest request)
    {
        var customerExists = await _customerService.GetCustomerByEmailAsync(request.Email);
        var errorMessage = "email or password incorrect";

        if (customerExists is null)
        {
            throw new UnauthorizedAccessException(errorMessage);
        }
        if (!request.Password.Equals(customerExists.Password))
        {
            throw new UnauthorizedAccessException(errorMessage);
        }

        var customer = _mapper.Map<Customer>(customerExists);
        var token = _jwtProvider.Generate(customer);

        return new AuthResponse(token);
    }
}