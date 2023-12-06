using Microsoft.Extensions.Options;
using OpenAI_API.Completions;
using OpenAI_API.Embedding;
using OpenAI_API.Images;
using OpenAI_API.Models;
using RedMango_Api.Config;
using RedMango_Api.Services.Contracts;
using System.Numerics;

namespace RedMango_Api.Services
{
    public class OpenAIService : IOpenAIService
    {
        private readonly OpenAISettings _openAISettings;
        public OpenAIService(IOptions<OpenAISettings> options)
        {
            _openAISettings = options.Value;
            
        }

        public async Task<string> CheckProgrammingLanguage(string language)
        {
            // create API instance
            var api = new OpenAI_API.OpenAIAPI(_openAISettings.Key);

            var chat = api.Chat.CreateConversation();

            chat.AppendSystemMessage("You are a teacher who helps new programmers understand things are programming languages or not." +
                "If a user tells you a programming language respond with yes, if a user tells you something which is not a programming language respond with no. " +
                "You will only respond with yes or no. Do not say anything else! ");

            chat.AppendUserInput(language);

            var response = await chat.GetResponseFromChatbotAsync();

            return response;
        }

        public async Task<string> CompleteSentence(string text)
        {
            // create API instance
            var api = new OpenAI_API.OpenAIAPI(_openAISettings.Key);

            var result = await api.Completions.GetCompletion(text);
            return result;
        }

        public async Task<string> CompleteSentenceAdvanced(string text)
        {
            // create API instance
            var api = new OpenAI_API.OpenAIAPI(_openAISettings.Key);

            var result = await api.Completions.CreateCompletionAsync(
                new CompletionRequest(text, model: Model.CurieText, temperature: 0.1));

            return result.Completions[0].Text;
        }

        public async Task<List<OpenAI_API.Embedding.Data>> CreateEmbeddings(string text)
        {
            // create API instance
            var api = new OpenAI_API.OpenAIAPI(_openAISettings.Key);

            var result = await api.Embeddings.CreateEmbeddingAsync(new EmbeddingRequest(text));

            
            return result.Data; ;
        }

        public async Task<string> CreateImage(string text)
        {
            // create API instance
            var api = new OpenAI_API.OpenAIAPI(_openAISettings.Key);

            var result = await api.ImageGenerations.CreateImageAsync("A drawing of a " + text);

            return result.Data[0].Url;
        }


    }
}
