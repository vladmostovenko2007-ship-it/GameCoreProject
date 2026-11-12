using System;
using System.Threading.Tasks;

namespace GameCoreProject
{
    class Program
    {
        // Головний метод програми. Використовуємо async Task, бо ми чекаємо на відповідь від API
        static async Task Main(string[] args)
        {
            // Встановлюємо кодування UTF-8, щоб українські літери в консолі відображалися правильно
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("=== Ініціалізація гри ===");
            CharacterApiService apiService = new CharacterApiService();

            // 1. Завантажуємо дані з API (або використовуємо заглушки, якщо API не працює)
            var charData1 = await apiService.GetCharacterFromApiAsync("Diluc");
            var charData2 = await apiService.GetCharacterFromApiAsync("Amber");

            // Перевірка, чи взагалі завантажились дані
            if (charData1 == null || charData2 == null)
            {
                Console.WriteLine("Критична помилка: Не вдалося отримати дані персонажів.");
                return;
            }

            // 2. Перетворюємо дані з API у наші ігрові профілі
            CharacterProfile p1Profile = charData1.ToCharacterProfile();
            CharacterProfile p2Profile = charData2.ToCharacterProfile();

            // === НАЛАШТУВАННЯ ШВИДКОСТІ ДЛЯ ТЕСТУ ===
            // Робимо Ділюка дуже швидким (200), а Ембер звичайною (100).
            // Завдяки цьому Ділюк буде бити вдвічі частіше.
            p1Profile.Speed = 200; 
            p2Profile.Speed = 100; 
            // ========================================

            // Створюємо бойових персонажів на основі профілів
            Character hero = new Character(p1Profile);
            Character enemy = new Character(p2Profile);

            // 3. Налаштовуємо конфігурацію гри
            GameConfig config = new GameConfig(hero, enemy);

            // 4. Створюємо екземпляр гри та запускаємо бій
            Game myGame = new Game();
            
            // Передаємо налаштування грі
            myGame.SetupGame(config);
            
            // Запускаємо головний цикл бою
            myGame.StartGame();

            // Чекаємо натискання клавіші, щоб консоль не закрилася відразу після бою
            Console.WriteLine("\nНатисніть будь-яку клавішу для виходу...");
            Console.ReadKey();
        }
    }
}