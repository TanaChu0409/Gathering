using eGathering.Domain.Gatherings;

namespace eGathering.Persistence.Specifications;

internal class GatheringByNameSpecification : Specification<Gathering>
{
    public GatheringByNameSpecification(string name)
        : base(gathering => string.IsNullOrWhiteSpace(name) ||
                            gathering.Name == name)
    {
        AddInclude(gathering => gathering.Creator);
        AddInclude(gathering => gathering.Attendees);

        AddOrderBy(gathering => gathering.Name);
    }
}