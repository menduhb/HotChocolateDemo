using HotChocolate.Data.Filters;
using HotChocolateDemo.Schema;

namespace HotChocolateDemo.Filters
{
    // Filter configuration in more details
    public class CourseFilterType : FilterInputType<CourseType>
    {
        protected override void Configure(IFilterInputTypeDescriptor<CourseType> descriptor)
        {   
            descriptor.Ignore(c => c.Students);
            base.Configure(descriptor);
        }
    }
}
