using eGathering.Domain.Gatherings;

namespace eGathering.Persistence.Specifications;

internal class GatheringByIdWithCreatorSpecification : Specification<Gathering>
{
    public GatheringByIdWithCreatorSpecification(Guid gatheringId)
        : base(gathering => gathering.Id == gatheringId)
    {
        AddInclude(gathering => gathering.Creator);
    }
}