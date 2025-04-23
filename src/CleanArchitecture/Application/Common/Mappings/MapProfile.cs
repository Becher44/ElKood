using AutoMapper;
using ElKood.Shared.Models.Item;
using ElKood.Shared.Models.User;

namespace ElKood.Application.Common.Mappings;

public class MapProfile : Profile
{
    public MapProfile()
    {
        CreateMap<Item, ItemDTO>().ReverseMap();
        CreateMap<Item, AddItemRequest>().ReverseMap();
        CreateMap<Item, UpdateItemRequest>().ReverseMap();


        CreateMap<User, UserSignInRequest>().ReverseMap();
        CreateMap<User, UserSignInResponse>().ReverseMap();
        CreateMap<User, UserSignUpRequest>().ReverseMap();
        CreateMap<User, UserSignUpResponse>().ReverseMap();
        CreateMap<User, UserProfileResponse>().ReverseMap();
    }
}
