using System;
using System.IO;
using System.Text.Json;

namespace GameCoreProject
{
    public class SaveGameService
    {
        private string _filePath = "savegame.json";

        public void SaveGame(GameConfig config)
        {
            try
            {
                var options = new JsonSerializerOptions { WriteIndented = true };
                string jsonString = JsonSerializer.Serialize(config, options);
                
                File.WriteAllText(_filePath, jsonString);
                Console.WriteLine($"\n[Система]: Гру успішно збережено у файл {_filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n[Помилка збереження]: {ex.Message}");
            }
        }

        // Додано ? після GameConfig, бо файлу може не існувати
        public GameConfig? LoadGame()
        {
            try
            {
                if (!File.Exists(_filePath))
                {
                    Console.WriteLine("\n[Система]: Файл збереження не знайдено.");
                    return null;
                }

                string jsonString = File.ReadAllText(_filePath);
                
                // Додано ? після типу
                GameConfig? config = JsonSerializer.Deserialize<GameConfig>(jsonString);
                
                Console.WriteLine($"\n[Система]: Гру успішно завантажено з файлу {_filePath}");
                return config;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n[Помилка завантаження]: {ex.Message}");
                return null;
            }
        }
    }
}