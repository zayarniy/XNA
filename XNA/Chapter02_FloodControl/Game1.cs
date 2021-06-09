using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace FloodControl
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D playingPieces, backgroundScreen, titleScreen;

        GameBoard gameBoard;
        Vector2 gameBoardDisplayOrigin = new Vector2(70, 89);
        int playerScore = 0;
        enum GameStates { TitleScreen, Playing };
        GameStates gameState = GameStates.TitleScreen;
        Rectangle EmptyPiece = new Rectangle(1, 247, 40, 40);
        const float MinTimeSinceLastInput = 0.25f;
        float timeSinceLastInput = 0.0f;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            this.IsMouseVisible = true;
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;
            graphics.ApplyChanges();
            gameBoard = new GameBoard();
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            playingPieces = Content.Load<Texture2D>(@"Textures\Tile_Sheet");
            backgroundScreen = Content.Load<Texture2D>(@"Textures\Background");
            titleScreen = Content.Load<Texture2D>(@"Textures\TitleScreen");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            switch (gameState)
            {
                case GameStates.TitleScreen:
                    if (Keyboard.GetState().IsKeyDown(Keys.Space))
                    {
                        gameBoard.ClearBoard();
                        gameBoard.GenerateNewPieces(false);
                        playerScore = 0;
                        gameState = GameStates.Playing;
                    }
                    break;
                case GameStates.Playing:
                    timeSinceLastInput +=
                    (float)gameTime.ElapsedGameTime.TotalSeconds;
                    if (timeSinceLastInput >= MinTimeSinceLastInput)
                    {
                        HandleMouseInput(Mouse.GetState());
                    }
                    gameBoard.ResetWater();
                    for (int y = 0; y < GameBoard.GameBoardHeight; y++)
                    {
                        CheckScoringChain(gameBoard.GetWaterChain(y));
                    }
                    gameBoard.GenerateNewPieces(true);
                    break;
            }
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        private void CheckScoringChain(List<Vector2> WaterChain)
        {
            if (WaterChain.Count > 0)
            {
                Vector2 LastPipe = WaterChain[WaterChain.Count - 1];
                if (LastPipe.X == GameBoard.GameBoardWidth - 1)
                {
                    if (gameBoard.HasConnector(
                    (int)LastPipe.X, (int)LastPipe.Y, "Right"))
                    {
                        playerScore += DetermineScore(WaterChain.Count);
                        foreach (Vector2 ScoringSquare in WaterChain)
                        {
                            gameBoard.SetSquare((int)ScoringSquare.X,
                            (int)ScoringSquare.Y, "Empty");
                        }
                    }
                }
            }
        }

        private int DetermineScore(int SquareCount)
        {
            return (int)((Math.Pow((SquareCount / 5), 2) + SquareCount) * 10);
        }

        private void HandleMouseInput(MouseState mouseState)
        {
            int x = ((mouseState.X - (int)gameBoardDisplayOrigin.X) / GamePiece.PieceWidth);
            int y = ((mouseState.Y - (int)gameBoardDisplayOrigin.Y) / GamePiece.PieceHeight);
            if ((x >= 0) && (x < GameBoard.GameBoardWidth) &&
            (y >= 0) && (y < GameBoard.GameBoardHeight))
            {
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    gameBoard.RotatePiece(x, y, false);
                    timeSinceLastInput = 0.0f;
                    Debug.WriteLine("x={0} y={1}", x, y);
                }
                if (mouseState.RightButton == ButtonState.Pressed)
                {
                    gameBoard.RotatePiece(x, y, true);
                    timeSinceLastInput = 0.0f;
                }
            }
        }
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            if (gameState == GameStates.TitleScreen)
            {
                spriteBatch.Begin();
                spriteBatch.Draw(titleScreen,
                new Rectangle(0, 0,
                this.Window.ClientBounds.Width,
                this.Window.ClientBounds.Height),
                Color.White);
                spriteBatch.End();
            }
            if (gameState == GameStates.Playing)
            {
                spriteBatch.Begin();
                spriteBatch.Draw(backgroundScreen,
                new Rectangle(0, 0,
                this.Window.ClientBounds.Width,
                this.Window.ClientBounds.Height),
        Color.White);
                for (int x = 0; x < GameBoard.GameBoardWidth; x++)
                    for (int y = 0; y < GameBoard.GameBoardHeight; y++)
                    {
                        int pixelX = (int)gameBoardDisplayOrigin.X +
                        (x * GamePiece.PieceWidth);
                        int pixelY = (int)gameBoardDisplayOrigin.Y +
                        (y * GamePiece.PieceHeight);
                        spriteBatch.Draw(
                        playingPieces,
                new Rectangle(
                pixelX,
                pixelY,
                GamePiece.PieceWidth,
                GamePiece.PieceHeight),
                EmptyPiece,
                Color.White);
                        spriteBatch.Draw(
                        playingPieces, new Rectangle(
                        pixelX,
                        pixelY,
                        GamePiece.PieceWidth,
                        GamePiece.PieceHeight),
                        gameBoard.GetSourceRect(x, y),
                        Color.White);
                    }
                this.Window.Title = playerScore.ToString();
                spriteBatch.End();
                base.Draw(gameTime);
            }
        }
    }
}
