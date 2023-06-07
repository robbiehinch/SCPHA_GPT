namespace SCPHA_GPT.Interfaces
{
    public interface IChatGPT
    {
        Task<IReadOnlyList<string>> Generate(string mood);
    }
}