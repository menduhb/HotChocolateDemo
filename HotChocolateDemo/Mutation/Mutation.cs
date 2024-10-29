using Bogus;
using HotChocolate.Subscriptions;
using HotChocolateDemo.DTOs;
using HotChocolateDemo.Repository;
using HotChocolateDemo.Schema; 

namespace HotChocolateDemo.Mutation
{
    public class Mutation
    {
        private readonly CourseRepository _coruse;


        public Mutation(CourseRepository coruse)
        {
            _coruse = coruse;
        }
        public async Task<CourseType> CreateCourse(string name, DTOs.Subject subject, Guid instructorID, [Service] ITopicEventSender topicEventSender)
        {
            var courseDTO = new CourseDTO()
            {
                Name = name,
                Subject = subject,
                InstructorId = instructorID
            };
            courseDTO = await _coruse.Create(courseDTO);
            var courseType = new CourseType()
            {
                Id = courseDTO.Id,
                Name = courseDTO.Name,
                Subject = courseDTO.Subject,
                InstructorId = courseDTO.InstructorId
            };
            return courseType;
        }

        public CourseDTO UpdateCourse(Guid courseId, string name, DTOs.Subject subject, Guid instructorID)
        {
            var exists = _coruse.GetById(courseId);
            if (exists == null)
            {
                throw new GraphQLException(new Error("Coruse not found", "NOT_FOUND"));
            }

            var courseDTO = new CourseDTO()
            {
                Id = courseId,
                Name = name,
                Subject = subject,
                InstructorId = instructorID
            };
            _coruse.Update(courseDTO);

            return courseDTO;

        }

        public async Task<bool> DeleteCoruse(Guid courseId)
        {
            var exists = _coruse.Exists(courseId);
            if (exists != null)
                return await _coruse.Delete(courseId);
            throw new GraphQLException(new Error("Coruse not found", "NOT_FOUND"));

        }
    }
}
