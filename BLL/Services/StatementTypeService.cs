using AutoMapper;
using BLL.DTO;
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
    public class StatementTypeService : IStatementTypeService
    {
        private readonly IMapper _mapper;
        readonly IUnitOfWork _unitOfWork;

        public StatementTypeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task Create(StatementTypeDTO statementTypeDTO)
        {
            var statementType = _mapper.Map<StatementTypeDTO, StatementType>(statementTypeDTO);
            await _unitOfWork.StatementTypes.CreateAsync(statementType);
            await _unitOfWork.SaveAsync();
        }

        public async Task Delete(StatementTypeDTO statementTypeDTO)
        {
            await _unitOfWork.StatementTypes.DeleteAsync(statementTypeDTO.Id);
            await _unitOfWork.SaveAsync();
        }

        public async Task<StatementTypeDTO> Get(int id)
        {
            var statementType = await _unitOfWork.StatementTypes.GetAsync(id);
            return _mapper.Map<StatementType,StatementTypeDTO>(statementType);
        }

        public async Task<IEnumerable<StatementTypeDTO>> GetAll()
        {
            var statementTypes = await _unitOfWork.StatementTypes.GetAllAsync();

            return _mapper.Map<IEnumerable<StatementType>, IEnumerable<StatementTypeDTO>>(statementTypes);
        }

        public async Task Update(StatementTypeDTO statementTypeDTO)
        {
            var statementType = await _unitOfWork.StatementTypes.GetAsync(statementTypeDTO.Id);

            statementType.MaxAmount = statementTypeDTO.MaxAmount;
            statementType.MinAmount = statementTypeDTO.MinAmount;
            statementType.MaxTerm = statementTypeDTO.MaxTerm;
            statementType.MinTerm = statementTypeDTO.MinTerm;
            statementType.Percentage = statementTypeDTO.Percentage;
            statementType.Name = statementTypeDTO.Name;

            await _unitOfWork.StatementTypes.UpdateAsync(statementType);
            await _unitOfWork.SaveAsync();
        }
    }
}
