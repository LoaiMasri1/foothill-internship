﻿using AutoMapper;
using RestaurantReservation.Contracts.Requests;
using RestaurantReservation.Contracts.Responses;
using RestaurantReservation.Db.Models;
using RestaurantReservation.Db.Repositories;

namespace RestaurantReservation.Services;

public class TableService
{
    private readonly TableRepository _tableRepository;
    private readonly IMapper _mapper;

    public TableService(TableRepository tableRepository,IMapper mapper)
    {
        _tableRepository = tableRepository;
        _mapper = mapper;
    }

    public async Task<TableResponse> CreateTableAsync(TableRequest tableRequest)
    {
        var newTable = _mapper.Map<Table>(tableRequest);

        await _tableRepository.CreateTableAsync(newTable);
        var response = _mapper.Map<TableResponse>(newTable);

        return response;
    }

    public async Task<TableResponse> UpdateTableAsync(int id, TableRequest tableRequest)
    {
        var updatedTable =_mapper.Map<Table>(tableRequest);

        var table = await _tableRepository
            .UpdateTableAsync(id, updatedTable);

        var response = _mapper.Map<TableResponse>(table);

        return response;
    }

    public async Task DeleteTableAsync(int id)
    {
        await _tableRepository.DeleteTableAsync(id);    
    }
}
