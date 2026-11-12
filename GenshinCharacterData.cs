using System.Text.Json.Serialization;

namespace GameCoreProject
{
    // Цей клас описує, як виглядає відповідь від API
    public class GenshinCharacterData
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("rarity")]
        public int Rarity { get; set; }

        [JsonPropertyName("element")]
        public string Element { get; set; }

        // Ми можемо використати ці дані для створення нашого CharacterProfile
        public CharacterProfile ToCharacterProfile()
        {
            // Оскільки API не дає нам ігрових статів (HP, Damage), ми згенеруємо їх на основі рідкості (Rarity)
            int hp = Rarity == 5 ? 150 : 100;
            int dmg = Rarity == 5 ? 40 : 25;
            int armor = Rarity == 5 ? 20 : 10;
            double resist = Rarity == 5 ? 0.2 : 0.1; // 20% або 10%
            int speed = 100; // Базова швидкість

            return new CharacterProfile($"{Name} ({Element})", hp, dmg, armor, resist, speed);
        }
    }
}