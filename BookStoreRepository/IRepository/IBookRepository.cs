using BookStoreCommon.Book;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreRepository.IRepository
{
    public interface IBookRepository
    {
        public Task<int> AddBook(Books obj);
        public IEnumerable<Books> GetAllBooks();
        public bool UpdateBook(Books obj);
        public bool DeleteBook(int BookId);
        public bool Image(IFormFile file, int BookId);
        public IEnumerable<Books> GetBookById(int BookId);
    }
}
