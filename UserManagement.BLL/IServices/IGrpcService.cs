using Microsoft.Extensions.Configuration;

namespace UserManagement.BLL.IServices;
public interface IGrpcService
{
    int GetUserCommentsCount(string userId);
}
