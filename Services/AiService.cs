using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Dto;

namespace Services
{
    public class AiService : IAiService
    {
        private readonly HttpClient _httpClient;
        private const string GroqApiKey = "";
        private const string GroqApiUrl = "https://api.groq.com/openai/v1/chat/completions";

        public AiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<int>> AnalyzeImageAsync(
            IFormFile image,
            IEnumerable<DtoProduct_Id_Name_Category_Price_Desc_Image> allProducts,
            IEnumerable<DtoSyle_id_name> styles,
            IEnumerable<DtoCategory_Name_Id> categories)
        {
            if (image == null) return new List<int>();

     
            var stylesContext = string.Join(", ", styles.Select(s => $"{{id:{s.StyleId}, name:'{s.Name}'}}"));
            var categoriesContext = string.Join(", ", categories.Select(c => $"{{id:{c.CategoryId}, name:'{c.Name}'}}"));

            var productListContext = string.Join(", ", allProducts.Select(p => {
                var sIds = p.ProductStyles != null
                           ? string.Join(",", p.ProductStyles.Select(ps => ps.StyleId))
                           : "";

                return $"{{id:{p.ProductId}, name:'{p.Name}', desc:'{p.Description}', catId:{p.CategoryId}, styleIds:[{sIds}]}}";
            }));

          
            using var ms = new MemoryStream();
            await image.CopyToAsync(ms);
            var base64Image = Convert.ToBase64String(ms.ToArray());

            
            var prompt = $@"
                Act as an Interior Design Expert. 
                Styles: [{stylesContext}]
                Categories: [{categoriesContext}]
                Catalog: [{productListContext}]

                Task: Match the room in the image to a Style ID, then select 10-15 products with that Style ID. 
                Ensure variety across different 'catId' values.
                Return ONLY JSON: {{ ""productIds"": [1, 2, 3] }}";

          
            var requestBody = new
            {
                model = "meta-llama/llama-4-scout-17b-16e-instruct",
                messages = new[] {
                    new {
                        role = "user",
                        content = new object[] {
                            new { type = "text", text = prompt },
                            new { type = "image_url", image_url = new { url = $"data:image/jpeg;base64,{base64Image}" } }
                        }
                    }
                },
                temperature = 0.8,
                response_format = new { type = "json_object" }
            };

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GroqApiKey);
            var content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(GroqApiUrl, content);

            if (!response.IsSuccessStatusCode) return new List<int>();

            var responseString = await response.Content.ReadAsStringAsync();
            return ParseIdsFromResponse(responseString);
        }

        private List<int> ParseIdsFromResponse(string jsonResponse)
        {
            try
            {
                using var doc = JsonDocument.Parse(jsonResponse);
                var content = doc.RootElement.GetProperty("choices")[0].GetProperty("message").GetProperty("content").GetString();
                using var contentDoc = JsonDocument.Parse(content);
                return JsonSerializer.Deserialize<List<int>>(contentDoc.RootElement.GetProperty("productIds").GetRawText()) ?? new List<int>();
            }
            catch { return new List<int>(); }
        }
    }
}