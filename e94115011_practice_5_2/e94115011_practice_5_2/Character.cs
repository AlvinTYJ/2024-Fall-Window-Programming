using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e94115011_practice_5_2
{
    public class Character
    {
        public string Name { get; set; }
        public int HP { get; set; }
        public int MaxHP { get; set; }
        public int ATK { get; set; }
        public int DEF { get; set; }
        public int DeploymentCost { get; set; }
        public int CooldownTime { get; set; }
        public int RemainingCooldown { get; set; }
        public bool IsCooldownComplete => RemainingCooldown <= 0;

        public Character(string name, int hp, int atk, int def, int cost, int cooldown)
        {
            Name = name;
            MaxHP = hp;
            HP = hp;
            ATK = atk;
            DEF = def;
            DeploymentCost = cost;
            CooldownTime = cooldown;
            RemainingCooldown = cooldown;
        }

        public void TakeDamage(int damage)
        {
            int actualDamage = damage - DEF;
            if (actualDamage > 0) HP -= actualDamage;
            if (HP < 0) HP = 0;
        }

        public void Attack(Enemy e)
        {
            e.TakeDamage(ATK);
        }

        public void ResetCooldown() => RemainingCooldown = CooldownTime;

        public void ReduceCooldown()
        {
            RemainingCooldown -= 1;
            RemainingCooldown = Math.Max(RemainingCooldown, 0);
        }
    }

    public class Cardigan : Character
    {
        public Cardigan() : base("Cardigan", 2130, 305, 475, 18, 20) { }
        public void UseSkill()
        {
            if (IsCooldownComplete)
            {
                int healthToRecover = (int)(MaxHP * 0.4);
                HP = Math.Min(HP + healthToRecover, MaxHP);
                ResetCooldown();
            }
        }
    }

    public class Myrtle : Character
    {
        private Form1 form;
        public Myrtle(Form1 form) : base("Myrtle", 1565, 520, 300, 10, 22)
        {
            this.form = form;
        }
        public void UseSkill()
        {
            if (IsCooldownComplete)
            {
                form.deploymentPoints += 14;
                ResetCooldown();
            }
        }
    }
    public class Melantha : Character
    {
        public Melantha() : base("Melantha", 2745, 738, 155, 15, 40) { }
        public void UseSkill(EnemyUI enemyUI)
        {
            if (IsCooldownComplete)
            {
                enemyUI.Enemy.TakeDamage(ATK * 2);

                Debug.WriteLine($"{Name} uses skill and attacks {enemyUI.Enemy} with 200% ATK!");
                ResetCooldown();
            }
        }
    }
}