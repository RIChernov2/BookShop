using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using BookShop.BooksAndAuthors.Manager.Configurations.Models;
using BookShop.BooksAndAuthors.Manager.Data.Entities;
using BookShop.BooksAndAuthors.Manager.Interfaces;
using BookShop.BooksAndAuthors.Manager.Interfaces.Managers;

namespace BookShop.BooksAndAuthors.Manager.StorageManagers
{
    public class BooksStorageManager : IBooksStorageManager
    {
        private readonly IUnitOfWork _uow;
        private readonly AppConfiguration _configuration;

        public BooksStorageManager(IUnitOfWork uow, AppConfiguration configuration)
        {
            _uow = uow;
            _configuration = configuration;
        }

        public async Task<IReadOnlyList<Book>> GetAllAsync()
            => await _uow.BooksRepository.GetAsync();

        public async Task<IReadOnlyList<Book>> GetByIdsAsync(IEnumerable<long> ids)
            => await _uow.BooksRepository.GetAsync(ids);

        public async Task<Book> GetByIdAsync(long id)
            => await _uow.BooksRepository.GetAsync(id);

        public async Task<int> CreateAsync(Book book)
        {
            var result = await _uow.BooksRepository.CreateAsync(book);
            _uow.Commit();
            return result;
        }

        public async Task<int> UpdateAsync(Book book)
        {
            var result = await _uow.BooksRepository.UpdateAsync(book);
            _uow.Commit();
            return result;
        }

        public async Task<int> DeleteAsync(long id)
        {
            var result = await _uow.BooksRepository.DeleteAsync(id);
            _uow.Commit();
            return result;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if ( disposing )
            {
                ReleaseUnmanagedResources();
            }
        }

        private void ReleaseUnmanagedResources()
        {
            _uow.Dispose();
        }

        ~BooksStorageManager()
        {
            Dispose(false);
        }
    }
}