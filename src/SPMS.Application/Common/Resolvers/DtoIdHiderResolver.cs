using AutoMapper;
using SPMS.Application.Common.Interfaces;
using SPMS.Application.Dtos.Common;
using SPMS.ViewModel;
using NotImplementedException = System.NotImplementedException;

namespace SPMS.Application.Common.Resolvers
{
    public class DtoIdHiderResolver : IValueResolver<DtoWithId, ViewModelWithId, string>
    {
        private readonly IIdentifierMask _masker;

        public DtoIdHiderResolver(IIdentifierMask masker)
        {
            _masker = masker;
        }

        public string Resolve(DtoWithId source, ViewModelWithId destination, string destMember, ResolutionContext context)
        {
            return _masker.HideId(source.Id);
        }
    }
}