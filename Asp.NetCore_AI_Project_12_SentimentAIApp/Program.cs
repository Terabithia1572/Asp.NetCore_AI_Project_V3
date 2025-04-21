using Newtonsoft.Json;
using System.Text;

class Program
{
    private static readonly string apiKey = "";
    static async Task Main(string[] args)
    {
        Console.Write("Lütfen bir metin giriniz: ");
        string inputText = Console.ReadLine();
        if (!string.IsNullOrEmpty(inputText))
        {
            Console.WriteLine();
            Console.WriteLine("Metin Duygu analizi yapılıyor...");
            Console.WriteLine();
            string sentiment = "buraya method gelecek";
            Console.WriteLine($"Metin Duygu analizi sonucu: {sentiment}");
        }
        else
        {
            Console.WriteLine("Lütfen geçerli bir metin giriniz.");
        }
    }
    static async Task<string> AnalyzeSentiment(string text)
    {
        using (HttpClient client = new HttpClient())
        {
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
            var requestBody = new
            {
                model = "gpt-3.5-turbo",
                messages = new[]
                {
                        new {role="system",content="You are an AI that analyzes sentiment. You categorize text as Positive, Negative or Neutral."},
                        new {role="user",content=$"Analyze the sentiment of this text: \"{text}\" and return only Positive, Negative or Neutral" }
                    }
            };

            string json = JsonConvert.SerializeObject(requestBody);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync("https://api.openai.com/v1/chat/completions", content);

        
        }
    }
}
 
