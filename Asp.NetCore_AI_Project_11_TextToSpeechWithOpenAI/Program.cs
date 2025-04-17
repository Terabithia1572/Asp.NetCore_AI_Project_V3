using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;

class Program
{
    private readonly static string apiKey="";
    static async Task Main(string[] args)
    {
        Console.WriteLine("Lütfen Seslendirmek İstediğiniz Cümleyi Giriniz..");
        string inputText = Console.ReadLine(); // Kullanıcıdan cümle alıyoruz
        if (!string.IsNullOrEmpty(inputText))
        {
            Console.WriteLine("Seslendirme Başlıyor...");
            await GenerateSpeech(inputText); // Seslendirme fonksiyonunu çağırıyoruz
            Console.WriteLine("Seslendirme Tamamlandı. Çıkmak için bir tuşa basın.");
            System.Diagnostics.Process.Start("explorer.exe","output.mp3"); // Ses dosyasını oynatıyoruz

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

                HttpResponseMessage httpResponseMessage = await httpClient.PostAsync("https://api.openai.com/v1/audio/generate", content); // API isteği gönderiyoruz
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    byte[] audioBytes = await httpResponseMessage.Content.ReadAsByteArrayAsync();
                    await File.WriteAllBytesAsync("output.mp3", audioBytes); // Ses dosyasını kaydediyoruz
                }
                else
                {
                    string errorResponse = await httpResponseMessage.Content.ReadAsStringAsync();
                    Console.WriteLine($"Hata: {httpResponseMessage.StatusCode}, Yanıt: {errorResponse}"); // Hata mesajını yazdırıyoruz
                }
            }
    }
}