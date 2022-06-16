using BLL.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeDTO>> GetAll();
        Task<EmployeeDTO> Get(int id);
        Task Create(EmployeeDTO employee);
        Task Delete(EmployeeDTO employee);
    }
}
