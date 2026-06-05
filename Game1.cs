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
    private Fighter _player;
    private Fighter enemy;
    public double mobTime;
    public double waitTime;
    private Battle battle;
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

        _player = new Fighter(new Vector2(100, 100), idle1, attack1, hurt1, SpriteEffects.None, 50);
        enemy = new Fighter(new Vector2(400, 100), idle2, attack2, hurt2, SpriteEffects.FlipHorizontally, 50);

        battle = new Battle(_player, enemy);

        mobTime = 0;
        waitTime = 0.5;


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
        _player.Update(gameTime.ElapsedGameTime.TotalSeconds);
        enemy.Update(gameTime.ElapsedGameTime.TotalSeconds);
        KeyboardState keyboard = Keyboard.GetState();
        if (!_gameOver)
        {
            Attack(keyboard, gameTime.ElapsedGameTime.TotalSeconds);
        }
        _prevKeyboard = keyboard;
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
        _player.Draw(_spriteBatch, _dino);
        enemy.Draw(_spriteBatch, _dino);
        if (_gameOver)
        {
            _spriteBatch.DrawString(_font, winnerText, new Vector2(50, 50), Color.White);
        }
        _spriteBatch.End();

        base.Draw(gameTime);
    }
    public void Attack(KeyboardState keyboard, double deltaTime)
    {
        if(!_player.IsBusy() && !enemy.IsBusy())
        {
            if(battle.Current() == _player)
            {
                if (keyboard.IsKeyDown(Keys.Space) && _prevKeyboard.IsKeyUp(Keys.Space))
                {
                    DoAttack();
                }
            }
            else
            {
                mobTime += deltaTime;
                if(mobTime >= waitTime)
                {
                    DoAttack();
                    mobTime = 0;
                }
                    
            }
        }
    }

    public void DoAttack()
    {
        battle.Attack();
        _gameOver = !battle.IsAlive();
        if (!_gameOver)
        {
            battle.NextRound();
        }
        else
        {   
            if(battle.Winner() == _player) winnerText = "Player 1 won";
            else winnerText = "Player 2 won";
        }
    }



}


