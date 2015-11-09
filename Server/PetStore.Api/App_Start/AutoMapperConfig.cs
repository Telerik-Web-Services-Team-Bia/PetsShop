namespace PetStore.Api
{
    using PetStore.Models;
    using Models;

    public class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            AutoMapper.Mapper.CreateMap<Category, CategoryResponseModel>();

            //AutoMapper.Mapper.CreateMap<Artist, ArtistModel>()
            //    .ForMember(dest => dest.Country,
            //               opts => opts.MapFrom(src => src.CountryName))
            //    .ReverseMap();
        }
    }
}