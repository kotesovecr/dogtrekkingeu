using Grpc.Core;
using Microsoft.Extensions.Logging;
using Protos;
using System;
using System.IO.IsolatedStorage;
using System.Threading.Tasks;
using Storage.Interfaces.Entities;
using Storage.Interfaces.Services;

namespace DogtrekkingCzGRPCService.Services;

public class BeachConditionsService : BeachConditions.BeachConditionsBase
{
    private readonly ILogger<BeachConditionsService> _logger;
    private readonly IStorageService _storageService;
    
    public BeachConditionsService(ILogger<BeachConditionsService> logger, IStorageService storageService)
    {
        _logger = logger;
        _storageService = storageService;
    }

    public async override Task<GetAllBeachConditionResponse> getAllBeachConditions(GetAllBeachConditionRequest request, ServerCallContext context)
    {
        await _storageService.AddActionAsync(new AddActionRequest { Name = "test" });
        
        GetAllBeachConditionResponse result = new GetAllBeachConditionResponse();
        result.BeachConditions.Add(new BeachCondition { BeachId = 1, Name = "North Point", Condition = "Sandy" });
        result.BeachConditions.Add(new BeachCondition { BeachId = 2, Name = "South Point", Condition = "Murky" });

        return result;
    }

    public override Task<CreateBeachConditionResponse> createBeachCondition(CreateBeachConditionRequest request, ServerCallContext context)
    {
        BeachCondition beachCondition = new BeachCondition
        {
            BeachId = new Random().Next(),
            Name = request.Name,
            Condition = request.Condition
        };

        CreateBeachConditionResponse response = new CreateBeachConditionResponse
        {
            CreatedBeachCondition = beachCondition
        };

        return Task.FromResult(response);
    }
}