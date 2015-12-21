using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using SkinnedModel;

namespace WalkingDino
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Model Dino;
        // AnimationPlayer DinoPlayer;
        ClipPlayer DinoClipPlayer;
        SkinningData DinoSkinningData;
        AnimationClip DinoClip;

        Controller Control;
        List<Action> ActionList;

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
            // List of Keyboard Actions
            ActionList = new List<Action>();
            ActionList.Add(new Action("Up", Keys.Up));
            ActionList.Add(new Action("Down", Keys.Down));
            ActionList.Add(new Action("Right", Keys.Right));
            ActionList.Add(new Action("Left", Keys.Left));
            ActionList.Add(new Action("Jump", Keys.Space));
            ActionList.Add(new Action("Camera", Keys.C));
            ActionList.Add(new Action("Attack", Keys.A));
            Control = new Controller(PlayerIndex.One, ActionList);



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

            // Load the model.
            Dino = Content.Load<Model>("Dino");

            // Look up our custom skinning information.
            DinoSkinningData = Dino.Tag as SkinningData;


            // Create an animation player, and start decoding an animation clip.
            DinoClipPlayer = new ClipPlayer(DinoSkinningData, 24);
            DinoClip = DinoSkinningData.AnimationClips["Take 001"];
            DinoClipPlayer.play(DinoClip, 2, 11, true);

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
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
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            if (Control.IsActionPressed("Up"))
            {
                DinoClipPlayer.play(DinoClip, 12, 31, true);
            }
               
            if (Control.IsActionPressed("Left"))
            {

            }
            Control.Update();
            DinoClipPlayer.Update(gameTime.ElapsedGameTime, true, Matrix.Identity);
            

            
            

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            Matrix[] bones = DinoClipPlayer.GetSkinTransforms();

            // Compute camera matrices.
            Matrix view = Matrix.CreateLookAt(new Vector3(20, 20, 50),
                                              new Vector3(0, 0, 0), Vector3.Up);

            Matrix projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4,
                                                                    GraphicsDevice.Viewport.AspectRatio,
                                                                    1,
                                                                    10000);

            // Render the skinned mesh.
            foreach (ModelMesh mesh in Dino.Meshes)
            {
                foreach (SkinnedEffect effect in mesh.Effects)
                {
                    effect.SetBoneTransforms(bones);

                    effect.View = view;
                    effect.Projection = projection;

                    effect.EnableDefaultLighting();

                    effect.SpecularColor = new Vector3(0.25f);
                    effect.SpecularPower = 16;
                }

                mesh.Draw();
            }

            base.Draw(gameTime);
        }
    }
}
