using AutoMapper;
using MyLetter.Dto;
using MyLetter.EF.Models;
using System.Numerics;

namespace MyLetter.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            

            // CreateMap<Photo, PhotoDto>();
            //   CreateMap<MemberUpdateDto, AppUser>();
            //  CreateMap<RegisterDto, AppUser>();
            CreateMap<Message, MessageDto>();
            CreateMap<MessageDto, Message>();

            CreateMap<(List<Education> education,List<Sector> secotr), FilterDto>();
            CreateMap<AppUser, ProfileDto>()
            .ForMember(dest => dest.about, act => act.MapFrom(src => src.AboutMe))
            .ForMember(dest => dest.username, act => act.MapFrom(src => src.UserName))
            .ForMember(dest => dest.country, act => act.MapFrom(src => src.CountryId))
            .ForMember(dest => dest.home, act => act.MapFrom(src => src.HomeId))
            //  .ForMember(dest => dest.seeking, act => act.MapFrom(src => src.SeekingId))
            .ForMember(dest => dest.age, act => act.MapFrom(src => src.AgeId))
            .ForMember(dest => dest.gender, act => act.MapFrom(src => src.GenderId))
            .ForMember(dest => dest.height, act => act.MapFrom(src => src.HeightId))
            .ForMember(dest => dest.salary, act => act.MapFrom(src => src.SalaryId))
            .ForMember(s => s.hobbies, c => c.MapFrom(m => m.Name))

            // .ForMember(dest => dest.have_kids, act => act.MapFrom(src => src.HavekidsId))
            // .ForMember(dest => dest.looking, act => act.MapFrom(src => src.SearchFor))
            .ForMember(dest => dest.education, act => act.MapFrom(src => src.EducationId))
            .ForMember(dest => dest.relationship, act => act.MapFrom(src => src.RelationshipId))
            .ForMember(dest => dest.name, act => act.MapFrom(src => src.Name))
            .ForMember(dest => dest.family_values, act => act.MapFrom(src => src.FamilyValuesId))
            // .ForMember(dest => dest.relocate, act => act.MapFrom(src => src.RelocateId))
            //  .ForMember(dest => dest.polygamy_opinion, act => act.MapFrom(src => src.PolygamyOpinionId))
            .ForMember(dest => dest.smoking, act => act.MapFrom(src => src.SmokingId))
            .ForMember(dest => dest.sector, act => act.MapFrom(src => src.SectorId))
            .ForMember(dest => dest.driver, act => act.MapFrom(src => src.DriverId))
            .ForMember(dest => dest.work, act => act.MapFrom(src => src.WorkId))
            //.ForMember(dest => dest.want_kids, act => act.MapFrom(src => src.WantKidsId))
            .ForMember(dest => dest.zodiac, act => act.MapFrom(src => src.ZodiacId))
            .ForMember(dest => dest.image, act => act.MapFrom(src => src.Image))
            .ForMember(dest => dest.id, act => act.MapFrom(src => src.Id));

        }
    }
}
