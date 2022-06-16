using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using DAL;
using DAL.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IMapper _mapper;
        readonly IUnitOfWork _unitOfWork;

        public EmployeeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task Create(EmployeeDTO employee)
        {
            var people = await _unitOfWork.People.GetAllAsync();
            if (people.FirstOrDefault(x => x.Id == employee.Id) == null)
                throw new ValidationException("this person is NOT registered", employee.Id.ToString());
            var employees = await _unitOfWork.Employees.GetAllAsync();
            if (employees.FirstOrDefault(x => x.PeopleId == employee.PeopleId) != null)
                throw new ValidationException("this employee is registered", employee.PeopleId.ToString());

            var _employee = _mapper.Map<EmployeeDTO, Employee>(employee);
            await _unitOfWork.Employees.CreateAsync(_employee);
            await _unitOfWork.SaveAsync();
        }

        public async Task Delete(EmployeeDTO employee)
        {
            await _unitOfWork.Employees.DeleteAsync(employee.Id);
            await _unitOfWork.SaveAsync();                
        }

        public async Task<EmployeeDTO> Get(int id)
        {
            var employee = await _unitOfWork.Employees.GetAsync(id);
            return _mapper.Map<Employee, EmployeeDTO>(employee);
        }

        public async Task<IEnumerable<EmployeeDTO>> GetAll()
        {
            var employees = await _unitOfWork.Employees.GetAllAsync();
            return _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeDTO>>(employees);
        }
    }
}
