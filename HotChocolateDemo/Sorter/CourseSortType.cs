using HotChocolate.Data.Sorting;
using HotChocolateDemo.Schema;

namespace HotChocolateDemo.Sorter
{
    public class CourseSortType : SortInputType<CourseType>
    {
        protected override void Configure(ISortInputTypeDescriptor<CourseType> descriptor)
        {
            descriptor.Ignore(x => x.InstructorId);
            descriptor.Field(x => x.Name).Description("Sort courses based on the name");
            base.Configure(descriptor);
        }
    }
}
