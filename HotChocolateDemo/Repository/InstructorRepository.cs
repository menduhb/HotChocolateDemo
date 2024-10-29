using HotChocolateDemo.DTOs;
using HotChocolateDemo.Services;
using Microsoft.EntityFrameworkCore;


namespace HotChocolateDemo.Repository
{
    public class InstructorRepository
    {
        private readonly IDbContextFactory<SchoolDbContext> _contextFactory;

        public InstructorRepository(IDbContextFactory<SchoolDbContext> contextFactory)
        {
            _contextFactory = contextFactory; 
        }


        public IQueryable<InstructorDTO> GetAll()
        {
            using (SchoolDbContext context = _contextFactory.CreateDbContext())
            {
                var instructor = context.Instructors;
                return instructor;
            }
        }

        public async Task<InstructorDTO> GetById(Guid instructorId)
        {
            using (SchoolDbContext context = _contextFactory.CreateDbContext())
            {
                var instructor = await context.Instructors
                    .Include(x => x.Courses)
                    .FirstOrDefaultAsync(x => x.Id == instructorId);
                return instructor;
            }
        }


        public async Task<InstructorDTO> GetByName(string instructorName)
        {
            using (SchoolDbContext context = _contextFactory.CreateDbContext())
            {
                var instructor = await context.Instructors
                    .Include(x => x.Courses)
                    .FirstOrDefaultAsync(x => x.FirstName.ToLower() == instructorName.ToLower());
                return instructor;
            }
        }
        public async Task<InstructorDTO> Create(InstructorDTO instructor)
        {
            using (SchoolDbContext context = _contextFactory.CreateDbContext())
            {
                context.Instructors.Add(instructor);
                await context.SaveChangesAsync();
            }

            return instructor;
        }

        public async Task<InstructorDTO> Update(InstructorDTO instructor)
        {
            using (SchoolDbContext context = _contextFactory.CreateDbContext())
            {
                context.Instructors.Update(instructor);
                await context.SaveChangesAsync();
            }

            return instructor;
        }

        public async Task<bool> Delete(Guid instructorId)
        {
            using (SchoolDbContext context = _contextFactory.CreateDbContext())
            {
                var instructor = new InstructorDTO()
                {
                    Id = instructorId
                };
                context.Instructors.Remove(instructor);
                return await context.SaveChangesAsync() > 0;
            }
        }

        public async Task<bool> Exists(Guid instructorId)
        {
            using (SchoolDbContext context = _contextFactory.CreateDbContext())
            {
                var instructor = await context.Instructors
                    .FirstOrDefaultAsync(x => x.Id == instructorId);
                return instructor != null;
            }
        }

        public async Task<IEnumerable<InstructorDTO>> GetManyByIds(IReadOnlyList<Guid> instructorIds)
        {
            using (SchoolDbContext context = _contextFactory.CreateDbContext())
            {
                var instructor = await context.Instructors
                    .Where(x => instructorIds.Contains(x.Id)).ToListAsync();
                return instructor;
            }
        }
    }
}
