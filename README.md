# Azure Embedding API

This project implements a RESTful API using ASP.NET Core for generating embeddings and calculating similarity scores for companies using Azure OpenAI.

## Table of Contents
1. [Features](#features)
2. [Prerequisites](#prerequisites)
3. [Setup](#setup)
4. [Usage](#usage)
5. [API Endpoints](#api-endpoints)
6. [Testing](#testing)
7. [Dependencies](#dependencies)
8. [Contributing](#contributing)
9. [License](#license)

## Features
- **Embedding Generation:** Generates embeddings for a list of company names.
- **Similarity Calculation:** Calculates cosine similarity between generated embeddings.
- **RESTful API:** Exposes endpoints to interact with the embedding generation and similarity calculation functionalities.

## Prerequisites
- [.NET 5.0](https://dotnet.microsoft.com/download/dotnet/5.0)
- Azure OpenAI API credentials (Endpoint URL and Key)
- Visual Studio (or Visual Studio Code) for development

## Setup
1. Clone the repository:
   ```
   bash
   git clone git@github.com:poswalabhishek/AzureEmbeddingAPI.git 
   ```
    **Azure Embedding API**

    - This project implements a RESTful API using ASP.NET Core for generating embeddings and calculating similarity scores for companies using Azure OpenAI.


2. **Open the solution in Visual Studio:**

3. **Configure `appsettings.json` with your Azure OpenAI credentials:**

   ```json
   {
     "OpenAI": {
       "Endpoint": "YOUR_OPENAI_ENDPOINT_URL",
       "Key": "YOUR_OPENAI_KEY",
       "ModelName": "YOUR_OPENAI_MODEL_NAME"
     }
   }
   ```
   
4. **Build the Solution**

- Build the solution to restore NuGet packages.

5. **Usage**

- Run the project in Visual Studio.
- Use tools like Postman or Visual Studio Code with REST Client extension to interact with the API endpoints.

6. **API Endpoints**

- **POST `/api/embedding/generate`**

  - Generates embeddings for a list of companies provided in the request body.

  **Example Request Body:**

  ```json
  {
    "Corps": ["CompanyA", "CompanyB", "CompanyC"]
  }
  ```
  ```json
  [
    {
      "InputID": 1,
      "DataType": "Corps",
      "InputName": "CompanyA",
      "MatchType": "Exact",
      "Matches": [
      {
          "MatchName": "CompanyB",
          "Weighting": 0.85,
          "MatchID": 2
        },
        ...
      ]
    },
    ...
  ]
  ```

7. **Testing**

- Ensure the API is running.
- Use the provided HTTP test file (AzureEmbeddingAPI.http) or tools like Postman to send requests to the API.

8. **Dependencies**

- Newtonsoft.Json
- Azure.AI.OpenAI
- Other dependencies as listed in .csproj file.

9. **Contributing**

- Contributions are welcome. For major changes, please open an issue first to discuss what you would like to change.
