using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LawyerService.BL.Interfaces;
using LawyerService.DataAccess.Interfaces;
using LawyerService.Entities;
using LawyerService.ViewModel;

namespace LawyerService.BL
{
    public class LawyerManager : BaseManager, ILawyerManager
    {
        private readonly IUow _uow;
        private readonly IMapper _mapper;
        private readonly IValidator<LawyerVM> _validator;
        private readonly ILocalisationManager _localisationManager;

        public LawyerManager(IUow uow, IMapper mapper, IValidator<LawyerVM> validator, ILocalisationManager localisationManager)
        {
            _uow = uow;
            _mapper = mapper;
            _validator = validator;
            _localisationManager = localisationManager;
        }

        public async Task<ICollection<LawyerVM>> GetAllAsync()
        {
            var result = await  _uow.Lawyer.GetQueryable()
               .ToListAsync();
            return _mapper.Map<ICollection<LawyerVM>>(result);
        }

        public async Task<LawyerVM> GetByIDAsync(int lawyerId)
        {
            var data = await _uow.Lawyer.GetQueryable().Where(x => x.Id == lawyerId).FirstOrDefaultAsync();
            return _mapper.Map<LawyerVM>(data);
        }

    }
}
