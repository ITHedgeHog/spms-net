using System;
using System.Linq;
using System.Security.Cryptography.Xml;
using AutoMapper;
using SPMS.Web.Models;
using SPMS.Web.ViewModels;
using SPMS.Web.ViewModels.Authoring;

namespace SPMS.Web.Mapper
{
    public class AuthorProfile : Profile
    {
        public AuthorProfile()
        {
            CreateMap<EpisodeEntry, AuthorPostViewModel>()
                .ForMember(x => x.IsReadOnly, opt => opt.Ignore())
                .ForMember(x => x.SiteDisclaimer, opt => opt.Ignore())
                .ForMember(x => x.SiteTitle, opt => opt.Ignore())
                .ForMember(x => x.GameName, opt => opt.Ignore())
                .ForMember(x => x.UseAnalytics, opt => opt.Ignore())
                .ForMember(x => x.SiteAnalytics, opt => opt.Ignore())
                .ForMember(x => x.IsAdmin, opt => opt.Ignore())
                .ForMember(x => x.IsPlayer, opt => opt.Ignore())
                .ForMember(x => x.PostTypes, opt => opt.Ignore())
                .ForMember(x => x.TypeId, opt => opt.Ignore())
                .ForMember(x => x.Episode, opt => opt.MapFrom(x => x.Episode.Title))
                .ForMember(x => x.Authors, opt => opt.MapFrom(x =>x.EpisodeEntryPlayer.Select(y => y.PlayerId)))
                .ForMember(x => x.EpisodeId, opt => opt.MapFrom(x => x.EpisodeId))
                .ForMember(x => x.StatusId, o => o.MapFrom(y =>y.EpisodeEntryStatusId))
                .ForMember(x=>x.Statuses, o=>o.Ignore()); ;


            CreateMap<AuthorPostViewModel, EpisodeEntry>()
                .ForMember(x => x.EpisodeEntryTypeId, opt => opt.MapFrom(x => x.TypeId))
                .ForMember(x => x.EpisodeEntryType, opt => opt.Ignore())
                .ForMember(x => x.EpisodeEntryPlayer, opt => opt.Ignore())
                .ForMember(x => x.Episode, opt => opt.Ignore())
                .ForMember(x => x.CreatedAt, opt => opt.MapFrom(x => DateTime.UtcNow))
                .ForMember(x => x.EpisodeEntryStatusId, o => o.MapFrom(y => y.StatusId))
                .ForMember(x => x.EpisodeEntryStatus, o => o.Ignore());


            CreateMap<EpisodeEntry, PostViewModel>()
                .ForMember(x => x.Authors, o => o.MapFrom(x => x.EpisodeEntryPlayer.Select(y => y.Player.DisplayName)));
        }
    }

    
}