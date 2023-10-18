using AutoMapper;
using FluentValidation;
using RestaurantReservation.API.Services.Interfaces;
using RestaurantReservation.Contracts.Requests;
using RestaurantReservation.Contracts.Responses;
using RestaurantReservation.Db.Models;
using RestaurantReservation.Db.Repositories.Interfaces;

namespace RestaurantReservation.API.Services;

public class MenuItemService : IMenuItemService
{
    private readonly IMenuItemRepository _menuItemRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<MenuItemRequest> _validator;

    public MenuItemService(
        IMenuItemRepository menuItemRepository,
        IMapper mapper,
        IValidator<MenuItemRequest> validator
    )
    {
        _menuItemRepository = menuItemRepository;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<MenuItemResponse> CreateMenuItemAsync(MenuItemRequest menuItemRequest)
    {
        var validationResult = await _validator.ValidateAsync(menuItemRequest);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var newMenuItem = _mapper.Map<MenuItem>(menuItemRequest);
        await _menuItemRepository.CreateMenuItemAsync(newMenuItem);

        var response = _mapper.Map<MenuItemResponse>(newMenuItem);

        return response;
    }

    public async Task<MenuItemResponse> UpdateMenuItemAsync(int id, MenuItemRequest menuItemRequest)
    {
        var validationResult = await _validator.ValidateAsync(menuItemRequest);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var updatedMenuItem = _mapper.Map<MenuItem>(menuItemRequest);

        var menuItem = await _menuItemRepository.UpdateMenuItemAsync(id, updatedMenuItem);

        var response = _mapper.Map<MenuItemResponse>(menuItem);

        return response;
    }

    public async Task DeleteMenuItemAsync(int id)
    {
        await _menuItemRepository.DeleteMenuItemAsync(id);
    }
}
