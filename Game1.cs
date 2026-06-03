using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace BattleSimDX;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Texture2D _dino;
    private Fighter _player1;
    private Fighter _player2;
    private KeyboardState _prevKeyboard;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        Animation idle1 = new Animation(0, 2, 0.5, true);
        Animation attack1 = new Animation(2, 3, 0.09, false);
        Animation hurt1 = new Animation(5, 1, 0.2, false);

        Animation idle2 = new Animation(0, 2, 0.5, true);
        Animation attack2 = new Animation(2, 3, 0.09, false);
        Animation hurt2 = new Animation(5, 1, 0.2, false);

        _player1 = new Fighter(new Vector2(100, 100), idle1, attack1, hurt1, SpriteEffects.None);
        _player2 = new Fighter(new Vector2(400, 100), idle2, attack2, hurt2, SpriteEffects.FlipHorizontally);
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        
        _dino = Content.Load<Texture2D>("dino");
        

        
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();
        _player1.Update(gameTime.ElapsedGameTime.TotalSeconds);
        _player2.Update(gameTime.ElapsedGameTime.TotalSeconds);
        KeyboardState keyboard = Keyboard.GetState();
        Attack(keyboard, _player1,Keys.Space);
        Attack(keyboard, _player2,Keys.Enter);
        _prevKeyboard = keyboard;
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
        _player1.Draw(_spriteBatch, _dino);
        _player2.Draw(_spriteBatch, _dino);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
    public void Attack(KeyboardState keyboard, Fighter player, Keys key)
    {
        if (keyboard.IsKeyDown(key) && _prevKeyboard.IsKeyUp(key))
            player.Attack();

    }
}


