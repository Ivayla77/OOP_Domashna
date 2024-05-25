using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArenaGameEngine
{
    public class Arena
    {
        public List<Hero> Heroes { get; private set; }

        public GameEventListener EventListener { get; set; }

        public Arena(Hero hero1, Hero hero2, Hero hero3, Hero hero4)
        {
            Heroes = new List<Hero> { hero1, hero2, hero3, hero4 };
        }

        public Hero Battle()
        {
            while (Heroes.Count(h => !h.IsDead) > 1)
            {
                for (int i = 0; i < Heroes.Count; i++)
                {
                    if (Heroes[i].IsDead)
                    {
                        continue;
                    }

                    Hero attacker = Heroes[i];
                    Hero defender = FindNextLivingHero(attacker);

                    if (defender == null)
                    {
                        break; // No more living defenders, battle ends
                    }

                    int damage = attacker.Attack();
                    defender.TakeDamage(damage);

                    if (EventListener != null)
                    {
                        EventListener.GameRound(attacker, defender, damage);
                    }

                    if (defender.IsDead && Heroes.Count(h => !h.IsDead) == 1)
                    {
                        return attacker; // Only one hero left standing
                    }
                }
            }

            return Heroes.FirstOrDefault(h => !h.IsDead); // Return the last hero standing
        }

        private Hero FindNextLivingHero(Hero attacker)
        {
            int attackerIndex = Heroes.IndexOf(attacker);
            for (int i = 1; i < Heroes.Count; i++)
            {
                int defenderIndex = (attackerIndex + i) % Heroes.Count;
                if (!Heroes[defenderIndex].IsDead)
                {
                    return Heroes[defenderIndex];
                }
            }
            return null;
        }
    }
}
