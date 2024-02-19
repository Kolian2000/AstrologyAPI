using NewWebApi.Models;
using NewWebApi.Models.Enum;
using Npgsql;

namespace NewWebApi.Interface
{
    public interface IReposi
    {
         public Task<Result> Request(NpgsqlCommand comand, TypeOfComand comandType);
    }
}