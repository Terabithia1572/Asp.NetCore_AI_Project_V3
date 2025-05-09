﻿using Newtonsoft.Json;
using System.Text;

class Program
{
    private static readonly string apiKey = "";
    static async Task Main(string[] args)
    {
        Console.Write("Lütfen bir metin giriniz: "); // Kullanıcıdan metin girişi istenir
        string inputText = Console.ReadLine(); // Kullanıcıdan metin alınır
        if (!string.IsNullOrEmpty(inputText)) 
        {
            Console.WriteLine(); // Boşluk bırakılır
            Console.WriteLine("Metin Duygu analizi yapılıyor..."); // Kullanıcıya bilgi verilir
            Console.WriteLine(); // Boşluk bırakılır
            string sentiment = await AnalyzeSentiment(inputText); //Metin duygu analizi yapılır
            Console.WriteLine($"Metin Duygu analizi sonucu: {sentiment}"); // // Sonuç yazdırılır
        }
        else
        {
            Console.WriteLine("Lütfen geçerli bir metin giriniz."); // Kullanıcıdan geçerli bir metin girmesi istenir
        }
    }
    static async Task<string> AnalyzeSentiment(string text) // Metin duygu analizi yapan metod
    {
        using (HttpClient client = new HttpClient()) // HttpClient nesnesi oluşturulur
        {
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}"); // API anahtarı eklenir
            var requestBody = new 
            {
                model = "gpt-3.5-turbo", // Model adı
                messages = new[]
                {
                        new {role="system",content="You are an AI that analyzes sentiment. You categorize text as Positive, Negative or Neutral."}, 
                        new {role="user",content=$"Analyze the sentiment of this text: \"{text}\" and return only Positive, Negative or Neutral" } // Kullanıcıdan gelen metin
                    }
            };

            string json = JsonConvert.SerializeObject(requestBody); // JSON formatına dönüştürülür
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json"); // İçerik oluşturulur

            HttpResponseMessage response = await client.PostAsync("https://api.openai.com/v1/chat/completions", content); // API'ye POST isteği gönderilir

            string responseJson = await response.Content.ReadAsStringAsync(); // Yanıt okunur
            if (response.IsSuccessStatusCode) // Başarılı bir yanıt alındıysa
            {
                var result = JsonConvert.DeserializeObject<dynamic>(responseJson); // Yanıt JSON formatında ayrıştırılır
                return result.choices[0].message.content.ToString(); // Sonuç döndürülür
            }
            else
            {
                Console.WriteLine("Bir hata oluştu" + responseJson); // Hata mesajı yazdırılır
                return "Hata"; // Hata durumu döndürülür
            }
        }
    }
}
 
