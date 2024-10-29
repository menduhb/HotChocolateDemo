using HotChocolateDemo.DTOs;
using HotChocolateDemo.Repository;
using HotChocolateDemo.Schema;

namespace HotChocolateDemo.DataLoaders
{
    public class InstructorDataLoader : BatchDataLoader<Guid, InstructorDTO>
    {
        private readonly InstructorRepository _instructorRepository;
        public InstructorDataLoader(IBatchScheduler batchScheduler, DataLoaderOptions options , InstructorRepository instructorRepository) : base(batchScheduler, options)
        {
            _instructorRepository = instructorRepository;
        }

        protected override async Task<IReadOnlyDictionary<Guid, InstructorDTO>> LoadBatchAsync(IReadOnlyList<Guid> keys, CancellationToken cancellationToken)
        {
            IEnumerable<InstructorDTO> instructor = await _instructorRepository.GetManyByIds(keys);

            return instructor.ToDictionary(x => x.Id);
        }
    }
}
