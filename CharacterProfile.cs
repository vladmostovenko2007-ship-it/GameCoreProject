namespace GameCoreProject
{
    public class CharacterProfile
    {
        public string Name { get; set; }
        public int BaseHealth { get; set; }
        public int BaseDamage { get; set; }
        public int Armor { get; set; } // Флатова броня
        public double Resist { get; set; } // Відсоток резисту (від 0.0 до 1.0)
        public int Speed { get; set; } // Для системи ходів

        public CharacterProfile(string name, int health, int damage, int armor, double resist, int speed)
        {
            Name = name;
            BaseHealth = health;
            BaseDamage = damage;
            Armor = armor;
            Resist = resist;
            Speed = speed;
        }
    }
}