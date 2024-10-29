using Bogus;
using HotChocolateDemo.DTOs;
using HotChocolateDemo.Repository;
using HotChocolateDemo.Services;
using System.Collections.Generic;
using HotChocolate.Data;
using HotChocolate;
using Microsoft.EntityFrameworkCore;

namespace HotChocolateDemo.Schema
{
    public class Query
    {
        private readonly CourseRepository _courseRepository;
        private readonly IDbContextFactory<SchoolDbContext> _contextFactory;

        public Query(CourseRepository coruse, IDbContextFactory<SchoolDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
            _courseRepository = coruse;
        }
         
        [UseFiltering]
        public IEnumerable<CourseType> GetAllFilter()
        {
            
            using (SchoolDbContext context = _contextFactory.CreateDbContext())
            {
                var a= context.Courses.Select(x => new CourseType()
                {
                    Id = x.Id,
                    Subject = x.Subject,
                    InstructorId = x.InstructorId,
                    Name = x.Name
                });

                return a.ToList();
            }
             
        }

        public async Task<IEnumerable<CourseType>> GetCourses()
        {
            IEnumerable<CourseDTO> courseDtos = await _courseRepository.GetAll();
            return courseDtos.Select(x => new CourseType()
            {
                Id = x.Id,
                Subject = x.Subject,
                InstructorId = x.InstructorId,
                Name = x.Name
            });
        }

        //In order not to take all data and sort, this needs to return IQueryable
        [UsePaging(IncludeTotalCount = true, DefaultPageSize = 3)]
        public async Task<IEnumerable<CourseType>> GetCursorBasedCourses()
        {
            IEnumerable<CourseDTO> courseDtos = await _courseRepository.GetAll();
            return courseDtos.Select(x => new CourseType()
            {
                Id = x.Id,
                Subject = x.Subject,
                InstructorId = x.InstructorId,
                Name = x.Name
            });
        }

        //In order not to take all data and sort, this needs to return IQueryable
        [UseOffsetPaging(IncludeTotalCount = true, DefaultPageSize = 3)]
        public async Task<IEnumerable<CourseType>> GetCoursesOffSetPagination()
        {
            IEnumerable<CourseDTO> courseDtos = await _courseRepository.GetAll();
            return courseDtos.Select(x => new CourseType()
            {
                Id = x.Id,
                Subject = x.Subject,
                InstructorId = x.InstructorId,
                Name = x.Name
            });
        }


        public async Task<CourseType> GetCoruseByIdAsync(Guid id)
        {
            var courseDto = await _courseRepository.GetById(id);
            if (courseDto != null)
            {
                var courseType = new CourseType()
                {
                    Id = courseDto.Id,
                    Subject = courseDto.Subject,
                    InstructorId = courseDto.InstructorId,
                    Name = courseDto.Name
                };
                return courseType;
            }
            throw null;
        }


        public async Task<CourseType> GetCoruseByName(string name)
        {
            var courseDto = await _courseRepository.GetByName(name);
            if (courseDto != null)
            {
                var courseType = new CourseType()
                {
                    Id = courseDto.Id,
                    Subject = courseDto.Subject,
                    InstructorId = courseDto.InstructorId,
                    Name = courseDto.Name
                };
                return courseType;
            }
            throw null;
        }



        [GraphQLDeprecated("This query is deprecated by 1st of January")]
        public string Instructions()
        {
            return "Smash that like ";
        }
    }
}
