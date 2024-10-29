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

        public IQueryable<CourseDTO> GetAll1()
        {
            using (SchoolDbContext context = _contextFactory.CreateDbContext())
            {
                var coruse = context.Courses;
                return coruse;
            }
        }

        public async Task<List<CourseDTO>> GetAll()
        {
            using (SchoolDbContext context = _contextFactory.CreateDbContext())
            {
                var coruse = await context.Courses
                    .Include(x => x.Instructor)
                    .Include(x => x.Students)
                    .ToListAsync();
                return coruse;
            }
        }

        public async Task<CourseDTO> GetById(Guid courseId)
        {
            using (SchoolDbContext context = _contextFactory.CreateDbContext())
            {
                var coruse = await context.Courses
                    .Include(x => x.Instructor)
                    .Include(x => x.Students)
                    .FirstOrDefaultAsync(x => x.Id == courseId);
                return coruse;
            }
        }


        public async Task<CourseDTO> GetByName(string coruseName)
        {
            using (SchoolDbContext context = _contextFactory.CreateDbContext())
            {
                var coruse = await context.Courses
                    .Include(x => x.Instructor)
                    .Include(x => x.Students)
                    .FirstOrDefaultAsync(x => x.Name.ToLower() == coruseName.ToLower());
                return coruse;
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
                var coruse = await context.Courses
                    .FirstOrDefaultAsync(x => x.Id == courseId);
                return coruse != null;
            }
        }
    }
}
