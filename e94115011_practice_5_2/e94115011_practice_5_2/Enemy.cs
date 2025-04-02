using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace e94115011_practice_5_2
{
    public class Enemy
    {
        public int HP { get; set; }
        public int MaxHP { get; set; }
        public int ATK { get; set; }
        public int DEF { get; set; }

        public Enemy(int hp, int atk, int def)
        {
            MaxHP = hp;
            HP = hp;
            ATK = atk;
            DEF = def;
        }

        public void TakeDamage(int damage)
        {
            int actualDamage = damage - DEF;
            if (actualDamage > 0) HP -= actualDamage;
            if (HP < 0) HP = 0;
        }

        public void Attack(Character c)
        {
            c.TakeDamage(ATK);
        }

        public bool IsDead => HP <= 0;
    }

    public class EnemyUI
    {
        public Enemy Enemy { get; set; }
        public Panel Panel { get; set; }
        public Label InfoLabel { get; set; }
        public Timer MoveTimer { get; set; }

        public EnemyUI(Enemy enemy, Panel panel, Label infoLabel, Timer moveTimer)
        {
            Enemy = enemy;
            Panel = panel;
            InfoLabel = infoLabel;
            MoveTimer = moveTimer;
        }
    }
}
