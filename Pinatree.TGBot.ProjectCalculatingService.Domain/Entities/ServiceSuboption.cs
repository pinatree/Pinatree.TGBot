namespace Pinatree.TGBot.ProjectCalculatingService.Domain.Entities
{
    [Serializable]
    public sealed class ServiceSuboption
    {
        public long Id { get; set; }

        public string Name { get; set; } = null!;

        public float Price { get; set; }

        public float Days { get; set; }
    }
}
