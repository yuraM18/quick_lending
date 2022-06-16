using AutoMapper;
using BLL.DTO;
using BLL.Helpers;
using BLL.Interfaces;
using DAL;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class PersonService : IPersonService
    {
        readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PersonService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task Create(PersonDTO person)
        {
            bool emailValid = EmailHelper.EmailValidate(person.Email);
            if (!emailValid)
            {
                new Exception("email not valid");
            }
            await _unitOfWork.People.CreateAsync(_mapper.Map<PersonDTO, Person>(person));
            await _unitOfWork.SaveAsync();
        }

        public async Task Delete(PersonDTO person)
        {
            await _unitOfWork.People.DeleteAsync(person.Id);
            await _unitOfWork.SaveAsync();
        }

        public async Task<PersonDTO> Get(int id)
        {
            var person = await _unitOfWork.People.GetAsync(id);
            return  _mapper.Map<Person, PersonDTO>(person);
        }

        public async Task<IEnumerable<PersonDTO>> GetAll()
        {
            var people = await _unitOfWork.People.GetAllAsync();
            return _mapper.Map<IEnumerable<Person>, IEnumerable<PersonDTO>>(people);
        }

        public async Task Update(PersonDTO person)
        {
            var _person = await _unitOfWork.People.GetAsync(person.Id);

            _person.Id = person.Id;
            _person.FirstName = person.FirstName;
            _person.LastName = person.LastName;
            _person.Phone = person.Phone;
            _person.IdNumber = person.IdNumber;
            _person.Email = person.Email;
            _person.Age = person.Age;
            _person.Tin = person.Tin;

            await _unitOfWork.People.UpdateAsync(_person);
            await _unitOfWork.SaveAsync();
        }
    }
}
