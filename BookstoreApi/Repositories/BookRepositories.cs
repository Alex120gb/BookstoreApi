using AutoMapper;
using AutoMapper.QueryableExtensions;
using BookstoreApi.Models;
using BookstoreApi.Repositories.Interface;
using BookstoreApi.TableDbContext;
using BookstoreApi.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace BookstoreApi.Repositories
{
    public class BookRepositories : IBookRepositories
    {
        private readonly BookDbContext _context;
        private readonly IMapper _mapper;

        public BookRepositories(BookDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<GetUpdateBooksModel>> GetBooks()
        {
            var books = await _context.Books
                .ProjectTo<GetUpdateBooksModel>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return books;
        }

        public async Task<Response<int>> AddBooks(AddBooksModel request)
        {
            if (request == null)
            {
                return new Response<int>() { IsSuccessful = false, Value = 0 };
            }

            if (_context.BookExists(request.Title))
            {
                return new Response<int>() { IsSuccessful = false, Value = 0, Message = "Book with same title already exists" };
            }

            var mapedBooks = _mapper.Map<Book>(request);
            _context.Books.Add(mapedBooks);
            var result = await _context.SaveChangesAsync();

            return new Response<int>() { IsSuccessful = true, Value = result, Message = "Book successfully added" };
        }

        public async Task<Response<int>> UpdateBook(GetUpdateBooksModel request)
        {
            if (request.Id < 0 || request == null)
            {
                return new Response<int>() { IsSuccessful = false, Value = 0 };
            }

            var bookExists = _context.Books.Find(request.Id);
            if (bookExists == null)
            {
                return new Response<int>() { IsSuccessful = false, Value = 0, Message = "Book does not exist" };
            }

            _mapper.Map(request, bookExists);
            var result = await _context.SaveChangesAsync();

            return new Response<int>() { IsSuccessful = true, Value = result, Message = "Book successfully updated" };
        }

        public async Task<Response<int>> DeleteBook(int id)
        {
            var bookForDeletion = _context.Books.Find(id);
            if (bookForDeletion == null)
            {
                return new Response<int>() { IsSuccessful = false, Value = 0, Message = "Book does not exist" };
            }

            _context.Books.Remove(bookForDeletion);
            var result = await _context.SaveChangesAsync();

            return new Response<int>() { IsSuccessful = true, Value = result, Message = "Book successfully deleted" };
        }
    }
}