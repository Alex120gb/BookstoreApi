using AutoMapper;
using BookstoreApi.Models;
using BookstoreApi.ViewModels;

namespace BookstoreApi.MappingProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Book, GetUpdateBooksModel>();
            CreateMap<GetUpdateBooksModel, Book>();

            CreateMap<Book, AddBooksModel>();
            CreateMap<AddBooksModel, Book>();

            CreateMap<User, RegisterUserModel>();
            CreateMap<RegisterUserModel, User>();
        }
    }
}
