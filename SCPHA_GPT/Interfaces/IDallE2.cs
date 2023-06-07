namespace SCPHA_GPT.Interfaces
{
    public interface IDallE2
    {
        Task<IReadOnlyList<string>> CreateImageFromDescription(string description, CancellationToken cancellationToken = default);
    }
}