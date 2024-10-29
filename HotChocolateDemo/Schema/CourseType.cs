using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata.Ecma335;
using HotChocolateDemo.DataLoaders;
using HotChocolateDemo.DTOs;
using HotChocolateDemo.Repository;

namespace HotChocolateDemo.Schema
{
    public class CourseType
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Subject Subject { get; set; }
        [IsProjected]
        public Guid InstructorId { get; set; }

        [GraphQLNonNullType]
        public async Task<InstructorType> Instructor([Service] InstructorDataLoader instructorDataLoader)
        {

            InstructorDTO instructorDTO =  await instructorDataLoader.LoadAsync(InstructorId, CancellationToken.None);
            return new InstructorType()
            {
                FirstName = instructorDTO.FirstName,
                LastName = instructorDTO.LastName,
                Id = instructorDTO.Id,
                Salary = instructorDTO.Salary
            };
        }
        public IEnumerable<StudentType> Students{ get; set; }
    }
}
