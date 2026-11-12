using System;
using System.Collections.Generic;

namespace GameCoreProject
{
    public class Game
    {
        private GameConfig _config;
        private List<Turn> _history; // Історія кроків
        private int _currentTurnNumber;
        private SaveGameService _saveService; // Сервіс для збереження

        public Game()
        {
            _history = new List<Turn>();
            _currentTurnNumber = 1;
            _saveService = new SaveGameService(); 
        }

        public void SetupGame(GameConfig config)
        {
            _config = config;
            Console.WriteLine($"Гра почалась: {config.Player1.Profile.Name} проти {config.Player2.Profile.Name}");
        }

        public void StartGame()
        {
            if (_config == null) return;

            Character p1 = _config.Player1;
            Character p2 = _config.Player2;

            Console.WriteLine("\n--- ПОЧАТОК БОЮ ---");
            
            // Даємо кожному по 100 очок дій на старт
            int p1ActionPoints = 100;
            int p2ActionPoints = 100;

            // Ігровий цикл (поки обидва живі)
            while (p1.IsAlive() && p2.IsAlive())
            {
                // Визначаємо, хто діє (у кого більше очок дій)
                Character attacker;
                Character defender;

                if (p1ActionPoints >= p2ActionPoints)
                {
                    attacker = p1;
                    defender = p2;
                }
                else
                {
                    attacker = p2;
                    defender = p1;
                }

                // Атакуємо (для тесту фізичною атакою)
                int damage = attacker.CalculateDamage(defender, isMagic: false);
                defender.TakeDamage(damage);

                // Записуємо крок в історію
                _history.Add(new Turn(_currentTurnNumber, attacker, defender, damage));
                Console.WriteLine($"Хід {_currentTurnNumber}: {attacker.Profile.Name} б'є {defender.Profile.Name} на {damage} шкоди. (HP ворога: {defender.CurrentHealth})");

                // Зберігаємо на 3-му ході (для перевірки роботи Save)
                if (_currentTurnNumber == 3) 
                {
                    _saveService.SaveGame(_config);
                }

                _currentTurnNumber++;

                // === МЕХАНІКА ШВИДКОСТІ ===
                // Забираємо 100 очок у того, хто бив, і додаємо його швидкість
                if (attacker == p1)
                {
                    p1ActionPoints -= 100;
                    p1ActionPoints += p1.Profile.Speed; 
                    p2ActionPoints += p2.Profile.Speed; 
                }
                else
                {
                    p2ActionPoints -= 100;
                    p2ActionPoints += p2.Profile.Speed;
                    p1ActionPoints += p1.Profile.Speed;
                }
                
                // Невелика пауза для краси в консолі
                System.Threading.Thread.Sleep(700); 
            }

            Character winner = p1.IsAlive() ? p1 : p2;
            Console.WriteLine($"\nГра закінчена! Переможець: {winner.Profile.Name}");
        }
    }
}