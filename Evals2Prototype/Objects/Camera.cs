﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Evals2Prototype.Objects
{ 
    class Camera : GameComponent
    {
        static Vector2 _camPos = Vector2.Zero;
        Vector2 _worldBound;
        public Scene scene;
        static public Matrix CurrentCameraTranslation
        {
            get
            {
                return Matrix.CreateTranslation(new Vector3(
                    -CamPos,
                    0));
            }
        }

        static public Vector2 CamPos
        {
            get
            {
                return _camPos;
            }

            set
            {
                _camPos = value;
            }
        }

        public Camera(Game game, Vector2 startPos, Vector2 bound,Scene s) : base(game)
        {
            game.Components.Add(this);
            CamPos = startPos;
            _worldBound = bound;
            scene = s;
        }

        public override void Update(GameTime gameTime)
        {
            CameraGuide guide;
            Player p;

            guide = (CameraGuide)scene.Components
                .FirstOrDefault(pl => pl.GetType() == typeof(CameraGuide));
            if (guide != null && guide.Visible)
                follow(guide.Position, Game.GraphicsDevice.Viewport);
            else
            {
                
                p = (Player)scene.Components
                    .FirstOrDefault(pl => pl.GetType() == typeof(Player));
                if (p != null)
                    follow(p.Position, Game.GraphicsDevice.Viewport);
            }
            base.Update(gameTime);
        }

        public void move(Vector2 delta, Viewport v)
        {
            CamPos += delta;
            CamPos = Vector2.Clamp(CamPos, Vector2.Zero, _worldBound - new Vector2(v.Width, v.Height));
        }

        public void follow(Vector2 followPos, Viewport v)
        {
            _camPos = followPos - new Vector2(v.Width / 2, v.Height / 2);
            _camPos = Vector2.Clamp(_camPos, Vector2.Zero, _worldBound - new Vector2(v.Width, v.Height));
        }

    }
}
