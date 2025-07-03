namespace Realtime_D3.GRPC.Services;
using Grpc.Core;
using Insertion;
using Notification;
using Realtime_D3.GRPC.Data;
using RealTime_D3.GRPC.Repository;

public class InsertionService : InsertService.InsertServiceBase
{
    private readonly ILogger<InsertionService> _logger;
    private readonly ITbllogRepository _tbllogRepository;

    public InsertionService(ILogger<InsertionService> logger, ITbllogRepository TbllogRepository)
    {
            _logger = logger;
        _tbllogRepository = TbllogRepository;
    }
    public override async Task<StatusResponse> InsertEntry(
        InsertRequest request,
        ServerCallContext context)
    {
        try
        {
            // Logique d'insertion dans la DB
            await _tbllogRepository.AddAsync(new Tbllog
            {
            
                Detail = request.Detail,
                Value = request.Value
            });

            // Retourne un succès
            return new StatusResponse
            {
                Code = 200,
                BodyMessage = "Insertion successful",
                Detail = $"Inserted entry ID: {request.Id}"
            };
        }
        catch (Exception ex)
        {
            // Retourne une erreur en cas d'exception
            return new StatusResponse
            {
                Code = 500,
                BodyMessage = "Database error",
                Detail = $"Exception: {ex.Message}"
            };
        }
    }
}

