using AutoMapper;
using FreeCourse.Service.Catalog.Dtos;
using FreeCourse.Service.Catalog.Models;
using FreeCourse.Service.Catalog.Settings;
using FreeCourseShared.Dtos;
using MongoDB.Driver;

namespace FreeCourse.Service.Catalog.Services
{
    public class CategoryServices:ICategoryService
    {
        private readonly IMongoCollection<Category> _categoryCollection;

        private readonly IMapper _mapper;

        public CategoryServices(IMapper mapper,IDatabaseSettings databaseSettings)
        {
            var client=new MongoClient(databaseSettings.ConnectionString);
            var db = client.GetDatabase(databaseSettings.DatabaseName);

            _categoryCollection = db.GetCollection<Category>(databaseSettings.CategoryCollectionName);
            _mapper = mapper;
        }


        public async Task<ResponseDto<List<CategoryDto>>> GetAllAsync()
        {
            var categories= await _categoryCollection.Find(category => true).ToListAsync();
            return  ResponseDto<List<CategoryDto>>.Success (_mapper.Map<List<CategoryDto>>(categories),200);

        }


        public async Task<ResponseDto<CategoryDto>> CreateAsync(CategoryDto categoryDto)
        {
            var category = _mapper.Map<Category>(categoryDto);

            await _categoryCollection.InsertOneAsync(category);
            return ResponseDto<CategoryDto>.Success(_mapper.Map<CategoryDto>(category),200);

        }

        public async Task<ResponseDto<CategoryDto>> GetByIdAsync(string id)
        {
            var category= await _categoryCollection.Find<Category>(x=>x.Id==id).FirstOrDefaultAsync();
            if (category == null)
            {
                return ResponseDto<CategoryDto>.Fail("Category Not Found", 404);
            }
            return ResponseDto<CategoryDto>.Success(_mapper.Map<CategoryDto>(category),200);

        }
    }
}
