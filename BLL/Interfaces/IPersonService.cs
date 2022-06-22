using BLL.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IPersonService
    {
        Task<IEnumerable<PersonDTO>> GetAll();
        Task<IEnumerable<PersonDTO>> GetMany(int currentPage, int itemsOnPage, bool? sortingDirection);
        Task<PersonDTO> Get(int id);
        Task Create(PersonDTO person);
        Task Update(PersonDTO person);
        Task Delete(PersonDTO person);
    }
}
