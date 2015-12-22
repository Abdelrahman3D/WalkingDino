using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace WalkingDino
{
    class IconImage
    {

        public Texture2D KeyOn;
        public Texture2D KeyOff;
        public Texture2D Current;
        public Vector2 KeyPosition;
        
        public  IconImage(Vector2 position)
        {
            this.KeyPosition = position;
        }
    }
}
