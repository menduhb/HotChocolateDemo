using HotChocolateDemo.DTOs;
using HotChocolateDemo.Services;
using Microsoft.EntityFrameworkCore;

namespace HotChocolateDemo.Repository
{
    public class CourseRepository
    { 
        private readonly IDbContextFactory<SchoolDbContext> _contextFactory;

        public CourseRepository(IDbContextFactory<SchoolDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IQueryable<CourseDTO> GetAllQueryable()
        {
            var context = _contextFactory.CreateDbContext(); // No using statement here, leave it open 
                var course = context.Courses;
                return course; 
        }

        public async Task<List<CourseDTO>> GetAll()
        {
            using (SchoolDbContext context = _contextFactory.CreateDbContext())
            {
                var course = await context.Courses
                    .Include(x => x.Instructor)
                    .Include(x => x.Students)
                    .ToListAsync();
                return course;
            }
        }

        public async Task<CourseDTO> GetById(Guid courseId)
        {
            using (SchoolDbContext context = _contextFactory.CreateDbContext())
            {
                var course = await context.Courses
                    .Include(x => x.Instructor)
                    .Include(x => x.Students)
                    .FirstOrDefaultAsync(x => x.Id == courseId);
                return course;
            }
        }


        public async Task<CourseDTO> GetByName(string coruseName)
        {
            using (SchoolDbContext context = _contextFactory.CreateDbContext())
            {
                var course = await context.Courses
                    .Include(x => x.Instructor)
                    .Include(x => x.Students)
                    .FirstOrDefaultAsync(x => x.Name.ToLower() == coruseName.ToLower());
                return course;
            }
        }
        public async Task<CourseDTO> Create(CourseDTO course)
        {
            using (SchoolDbContext context = _contextFactory.CreateDbContext())
            {
                context.Courses.Add(course);
                await context.SaveChangesAsync();
            }

            return course;
        }

        public async Task<CourseDTO> Update(CourseDTO course)
        {
            using (SchoolDbContext context = _contextFactory.CreateDbContext())
            {
                context.Courses.Update(course);
                await context.SaveChangesAsync();
            }

            return course;
        }

        public async Task<bool> Delete(Guid courseId)
        {
            using (SchoolDbContext context = _contextFactory.CreateDbContext())
            {
                var course = new CourseDTO()
                {
                    Id = courseId
                };
                context.Courses.Remove(course);
                return await context.SaveChangesAsync() > 0;
            }
        }

        public async Task<bool> Exists(Guid courseId)
        {
            using (SchoolDbContext context = _contextFactory.CreateDbContext())
            {
                var course = await context.Courses
                    .FirstOrDefaultAsync(x => x.Id == courseId);
                return course != null;
            }
        }
    }
}
