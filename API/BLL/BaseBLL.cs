using AutoMapper;
using DataLayer.Repositories;

namespace API.BLL
{
    public class BaseBLL
    {
        protected readonly UnitOfWork _unitOfWork;
        protected readonly IMapper _mapper;
        public BaseBLL(UnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
    }
}
