using AutoMapper;
using ECommerce.Services.Catalog.Dtos.Category;
using ECommerce.Services.Catalog.Dtos.Course;
using ECommerce.Services.Catalog.Models;
using ECommerce.Services.Catalog.Settings;
using ECommerce.Shared.Dtos;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Services.Catalog.Services
{
    internal class CourseService : ICourseService
    {
        private readonly IMongoCollection<Course> _courseCollection;
        private readonly IMongoCollection<Category> _categoryCollection;

        private readonly IMapper _mapper;

        public CourseService(IMapper mapper, IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            _courseCollection = database.GetCollection<Course>(databaseSettings.CourseCollectionName);
            _categoryCollection = database.GetCollection<Category>(databaseSettings.CategoryCollectionName);
            _mapper = mapper;
        }

        public async Task<Response<List<CourseDto>>> GetAllAsync()
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

            return Response<List<CourseDto>>.Success(_mapper.Map<List<CourseDto>>(courses), 200);
        }

        public async Task<Response<CourseDto>> GetByIdAsync(string id)
        {
            var course = await _courseCollection.Find(course => course.Id == id).FirstOrDefaultAsync();
            if (course == null)
                return Response<CourseDto>.Fail("Course not fonud", 404);
            course.Category = await _categoryCollection.Find<Category>(category => category.Id == course.CategoryId).FirstOrDefaultAsync();
            return Response<CourseDto>.Success(_mapper.Map<CourseDto>(course), 200);
        }

        public async Task<Response<List<CourseDto>>> GetAllByUserId(string userId)
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

            return Response<List<CourseDto>>.Success(_mapper.Map<List<CourseDto>>(courses), 200);
        }

        public async Task<Response<CourseDto>> CreateAsync(CourseCreateDto dto)
        {
            var course = _mapper.Map<Course>(dto);
            course.CreatedTime = DateTime.Now;
            await _courseCollection.InsertOneAsync(course);
            return Response<CourseDto>.Success(_mapper.Map<CourseDto>(course), 200);
        }

        public async Task<Response<NoContent>> ResponseAsync(CourseUpdateDto dto)
        {
            var updateCourse = _mapper.Map<Course>(dto);

            var result = await _courseCollection.FindOneAndReplaceAsync(course => course.Id == dto.Id, updateCourse);

            if (result == null)
                return Response<NoContent>.Fail("Course not found", 404);
            return Response<NoContent>.Success(204);
        }

        public async Task<Response<NoContent>> DeleteAsync(string id)
        {
            var result = await _courseCollection.FindOneAndDeleteAsync(course => course.Id == id);
            if (result == null)
                return Response<NoContent>.Fail("Course not found", 404);
            return Response<NoContent>.Success(204);
        }
    }
}
