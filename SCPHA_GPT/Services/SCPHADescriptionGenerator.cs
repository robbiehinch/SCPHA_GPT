using OpenAI;
using OpenAI.Chat;
using SCPHA_GPT.Interfaces;

namespace SCPHA_GPT.Services
{
    public class SCPHADescriptionGenerator : IChatGPT
    {
        private readonly OpenAIClient openAIClient;

        public SCPHADescriptionGenerator(OpenAIClient openAIClient)
        {
            this.openAIClient = openAIClient;
        }

        public async Task<IReadOnlyList<string>> Generate(string mood)
        {
            var chatResult = await openAIClient.ChatEndpoint.GetCompletionAsync(new OpenAI.Chat.ChatRequest(
                new []{
                    new Message(Role.System, "Assistant is a very descriptive chatbot that gives veyr descriptive and imaginative scene depictions along the lines of the user prompt"),
                    new Message(Role.User, $"Describe the Felixstowe port authority premises where the general mood of the scene and weather is {mood}"),
                }
            ));

            return chatResult.Choices
                .Select(_ => _.Message.Content)
                .ToList();
        }
    }
}
