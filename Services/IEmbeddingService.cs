using System.Collections.Generic;

namespace AzureEmbeddingAPI.Services
{
    public interface IEmbeddingService
    {
        List<string> GenerateEmbeddings(List<string> corps);
    }
}
