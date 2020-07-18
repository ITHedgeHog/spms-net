using AutoMapper;
using SPMS.Application.Common.Interfaces;
using SPMS.Common.BaseObjects;
using SPMS.ViewModel;

namespace SPMS.Application.Common.Resolvers
{
    public class VmToQueryIdResolver : IValueResolver<ViewModelWithId, QueryWithId , int>
    {
        private readonly IIdentifierMask _masker;

        public VmToQueryIdResolver(IIdentifierMask masker)
        {
            _masker = masker;
        }

        public int Resolve(ViewModelWithId source, QueryWithId destination, int destMember, ResolutionContext context)
        {
            return _masker.RevealId(source.Id);
        }
    }
}