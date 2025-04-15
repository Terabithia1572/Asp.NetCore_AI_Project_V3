using Newtonsoft.Json;
using System.Text;

class Program
{
    private static async Task Main(string[] args)
    {
        Console.WriteLine("Lütfen Çevirmek İstediğiniz Cümleyi Giriniz..");
        string inputText = Console.ReadLine();// Kullanıcıdan cümle alıyoruz
        string apiKey= "Buraya Api Key Gelecek"; // OpenAI API anahtarınızı buraya ekleyin
        string translatedText = await TranslateTextToEnglish(inputText, apiKey); // Cümleyi İngilizceye çeviriyoruz
        Console.WriteLine("Çevrilen Cümle: " + translatedText); // Çevrilen cümleyi yazdırıyoruz

    }
    private static async Task<string> TranslateTextToEnglish(string text, string apiKey)
    {
        using (HttpClient client = new HttpClient()) // HttpClient nesnesi oluşturuyoruz
        {
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}"); // OpenAI API anahtarını ekliyoruz
            var requestBody = new // JSON nesnesi oluşturuyoruz
            {
                model = "gpt-3.5-turbo", // OpenAI modelini belirtiyoruz
                messages = new[] // JSON mesajları dizisi oluşturuyoruz
                {

                    new { role="system", content="You are a helpful assistant." }, // Sistem mesajı
                    new { role="user", content=$"Translate the following text to English: {text}" } // Kullanıcı mesajı

                }
            };
            string jsonBody = JsonConvert.SerializeObject(requestBody); // JSON nesnesini serileştiriyoruz
            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json"); // JSON içeriğini oluşturuyoruz
                                                                                          //   var response = await client.PostAsync("https://api.openai.com/v1/chat/completions", content); // API isteği gönderiyoruz
            try
            {
                HttpResponseMessage response = await client.PostAsync("https://api.openai.com/v1/chat/completions", content);
                string responseString = await response.Content.ReadAsStringAsync();

                dynamic responseObject = JsonConvert.DeserializeObject(responseString);
                string translation = responseObject.choices[0].message.content;

                return translation;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Bir hata oluştu: {ex.Message}");
                return null;
            }
        }
    }
}