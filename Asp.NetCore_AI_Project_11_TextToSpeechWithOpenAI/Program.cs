using Newtonsoft.Json;
using System.Text;

class Program
{
    private readonly static string apiKey="";
    static void Main(string[] args)
    {
        Console.WriteLine("Lütfen Seslendirmek İstediğiniz Cümleyi Giriniz..");
        string inputText = Console.ReadLine(); // Kullanıcıdan cümle alıyoruz
        if (!string.IsNullOrEmpty(inputText))
        {
            Console.WriteLine("Seslendirme Başlıyor...");

        }
    
    static async Task GenerateSpeech(string text)
    {
        using (HttpClient httpClient = new HttpClient())
        {
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}"); // OpenAI API anahtarını ekliyoruz
            var requestBody = new //
            {
               model="tts-1", // OpenAI modelini belirtiyoruz
                input =text, // Kullanıcıdan alınan metni seslendirmek için kullanıyoruz
                voice = "alloy" // Seslendirme için kullanılacak sesi belirtiyoruz
            };
            string jsonBody = JsonConvert.SerializeObject(requestBody); // JSON nesnesini serileştiriyoruz
            HttpContent content = new StringContent(jsonBody, Encoding.UTF8, "application/json"); // JSON içeriğini oluşturuyoruz
        }
    }
}