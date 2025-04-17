using System.Speech.Synthesis;

class Program
{
    static void Main(string[] args)
    {
        SpeechSynthesizer speechSynthesizer = new();
        speechSynthesizer.Volume=100; // Sesin ses seviyesini ayarlıyoruz (0-100)
        speechSynthesizer.Rate = 0; // Sesin hızını ayarlıyoruz (-10 ile 10 arasında)
        Console.WriteLine("Lütfen Seslendirmek İstediğiniz Cümleyi Giriniz..");
        string inputText = Console.ReadLine(); // Kullanıcıdan cümle alıyoruz
        if(!string.IsNullOrEmpty(inputText)) // Eğer cümle boş değilse
        {
            speechSynthesizer.Speak(inputText); // Cümleyi seslendiriyoruz
            Console.ReadLine(); // Kullanıcının bir tuşa basmasını bekliyoruz
        }
        else
        {
            Console.WriteLine("Boş bir cümle girdiniz. Lütfen geçerli bir cümle girin."); // Hata mesajı
        }
    }
}