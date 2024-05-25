using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArenaGameEngine
{
    public class Ice_Mage : Hero
    {
        private int hitCount;
        private int mana;
        private int iceBarrierCooldown;
        public Ice_Mage() : this("Frostbane Glacius")
        {
        }

        public Ice_Mage(string name) : base(name)
        {
            hitCount = 0;
            mana = 100; // Initialize with full mana
            iceBarrierCooldown = 0; // Initialize ice barrier cooldown
        }

        public override int Attack()
        {
            int baseAttack = base.Attack();
            mana += Random.Shared.Next(5, 15); // Regenerate some mana with each attack

            if (mana >= 30)
            {
                mana -= 30;
                Console.WriteLine($"{Name} casts Ice Spike!");
                baseAttack += Strength * 2; // Powerful ice spike attack consuming mana
            }

            return baseAttack;
        }

        public override void TakeDamage(int incomingDamage)
        {
            int healthThreshold = Random.Shared.Next(0, 100);

            if (iceBarrierCooldown == 0 && healthThreshold <= 25)
            {
                incomingDamage /= 2; // Reduce damage by half
                iceBarrierCooldown = 5; // Ice barrier ability with cooldown
                Console.WriteLine($"{Name} activates Ice Barrier and reduces the damage by half!");
            }
            else
            {
                base.TakeDamage(incomingDamage);
            }

            if (iceBarrierCooldown > 0)
            {
                iceBarrierCooldown--; // Decrease cooldown each turn
            }
        }
    }
}
