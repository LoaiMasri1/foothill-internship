using AutoMapper;
using FluentValidation;
using RestaurantReservation.API.Services.Interfaces;
using RestaurantReservation.Contracts.Requests;
using RestaurantReservation.Contracts.Responses;
using RestaurantReservation.Db.Models;
using RestaurantReservation.Db.Repositories.Interfaces;

namespace RestaurantReservation.API.Services;

public class TableService : ITableService
{
    private readonly ITableRepository _tableRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<TableRequest> _validator;

    public TableService(
        ITableRepository tableRepository,
        IMapper mapper,
        IValidator<TableRequest> validator
    )
    {
        _tableRepository = tableRepository;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<TableResponse> CreateTableAsync(TableRequest tableRequest)
    {
        var validationResult = await _validator.ValidateAsync(tableRequest);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var newTable = _mapper.Map<Table>(tableRequest);

        await _tableRepository.CreateTableAsync(newTable);
        var response = _mapper.Map<TableResponse>(newTable);

        return response;
    }

    public async Task<TableResponse> UpdateTableAsync(int id, TableRequest tableRequest)
    {
        var validationResult = await _validator.ValidateAsync(tableRequest);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var updatedTable = _mapper.Map<Table>(tableRequest);

        var table = await _tableRepository.UpdateTableAsync(id, updatedTable);

        var response = _mapper.Map<TableResponse>(table);

        return response;
    }

    public async Task DeleteTableAsync(int id)
    {
        await _tableRepository.DeleteTableAsync(id);
    }
}
