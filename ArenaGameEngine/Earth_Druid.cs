using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArenaGameEngine
{
    public class Earth_Druid : Hero
    {
        private int mana;
        private int stoneSkinCooldown;
        private int quakeCooldown;

        public Earth_Druid() : this("Gorath Stonebreaker")
        {
        }

        public Earth_Druid(string name) : base(name)
        {
            mana = 100; // Initialize with full mana
            stoneSkinCooldown = 0; // Initialize Stone Skin cooldown
            quakeCooldown = 0; // Initialize Earthquake cooldown
        }

        public override int Attack()
        {
            int baseAttack = base.Attack();
            mana += Random.Shared.Next(5, 10); // Regenerate some mana with each attack

            if (mana >= 25)
            {
                mana -= 25;
                Console.WriteLine($"{Name} casts Rock Throw!");
                baseAttack += Strength * 2; // Rock throw attack consuming mana
            }

            return baseAttack;
        }

        public override void TakeDamage(int incomingDamage)
        {
            int healthThreshold = Random.Shared.Next(0, 100);

            if (stoneSkinCooldown == 0 && healthThreshold <= 20)
            {
                incomingDamage /= 2; // Reduce damage by half
                stoneSkinCooldown = 6; // Stone Skin ability with cooldown
                Console.WriteLine($"{Name} activates Stone Skin and reduces the damage by half!");
            }
            else
            {
                base.TakeDamage(incomingDamage);
            }

            if (stoneSkinCooldown > 0)
            {
                stoneSkinCooldown--; // Decrease cooldown each turn
            }
        }

        public void CastEarthquake()
        {
            if (mana >= 75 && quakeCooldown == 0)
            {
                mana -= 75;
                quakeCooldown = 10; // Earthquake ability with cooldown
                Console.WriteLine($"{Name} casts Earthquake, dealing massive area damage and stunning enemies!");
                // Implement the area damage and stun effect in the game logic
            }
            else if (quakeCooldown > 0)
            {
                Console.WriteLine($"{Name}'s Earthquake is on cooldown.");
            }
            else
            {
                Console.WriteLine($"{Name} doesn't have enough mana to cast Earthquake.");
            }
        }
    }
}
