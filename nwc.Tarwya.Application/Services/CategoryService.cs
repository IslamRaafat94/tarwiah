using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using nwc.Tarwya.Application.Services.Contracts;
using nwc.Tarwya.Application.ViewModels.Categories;
using nwc.Tarwya.Application.ViewModels.Shared;
using nwc.Tarwya.Domain.Models.Models;
using nwc.Tarwya.Domain.Repositories;
using nwc.Tarwya.Infra.Core;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace nwc.Tarwya.Application.Services
{
    public class CategoryService : ServiceBase, ICategoryService
    {
        private readonly IRepository<Category> CategoryRepo;
        private readonly IRepository<CategoryItem> SubCategoryRepo;

        public CategoryService(
            IOptions<SystemSettings> settings,
            IMapper mapper,
            IRepository<Category> _categoryRepo,
            IRepository<CategoryItem> _subCategoryRepo
            )
            : base(settings, mapper)
        {
            this.CategoryRepo = _categoryRepo;
            this.SubCategoryRepo = _subCategoryRepo;
        }
        public IQueryable<CategoryVm> GetAllCategories()
        {
            var list = CategoryRepo.Get(i => i.IsActive && !i.IsDeleted)
                .AsNoTracking()
                .ProjectTo<CategoryVm>(mapper.ConfigurationProvider);

            return list;

        }
        public async Task<List<LookUpVm>> GetCategoriesLookUp()
        {
            var list = await CategoryRepo.Get(i => i.IsActive && !i.IsDeleted)
                .OrderBy(i=>i.OrderNo)
                .AsNoTracking()
                .ProjectTo<LookUpVm>(mapper.ConfigurationProvider)
                .ToListAsync();

            if (list.Count == 0)
                return new List<LookUpVm>();

            return list;
        }
        public IQueryable<SubCategoryVm> GetAllSubCategories(int CategoryId)
        {
            var list = SubCategoryRepo.Get(i => i.IsActive && !i.IsDeleted && i.CategoryId == CategoryId)
                .AsNoTracking()
                .ProjectTo<SubCategoryVm>(mapper.ConfigurationProvider);

            return list;

        }

        public async Task<List<CategoryItemLookUpVm>> GetSubCategoriesLookUp(int CategoryId)
        {
            var list = await SubCategoryRepo.Get(i => i.IsActive && !i.IsDeleted && i.CategoryId == CategoryId)
                .OrderBy(i => i.OrderNo)
                .AsNoTracking()
                .ProjectTo<CategoryItemLookUpVm>(mapper.ConfigurationProvider)
                .ToListAsync();

            if (list.Count == 0)
                return new List<CategoryItemLookUpVm>();

            return list;
        }

        public async Task<bool> ImportCategories(CategoriesFileVm fileObject)
        {
            using var tranc = CategoryRepo.GetTransaction();
            try
            {
                var catiegorislist = new List<Category>();
                for (int i = 0; i < fileObject.en.Count; i++)
                {
                    var category = new Category()
                    {
                        IsActive = true,
                        IsDeleted = false,
                        NameEn = fileObject.en[i].cat,
                        NameAr = fileObject.ar[i].cat,
                        NameFa = fileObject.fa[i].cat,
                        NameFr = fileObject.fr[i].cat,
                        NameTr = fileObject.tr[i].cat,
                        NameUr = fileObject.ur[i].cat,
                        NameId = fileObject.id[i].cat,
                    };

                    for (int j = 0; j < fileObject.en[i].catItems.Count; j++)
                    {
                        var categoryItem = new CategoryItem()
                        {
                            IsActive = true,
                            IsDeleted = false,
                            Code = fileObject.en[i].catItems[j].key,
                            NameEn = fileObject.en[i].catItems[j].name,
                            NameAr = fileObject.ar[i].catItems[j].name,
                            NameFa = fileObject.fa[i].catItems[j].name,
                            NameFr = fileObject.fr[i].catItems[j].name,
                            NameTr = fileObject.tr[i].catItems[j].name,
                            NameUr = fileObject.ur[i].catItems[j].name,
                            NameId = fileObject.id[i].catItems[j].name,
                            ServerName = fileObject.en[i].catItems[j].toServer,

                        };
                        category.CategoryItems.Add(categoryItem);
                    }
                    catiegorislist.Add(category);
                }

                var oldData = CategoryRepo.Get(i => i.IsActive == true).ToList();
                foreach (var item in oldData)
                {
                    item.IsActive = false;
                    foreach (var item2 in item.CategoryItems)
                    {
                        item2.IsActive = false;
                    }
                }
                if (catiegorislist.Count < 1)
                {
                    return true;
                }
                await CategoryRepo.BulkUpdateAsync(oldData);

                await CategoryRepo.BulkInsertAsync(catiegorislist, new EFCore.BulkExtensions.BulkConfig() { IncludeGraph = true });
                await tranc.CommitAsync();
                return true;
            }
            catch
            {
                await tranc.RollbackAsync();

                throw;
            }
            finally
            {
                await tranc.DisposeAsync();
            }
        }
    }
}
