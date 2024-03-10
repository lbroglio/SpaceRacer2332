using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace SpaceRace2332.Components
{
    internal class PlayerShip : DrawableGameComponent
    {
        // Statics
        /// <summary>
        /// The 3D model to render for this visual in the word 
        /// </summary>
        private static  Model _model = null;
        /// <summary>
        /// The apsect ratio of the screen
        /// </summary>
        private static float _aspectRatio;

        //Instance

        /// <summary>
        /// The position of the ship's model in the  world
        /// </summary>
        private Vector3 _position;

        private Vector3 _velocity;

        /// <summary>
        /// Holds the change in  the rotation of the ship
        /// </summary>
        private Vector3 _rotationVelocity;

        /// <summary>
        /// The rotation  of this ship's model
        /// </summary>
        private Vector3 _rotation;

        //Properties

        /// <summary>
        /// The position of the ship's model in the  world
        /// </summary>
        public Vector3 Position
        {
            get { return _position; }
            set { _position = value; }
        }

        public Vector3 Velocity
        {
            get { return _velocity; }
            set { _velocity = value; }
        }

        /// <summary>
        /// Holds the change in  the rotation of the ship
        /// </summary>
        public Vector3 RotationVelocity
        {
            get { return _rotationVelocity; }
            set { _rotationVelocity = value; }
        }

        /// <summary>
        /// The rotation  of this ship's model
        /// </summary>
        public Vector3 Rotation
        {
            get { return _rotation; }
            set { _rotation = value; }
        }

        public PlayerShip(Game game) : base(game)
        {
        }

        protected override void LoadContent()
        {
            // If the model hasn't been loaded load it
            if(_model == null)
            {
                _model = Game.Content.Load<Model>("Models\\TriangularShip");

            }
            _aspectRatio = Game.GraphicsDevice.Viewport.AspectRatio;

            base.LoadContent();

        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState state = Keyboard.GetState();

            


            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {

            foreach (ModelMesh mesh in _model.Meshes)
            {
                // This is where the mesh orientation is set, as well 
                // as our camera and projection.
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();

                    effect.World = Matrix.CreateRotationX(_rotation.X) 
                        * Matrix.CreateRotationY(_rotation.Y) 
                        * Matrix.CreateRotationZ(_rotation.Z) 
                        * Matrix.CreateTranslation(_position);

                    Vector3 cameraPosition = new Vector3(_position.X - 10, _position.Y, _position.Z);
                    effect.View = Matrix.CreateLookAt(cameraPosition, Vector3.Zero, Vector3.Up);
                    effect.Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45.0f), _aspectRatio, 1.0f, 10000.0f);
                }
                // Draw the mesh, using the effects set above.
                mesh.Draw();
            }

            base.Draw(gameTime);
        }
    }
}
