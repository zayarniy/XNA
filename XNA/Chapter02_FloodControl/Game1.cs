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
        enum GameStates { TitleScreen, Playing, GameOver };
        GameStates gameState = GameStates.TitleScreen;
        Rectangle EmptyPiece = new Rectangle(1, 247, 40, 40);
        const float MinTimeSinceLastInput = 0.25f;
        float timeSinceLastInput = 0.0f;
        SpriteFont pericles36Font;
        Vector2 scorePosition = new Vector2(605, 215);
        Vector2 gameOverLocation = new Vector2(200, 260);
        float gameOverTimer;


        //Flood
        const float MaxFloodCounter = 100.0f;
        float floodCount = 0.0f;
        float timeSinceLastFloodIncrease = 0.0f;
        float timeBetweenFloodIncreases = 1.0f;
        float floodIncreaseAmount = 0.5f;

        const int MaxWaterHeight = 244;
        const int WaterWidth = 297;
        Vector2 waterOverlayStart = new Vector2(85, 245);
        Vector2 waterPosition = new Vector2(478, 338);

        //Score
        Queue<ScoreZoom> ScoreZooms = new Queue<ScoreZoom>();


        //Levels
        int currentLevel = 0;
        int linesCompletedThisLevel = 0;
        const float floodAccelerationPerLevel = 0.5f;
        Vector2 levelTextPosition = new Vector2(512, 215);

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
            pericles36Font = Content.Load<SpriteFont>(@"Fonts\Pericles36");
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
                        currentLevel = 0;
                        floodIncreaseAmount = 0.0f;
                        StartNewLevel();
                    }
                    break;
                case GameStates.Playing:
                    timeSinceLastInput += (float)gameTime.ElapsedGameTime.TotalSeconds;
                    timeSinceLastFloodIncrease +=
                    (float)gameTime.ElapsedGameTime.TotalSeconds;
                    if (timeSinceLastFloodIncrease >= timeBetweenFloodIncreases)
                    {
                        floodCount += floodIncreaseAmount;
                        timeSinceLastFloodIncrease = 0.0f;
                        if (floodCount >= MaxFloodCounter)
                        {
                            gameOverTimer = 8.0f;
                            gameState = GameStates.GameOver;
                        }
                    }
                    if (gameBoard.ArePiecesAnimating())
                    {
                        gameBoard.UpdateAnimatedPieces();
                    }
                    else
                    {
                        gameBoard.ResetWater();
                        for (int y = 0; y < GameBoard.GameBoardHeight; y++)
                        {
                            CheckScoringChain(gameBoard.GetWaterChain(y));
                        }
                        gameBoard.GenerateNewPieces(true);
                        if (timeSinceLastInput >= MinTimeSinceLastInput)
                        {
                            HandleMouseInput(Mouse.GetState());
                        }
                    }
                    UpdateScoreZooms();
                    break;
                case GameStates.GameOver:
                    gameOverTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                    if (gameOverTimer <= 0)
                    {
                        gameState = GameStates.TitleScreen;
                    }
                    break;
            }
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        private void DrawEmptyPiece(int pixelX, int pixelY)
        {
            spriteBatch.Draw(
            playingPieces,
            new Rectangle(pixelX, pixelY,
            GamePiece.PieceWidth, GamePiece.PieceHeight), EmptyPiece,Color.White);
        }
        private void DrawStandardPiece(int x, int y,
        int pixelX, int pixelY)
        {
            spriteBatch.Draw(
            playingPieces, new Rectangle(pixelX, pixelY,
            GamePiece.PieceWidth, GamePiece.PieceHeight),
            gameBoard.GetSourceRect(x, y),
            Color.White);
        }
        private void DrawFallingPiece(int pixelX, int pixelY,
        string positionName)
        {
            spriteBatch.Draw(
            playingPieces,
            new Rectangle(pixelX, pixelY -
            gameBoard.fallingPieces[positionName].VerticalOffset,
            GamePiece.PieceWidth, GamePiece.PieceHeight),
            gameBoard.fallingPieces[positionName].GetSourceRect(),
            Color.White);
        }
        private void DrawFadingPiece(int pixelX, int pixelY,
        string positionName)
        {
            spriteBatch.Draw(
            playingPieces,
            new Rectangle(pixelX, pixelY,
            GamePiece.PieceWidth, GamePiece.PieceHeight),
            gameBoard.fadingPieces[positionName].GetSourceRect(),
            Color.White *
            gameBoard.fadingPieces[positionName].alphaLevel);
        }
        private void DrawRotatingPiece(int pixelX, int pixelY,
        string positionName)
        {
            spriteBatch.Draw(
            playingPieces,
            new Rectangle(pixelX + (GamePiece.PieceWidth / 2),
            pixelY + (GamePiece.PieceHeight / 2),
            GamePiece.PieceWidth,
            GamePiece.PieceHeight),
            gameBoard.rotatingPieces[positionName].GetSourceRect(),
            Color.White,
            gameBoard.rotatingPieces[positionName].RotationAmount, new Vector2(GamePiece.PieceWidth / 2,
GamePiece.PieceHeight / 2),
SpriteEffects.None, 0.0f);
        }


        private void UpdateScoreZooms()
        {
            int dequeueCounter = 0;
            foreach (ScoreZoom zoom in ScoreZooms)
            {
                zoom.Update();
                if (zoom.IsCompleted)
                    dequeueCounter++;
            }
            for (int d = 0; d < dequeueCounter; d++)
                ScoreZooms.Dequeue();
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
                        linesCompletedThisLevel++;
                        floodCount = MathHelper.Clamp(floodCount -
                        (DetermineScore(WaterChain.Count) / 10), 0.0f, 100.0f);
                        ScoreZooms.Enqueue(new ScoreZoom("+" +DetermineScore(WaterChain.Count).ToString(),new Color(1.0f, 0.0f, 0.0f,0.4f)));
                        foreach (Vector2 ScoringSquare in WaterChain)
                        {
                            gameBoard.AddFadingPiece((int)ScoringSquare.X,(int)ScoringSquare.Y,gameBoard.GetSquare((int)ScoringSquare.X,(int)ScoringSquare.Y));
                            gameBoard.SetSquare((int)ScoringSquare.X,(int)ScoringSquare.Y, "Empty");
                        }
                        if (linesCompletedThisLevel >= 10)
                        {
                            StartNewLevel();
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
                    gameBoard.AddRotatingPiece(x, y,gameBoard.GetSquare(x, y), false);
                    gameBoard.RotatePiece(x, y, false);
                    timeSinceLastInput = 0.0f;
                    Debug.WriteLine("x={0} y={1}", x, y);
                }
                if (mouseState.RightButton == ButtonState.Pressed)
                {
                    gameBoard.AddRotatingPiece(x, y,gameBoard.GetSquare(x, y), true);
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
            if (gameState == GameStates.Playing || gameState==GameStates.GameOver)
            {
                spriteBatch.Begin();
                spriteBatch.Draw(backgroundScreen,
                new Rectangle(0, 0,
                this.Window.ClientBounds.Width,
                this.Window.ClientBounds.Height),Color.White);

                for (int x = 0; x < GameBoard.GameBoardWidth; x++)
                    for (int y = 0; y < GameBoard.GameBoardHeight; y++)
                    {
                        int pixelX = (int)gameBoardDisplayOrigin.X +
                        (x * GamePiece.PieceWidth);
                        int pixelY = (int)gameBoardDisplayOrigin.Y +
                        (y * GamePiece.PieceHeight);
                        DrawEmptyPiece(pixelX, pixelY);
                        bool pieceDrawn = false;
                        string positionName = x.ToString() + "_" + y.ToString();
                        if (gameBoard.rotatingPieces.ContainsKey(positionName))
                        {
                            DrawRotatingPiece(pixelX, pixelY, positionName);
                            pieceDrawn = true;
                        }
                        if (gameBoard.fadingPieces.ContainsKey(positionName))
                        {
                            DrawFadingPiece(pixelX, pixelY, positionName);
                            pieceDrawn = true;
                        }
                        if (gameBoard.fallingPieces.ContainsKey(positionName))
                        {
                            DrawFallingPiece(pixelX, pixelY, positionName);
                            pieceDrawn = true;
                        }
                        if (!pieceDrawn)
                        {
                            DrawStandardPiece(x, y, pixelX, pixelY);
                        }
                    }
                //this.Window.Title = playerScore.ToString();
                spriteBatch.DrawString(pericles36Font,playerScore.ToString(),scorePosition,Color.Black);
                spriteBatch.DrawString(pericles36Font,
                currentLevel.ToString(),
                levelTextPosition,
                Color.Black);
                foreach (ScoreZoom zoom in ScoreZooms)
                {
                    spriteBatch.DrawString(pericles36Font, zoom.Text,
                    new Vector2(this.Window.ClientBounds.Width / 2,
                    this.Window.ClientBounds.Height / 2),
                    zoom.DrawColor, 0.0f,
                    new Vector2(pericles36Font.MeasureString(zoom.Text).X / 2,
                    pericles36Font.MeasureString(zoom.Text).Y / 2),
                    zoom.Scale, SpriteEffects.None, 0.0f);
                }

                int waterHeight = (int)(MaxWaterHeight * (floodCount / 100));
                spriteBatch.Draw(backgroundScreen,
                new Rectangle(
                (int)waterPosition.X,
                (int)waterPosition.Y + (MaxWaterHeight - waterHeight),
                WaterWidth,
                waterHeight),
                new Rectangle(
                (int)waterOverlayStart.X,
                (int)waterOverlayStart.Y + (MaxWaterHeight - waterHeight),
                WaterWidth,
                waterHeight),
                new Color(255, 255, 255, 180));


                spriteBatch.End();
                if (gameState == GameStates.GameOver)
                {
                    spriteBatch.Begin();
                    spriteBatch.DrawString(pericles36Font,"G A M E O V E R!",gameOverLocation,Color.Yellow);
                    spriteBatch.End();
                }
                base.Draw(gameTime);
            }
        }

        private void StartNewLevel()
        {
            currentLevel++;
            floodCount = 0.0f;
            linesCompletedThisLevel = 0;
            floodIncreaseAmount += floodAccelerationPerLevel;
            gameBoard.ClearBoard();
            gameBoard.GenerateNewPieces(false);
        }
    }
}
