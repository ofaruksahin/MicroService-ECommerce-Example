using AutoMapper;
using ECommerce.Services.Catalog.Dtos.Category;
using ECommerce.Services.Catalog.Dtos.Course;
using ECommerce.Services.Catalog.Models;
using ECommerce.Services.Catalog.Settings;
using ECommerce.Shared.Dtos;
using ECommerce.Shared.Messages;
using MassTransit;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Services.Catalog.Services
{
    public class CourseService : ICourseService
    {
        private readonly IMongoCollection<Course> _courseCollection;
        private readonly IMongoCollection<Category> _categoryCollection;

        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publishEndpoint;

        public CourseService(IMapper mapper, IDatabaseSettings databaseSettings,IPublishEndpoint publishEndpoint)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            _courseCollection = database.GetCollection<Course>(databaseSettings.CourseCollectionName);
            _categoryCollection = database.GetCollection<Category>(databaseSettings.CategoryCollectionName);
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<Shared.Dtos.Response<List<CourseDto>>> GetAllAsync()
        {
            var courses = await _courseCollection.Find(course => true).ToListAsync();

            if (courses.Any())
            {
                foreach (var course in courses)
                {
                    course.Category = await _categoryCollection.Find<Category>(category => category.Id == course.CategoryId).FirstOrDefaultAsync();
                }
            }
            else
                courses = new List<Course>();

            return Shared.Dtos.Response<List<CourseDto>>.Success(_mapper.Map<List<CourseDto>>(courses), 200);
        }

        public async Task<Shared.Dtos.Response<CourseDto>> GetByIdAsync(string id)
        {
            var course = await _courseCollection.Find(course => course.Id == id).FirstOrDefaultAsync();
            if (course == null)
                return Shared.Dtos.Response<CourseDto>.Fail("Course not fonud", 404);
            course.Category = await _categoryCollection.Find<Category>(category => category.Id == course.CategoryId).FirstOrDefaultAsync();
            return Shared.Dtos.Response<CourseDto>.Success(_mapper.Map<CourseDto>(course), 200);
        }

        public async Task<Shared.Dtos.Response<List<CourseDto>>> GetAllByUserIdAsync(string userId)
        {
            var courses = await _courseCollection.Find(course => course.UserId == userId).ToListAsync();
            if (courses.Any())
            {
                foreach (var course in courses)
                {
                    course.Category = await _categoryCollection.Find(category => category.Id == course.CategoryId).FirstOrDefaultAsync();
                }
            }
            else
                courses = new List<Course>();

            return Shared.Dtos.Response<List<CourseDto>>.Success(_mapper.Map<List<CourseDto>>(courses), 200);
        }

        public async Task<Shared.Dtos.Response<CourseDto>> CreateAsync(CourseCreateDto dto)
        {
            var course = _mapper.Map<Course>(dto);
            course.CreatedTime = DateTime.Now;
            await _courseCollection.InsertOneAsync(course);
            return Shared.Dtos.Response<CourseDto>.Success(_mapper.Map<CourseDto>(course), 200);
        }

        public async Task<Shared.Dtos.Response<NoContent>> UpdateAsync(CourseUpdateDto dto)
        {
            var updateCourse = _mapper.Map<Course>(dto);

            var result = await _courseCollection.FindOneAndReplaceAsync(course => course.Id == dto.Id, updateCourse);

            if (result == null)
                return Shared.Dtos.Response<NoContent>.Fail("Course not found", 404);

            await _publishEndpoint.Publish<ProductNameChangedEvent>(new ProductNameChangedEvent
            {
                ProductId = updateCourse.Id,
                NewName = updateCourse.Name
            });

            return Shared.Dtos.Response<NoContent>.Success(204);
        }

        public async Task<Shared.Dtos.Response<NoContent>> DeleteAsync(string id)
        {
            var result = await _courseCollection.FindOneAndDeleteAsync(course => course.Id == id);
            if (result == null)
                return Shared.Dtos.Response<NoContent>.Fail("Course not found", 404);
            return Shared.Dtos.Response<NoContent>.Success(204);
        }
    }
}
