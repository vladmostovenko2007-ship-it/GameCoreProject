namespace GameCoreProject
{
    public class Turn
    {
        public int TurnNumber { get; set; }
        public Character Attacker { get; set; }
        public Character Defender { get; set; }
        public int DamageDealt { get; set; }

        public Turn(int turnNumber, Character attacker, Character defender, int damageDealt)
        {
            this.TurnNumber = turnNumber;
            this.Attacker = attacker;
            this.Defender = defender;
            this.DamageDealt = damageDealt;
        } 
    }
}