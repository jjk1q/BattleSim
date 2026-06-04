using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace BattleSimDX;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private SpriteFont _font;
    private Texture2D _dino;
    private Fighter _player1;
    private Fighter _player2;
    private bool _gameOver;
    private string winnerText; 
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

        _player1 = new Fighter(new Vector2(100, 100), idle1, attack1, hurt1, SpriteEffects.None, 50);
        _player2 = new Fighter(new Vector2(400, 100), idle2, attack2, hurt2, SpriteEffects.FlipHorizontally, 50);
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        
        _dino = Content.Load<Texture2D>("dino");

        _font = Content.Load<SpriteFont>("Font");
        

        
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();
        _player1.Update(gameTime.ElapsedGameTime.TotalSeconds);
        _player2.Update(gameTime.ElapsedGameTime.TotalSeconds);
        KeyboardState keyboard = Keyboard.GetState();
        if (!_gameOver)
        {
            Attack(keyboard, _player1, _player2, Keys.Space);
            Attack(keyboard, _player2, _player1, Keys.Enter);
            if(!_player1.IsAlive())
            {
                _gameOver = true;
                winnerText = "player2 won";
            }
            if (!_player2.IsAlive())
            {
                _gameOver = true;
                winnerText = "player1 won";
            }
            _prevKeyboard = keyboard;
            base.Update(gameTime);
        }
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
        _player1.Draw(_spriteBatch, _dino);
        _player2.Draw(_spriteBatch, _dino);
        if (_gameOver)
        {
            _spriteBatch.DrawString(_font, winnerText, new Vector2(50, 50), Color.White);
        }
        _spriteBatch.End();

        base.Draw(gameTime);
    }
    public void Attack(KeyboardState keyboard, Fighter attacker, Fighter target, Keys key)
    {
        if (keyboard.IsKeyDown(key) && _prevKeyboard.IsKeyUp(key))
        {
            attacker.Attack();
            target.Hurt();
        }

    }
}


