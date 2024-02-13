using System.Data;

namespace NewWebApi.Models
{
    public class Result
    {
        public bool IsSuccess { get; set; }
        public DataTable DataTableResult { get; set; }
        public string ErrorMessage { get; set; }
    }
}