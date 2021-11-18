using BookShop.BooksAndAuthors.Manager.Configurations.Models;
using BookShop.BooksAndAuthors.Manager.Data.Entities;
using BookShop.BooksAndAuthors.Manager.Interfaces;
using BookShop.BooksAndAuthors.Manager.Interfaces.Managers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookShop.BooksAndAuthors.Manager.StorageManagers
{
    public class AuthorsStorageManager : IAuthorsStorageManager
    {
        private readonly IUnitOfWork _uow;
        private readonly AppConfiguration _configuration;

        public AuthorsStorageManager(IUnitOfWork uow, AppConfiguration configuration)
        {
            _uow = uow;
            _configuration = configuration;
        }

        public async Task<IReadOnlyList<Author>> GetAllAsync()
            => await _uow.AuthorsRepository.GetAsync();

        public async Task<IReadOnlyList<Author>> GetByIdsAsync(IEnumerable<long> ids)
            => await _uow.AuthorsRepository.GetAsync(ids);

        public async Task<Author> GetByIdAsync(long id)
            => await _uow.AuthorsRepository.GetAsync(id);

        public async Task<int> CreateAsync(Author author)
        {
            var result = await _uow.AuthorsRepository.CreateAsync(author);
            _uow.Commit();
            return result;
        }

        public async Task<int> UpdateAsync(Author author)
        {
            var result = await _uow.AuthorsRepository.UpdateAsync(author);
            _uow.Commit();
            return result;
        }

        public async Task<int> DeleteAsync(long id)
        {
            var result = await _uow.AuthorsRepository.DeleteAsync(id);
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
            if (disposing)
            {
                ReleaseUnmanagedResources();
            }
        }

        private void ReleaseUnmanagedResources()
        {
            _uow.Dispose();
        }

        ~AuthorsStorageManager()
        {
            Dispose(false);
        }
    }
}