using OpenAI;
using SCPHA_GPT.Interfaces;

namespace SCPHA_GPT.Services
{
    public class DallE2 : IDallE2
    {
        private readonly OpenAIClient openAIClient;

        public DallE2(OpenAIClient openAIClient)
        {
            this.openAIClient = openAIClient;
        }

        public Task<IReadOnlyList<string>> CreateImageFromDescription(string description, CancellationToken cancellationToken=default)
        {
            return openAIClient.ImagesEndPoint.GenerateImageAsync($"Create a photorealistic depiction of this description: {description.Substring(0, 500)}", cancellationToken: cancellationToken);
        }
    }
}
