namespace UserManagement.DAL.Models;

public class PaginationQueryModel
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 20;
}
