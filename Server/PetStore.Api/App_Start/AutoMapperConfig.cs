﻿namespace PetStore.Api
{
    using System.Linq;

    using Models;
    using PetStore.Models;

    public class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            AutoMapper.Mapper.CreateMap<Category, CategoryDataTransferModel>();

            AutoMapper.Mapper.CreateMap<Pet, PetResponseModel>()
                .ForMember(dest => dest.Color,
                           opts => opts.MapFrom(src => src.Color.Name))
                .ForMember(dest => dest.Species,
                           opts => opts.MapFrom(src => src.Species.Name))
                .ForMember(dest => dest.Category,
                           opts => opts.MapFrom(src => src.Species.Category.Name))
                .ForMember(dest => dest.Rating,
                           opts => opts.MapFrom(src => (src.Ratings.Count == 0) ? 0 : (double)src.Ratings.Sum(r => r.Value) / src.Ratings.Count))
                .ForMember(dest => dest.Image,
                           opts => opts.MapFrom(src => src.Image.Image))
                .ForMember(dest => dest.Seller,
                           opts => opts.MapFrom(src => src.User.UserName));

            AutoMapper.Mapper.CreateMap<Rating, RatingResponseModel>();
        }
    }
}