using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using SPMS.Application.Common.Interfaces;
using SPMS.Application.Dtos.Story;
using SPMS.ViewModel.Story;

namespace SPMS.Application.Common.Resolvers
{
    public class IdHiderResolver : IValueResolver<StoryPostDto, StoryPostViewModel, string>
    {
        private readonly IIdentifierMask _masker;

        public IdHiderResolver(IIdentifierMask masker)
        {
            _masker = masker;
        }  

        public string Resolve(StoryPostDto source, StoryPostViewModel destination, string destMember, ResolutionContext context)
        {
            return _masker.HideId(source.Id);
        }
    }

    public class IdRevealerResolver : IValueResolver<StoryPostViewModel, StoryPostDto, int>
    {
        private readonly IIdentifierMask _masker;

        public IdRevealerResolver(IIdentifierMask masker)
        {
            _masker = masker;
        }

        public int Resolve(StoryPostViewModel source, StoryPostDto destination, int destMember, ResolutionContext context)
        {
            return _masker.RevealId(source.Id);
        }
    }
}
