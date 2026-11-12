using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;

namespace GameCoreProject
{
    public class CharacterApiService
    {
        private static readonly HttpClient client = new HttpClient();

        public async Task<GenshinCharacterData?> GetCharacterFromApiAsync(string characterName)
        {
            try
            {
                string url = $"https://genshin.jmp.blue/characters/{characterName.ToLower()}";
                Console.WriteLine($"Завантаження даних для: {characterName}...");
                
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();

                string jsonString = await response.Content.ReadAsStringAsync();
                
                // Додано ? після типу, бо сервер може повернути null
                GenshinCharacterData? data = JsonSerializer.Deserialize<GenshinCharacterData>(jsonString);

                if (data == null) throw new Exception("Отримано порожні дані");
                return data;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка API ({ex.Message}). Використовуємо тестові дані (Mock)...");
                return GetMockCharacterData(characterName);
            }
        }

        private GenshinCharacterData GetMockCharacterData(string name)
        {
            if (name.ToLower() == "diluc")
            {
                return new GenshinCharacterData { Name = "Diluc (Mock)", Rarity = 5, Element = "Pyro" };
            }
            else if (name.ToLower() == "amber")
            {
                return new GenshinCharacterData { Name = "Amber (Mock)", Rarity = 4, Element = "Pyro" };
            }
            else
            {
                return new GenshinCharacterData { Name = "Unknown (Mock)", Rarity = 4, Element = "Physical" };
            }
        }
    }
}