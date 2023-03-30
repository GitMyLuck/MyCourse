

using System.Data;

namespace MyCourse.Models.Services.Infrastructure
{
    public interface IDBAccess
    {
        DataSet Query(string query);
    }
}