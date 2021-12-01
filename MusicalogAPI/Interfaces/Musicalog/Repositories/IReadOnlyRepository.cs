namespace MusicalogAPI.Interfaces.Musicalog.Repositories
{
    public interface IReadOnlyRepository<T>
    {
        Task<IEnumerable<T>> GetAsync();
    }
}