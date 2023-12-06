using OpenAI_API.Images;

namespace RedMango_Api.Services.Contracts
{
    public interface IOpenAIService
    {
        Task<string> CompleteSentence(string text);
        Task<string> CompleteSentenceAdvanced(string text);
        Task<string> CheckProgrammingLanguage(string language);
        Task<string> CreateImage(string text);
        Task<List<OpenAI_API.Embedding.Data>> CreateEmbeddings(string text);

    }
}
