
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class Fighter
{
    private Animation idle, attack, hurt;
    private Animation current;

    private Vector2 position;
    private SpriteEffects spriteEffect;
    private int hp;


    public Fighter(Vector2 position, Animation idle, Animation attack, Animation hurt, SpriteEffects spriteEffect, int hp)
    {
        this.idle = idle;
        this.attack = attack;
        this.hurt = hurt;
        this.position = position;
        this.spriteEffect = spriteEffect;
        this.hp = hp;
        current = idle;
    }
    public void Attack()
    {
        current = attack;
        attack.Reset();
    }

    public void Hurt()
    {
        current = hurt;
        hurt.Reset();
        hp -= 10;
    }
    
    public bool IsAlive() => hp > 0;

    public void Update(double deltaTime)
    {
        current.Update(deltaTime);
        if(current.IsEnded()) current = idle;
    }
    public void Draw(SpriteBatch spriteBatch, Texture2D texture)
    {
        spriteBatch.Draw(texture, position, new Rectangle(current.CurrentFrame * 64, 0, 64, 64),
                     Color.White, 0f, Vector2.Zero, 4f, spriteEffect, 0f);
    }
}