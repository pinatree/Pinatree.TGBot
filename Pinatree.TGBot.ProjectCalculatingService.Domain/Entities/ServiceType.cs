namespace Pinatree.TGBot.ProjectCalculatingService.Domain.Entities
{
    [Serializable]
    public sealed class ServiceType
    {
        public long Id { get; set; }

        public string Name { get; set; } = null!;

        public IEnumerable<ServiceSuboption> AvailableSuboptions { get; set; }

        public IEnumerable<ServiceSuboption> ObligatorySuboptions { get; set; }
    }
}
