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
                throw new ValidationException("email valid", person.Email);
            if (person.Phone.Trim().Length != 10)
                throw new ValidationException("phone number != 10", person.Phone);
            if (person.IdNumber.Trim().Length != 9)
                throw new ValidationException("Id number not equal to 9", person.IdNumber);
            if (person.Age < 18 || person.Age > 90)
                throw new ValidationException("person age more then 90 or less 18", person.Age.ToString());

            var valid = await _unitOfWork.People.GetAllAsync();
            if (valid.FirstOrDefault(x => x.Email == person.Email) != null)
                throw new ValidationException("email NOT Unique", person.Email);
            if (valid.FirstOrDefault(x => x.IdNumber == person.IdNumber) != null)
                throw new ValidationException("Id number NOT Unique", person.IdNumber);
            if (valid.FirstOrDefault(x => x.Tin == person.Tin) != null)
                throw new ValidationException("TIN NOT Unique", person.Tin.ToString());
            if (valid.FirstOrDefault(x => x.Phone == person.Phone) != null)
                throw new ValidationException("Phone number NOT Unique", person.Phone);

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
            bool emailValid = EmailHelper.EmailValidate(person.Email);
            if (!emailValid)
                throw new ValidationException("email valid", person.Email);
            if (person.Phone.Trim().Length != 10)
                throw new ValidationException("phone number != 10", person.Phone);
            if (person.IdNumber.Trim().Length != 9)
                throw new ValidationException("Id number not equal to 9", person.IdNumber);
            if (person.Age < 18 || person.Age > 90)
                throw new ValidationException("person age more then 90 or less 18", person.Age.ToString());

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
