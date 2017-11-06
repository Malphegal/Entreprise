using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AIA.Core
{
    public class AnimatedObject : GameObject
    {
        // FIELDS
        public Rectangle Source;    // Rectangle permettant de définir la zone de l'image à afficher
        public float time;  // Durée depuis laquelle l'image est à l'écran
        public float frameTime = 0.5f;  // Durée de visibilité d'une image
        private int _totalFrames;
        private int _frameWidth;
        private int _frameHeight;
        protected int _ligne;
        protected int _colonne;

        // CONSTRUCTORS
        public AnimatedObject()
        {
            _totalFrames = 2;
            _frameWidth = 64;
            _frameHeight = 64;
            _ligne = 0;
            _colonne = 0;
        }

        public AnimatedObject(int totalAnimationFrames, int frameWidth, int frameHeight)
        {
            _totalFrames = totalAnimationFrames;
            _frameWidth = frameWidth;
            _frameHeight = frameHeight;
            _ligne = 0;
            _colonne = 0;
        }

        // METHODS
        public int totalFrames
        {
            get { return _totalFrames; }
        }

        public int frameWidth
        {
            get { return _frameWidth; }
        }

        public int frameHeight
        {
            get { return _frameHeight; }
        }

        public void DrawAnimation(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Source, Color.White);
        }

        public void UpdateFrame(GameTime gameTime)
        {
            time += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (time > frameTime)
            {
                this._colonne++;
                if (this._colonne>this._totalFrames-1)
                {
                    this._colonne = 0;
                }

                time = 0f;
            }


            Source = new Rectangle(
                _colonne * frameWidth,
                _ligne * frameHeight,
                frameWidth,
                frameHeight);
        }
    }
}
