using Azure;
using Azure.AI.OpenAI;
using AzureEmbeddingAPI.Models;
using OpenAI.Embeddings;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AzureEmbeddingAPI.Services
{
    public class EmbeddingService
    {
        private readonly AzureOpenAIClient _client;
        private readonly string _embeddingModel;

        public EmbeddingService(string endpoint, string apiKey, string embeddingModel)
        {
            var uri = new Uri(endpoint);
            var credentials = new AzureKeyCredential(apiKey);
            _client = new AzureOpenAIClient(uri, credentials);
            _embeddingModel = embeddingModel;
        }

        public List<EmbeddingResponse> GenerateEmbeddings(List<string> companies)
        {
            var embedClient = _client.GetEmbeddingClient(_embeddingModel);
            var embedOptions = new EmbeddingGenerationOptions();

            var embeds = embedClient.GenerateEmbeddings(companies, embedOptions);
            var embeddingsList = new List<Tuple<string, float[]>>();

            foreach (var em in embeds.Value)
            {
                var vector = em.Vector.ToArray();
                embeddingsList.Add(new Tuple<string, float[]>(companies[em.Index], vector));
            }

            var outputList = new List<EmbeddingResponse>();

            foreach (var inputCompany in companies)
            {
                var inputID = companies.IndexOf(inputCompany) + 1;
                var targetEmbedding = embeddingsList.FirstOrDefault(e => e.Item1 == inputCompany)?.Item2;

                if (targetEmbedding != null)
                {
                    var similarityScores = new List<Tuple<string, double, int>>();
                    foreach (var item in embeddingsList)
                    {
                        if (item.Item1 != inputCompany)
                        {
                            var similarity = CalculateCosineSimilarity(targetEmbedding, item.Item2);
                            similarityScores.Add(new Tuple<string, double, int>(item.Item1, similarity, embeddingsList.IndexOf(item) + 1));
                        }
                    }

                    var topMatches = similarityScores.OrderByDescending(x => x.Item2).Take(10).ToList();
                    var matchType = topMatches.Any(x => x.Item1 == inputCompany) ? "Exact" : "Partial phrase";
                    var matches = topMatches.Select(match => new Match
                    {
                        MatchName = match.Item1,
                        Weighting = match.Item2,
                        MatchID = match.Item3
                    }).ToList();

                    outputList.Add(new EmbeddingResponse
                    {
                        InputID = inputID,
                        DataType = "Corps",
                        InputName = inputCompany,
                        MatchType = matchType,
                        Matches = matches
                    });
                }
            }

            return outputList;
        }

        private double CalculateCosineSimilarity(float[] vectorA, float[] vectorB)
        {
            double dotProduct = 0.0;
            double magnitudeA = 0.0;
            double magnitudeB = 0.0;

            for (int i = 0; i < vectorA.Length; i++)
            {
                dotProduct += vectorA[i] * vectorB[i];
                magnitudeA += vectorA[i] * vectorA[i];
                magnitudeB += vectorB[i] * vectorB[i];
            }

            magnitudeA = Math.Sqrt(magnitudeA);
            magnitudeB = Math.Sqrt(magnitudeB);

            if (magnitudeA == 0 || magnitudeB == 0)
            {
                return 0.0;
            }
            else
            {
                return dotProduct / (magnitudeA * magnitudeB);
            }
        }
    }
}
