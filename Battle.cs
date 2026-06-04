using System;
using System.Transactions;

class Battle
{
    private Fighter current;
    private Fighter other;
    private int round;

    public Battle(Fighter current, Fighter other, int round = 0)
    {
        int rnd = Random.Shared.Next(2);
        if(rnd == 0)
        {
            this.current = current;
            this.other = other;
        }
        else
        {
            this.current = other;
            this.other = current;
        }
        this.round = round;
    }

    public void Attack()
    {
        current.Attack();
        other.Hurt();
    }
    
    public bool IsAlive() => other.IsAlive();

    public void NextRound()
    {
        Fighter temp = current;
        current = other;
        other = temp;
        round++;
    }

    public Fighter Winner() => current;
}