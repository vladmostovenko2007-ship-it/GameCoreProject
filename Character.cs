using System;

namespace GameCoreProject
{
    public class Character
    {
        public CharacterProfile Profile { get; private set; }
        public int CurrentHealth { get; private set; }

        public Character(CharacterProfile profile)
        {
            Profile = profile;
            CurrentHealth = profile.BaseHealth;
        }

        // Перевірка, чи живий персонаж
        public bool IsAlive()
        {
            return CurrentHealth > 0;
        }

        // Метод отримання шкоди
        public void TakeDamage(int damage)
        {
            CurrentHealth -= damage;
            if (CurrentHealth < 0) CurrentHealth = 0;
        }

        // Тут буде логіка атаки (ми винесемо її окремо для зручності)
        public int CalculateDamage(Character target, bool isMagic)
        {
            int finalDamage = 0;

            if (isMagic)
            {
                // Магія ігнорує броню, але враховує резист
                finalDamage = (int)(Profile.BaseDamage * (1.0 - target.Profile.Resist));
            }
            else
            {
                // Фізична шкода: Віднімаємо броню
                int damageAfterArmor = Profile.BaseDamage - target.Profile.Armor;
                
                // Якщо броня повністю заблокувала урон
                if (damageAfterArmor < 0) damageAfterArmor = 0; 

                // Накладаємо резист
                finalDamage = (int)(damageAfterArmor * (1.0 - target.Profile.Resist));
            }

            // Шкода не може бути від'ємною
            return Math.Max(0, finalDamage);
        }
    }
}