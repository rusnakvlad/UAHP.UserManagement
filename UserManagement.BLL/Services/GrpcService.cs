using Grpc.Net.Client;
using Microsoft.Extensions.Configuration;
using UserManagement.BLL.IServices;
using AdUserFeauresServer;

namespace UserManagement.BLL.Services;
public class GrpcService : IGrpcService
{
    private readonly IConfiguration configuration;

    public GrpcService(IConfiguration configuration) => this.configuration = configuration;

    public int GetUserCommentsCount(string userId)
    {
        var hostAdress = configuration["GrpcAdUserFeatures"];
        var chanel = GrpcChannel.ForAddress(hostAdress);
        var client = new GrpcAdUserFeatures.GrpcAdUserFeaturesClient(chanel);
        var request = new GetCommentsCountRequest { UserId = userId };
        var result = client.GetUserCommentsCount(request);
        return result.Count;
    }
}
