using System;
using System.Collections.Generic;
using System.Linq;
using Genbox.VelcroPhysics.Collision.RayCast;
using Genbox.VelcroPhysics.Dynamics;
using Genbox.VelcroPhysics.Factories;
using IPCA.MonoGame;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TDJ
{
    class Coin : AnimatedSprite
    {
        enum Status
        {
            idle
        }

        private Game1 _game;
        private bool isDead;
        public bool IsDead => isDead;
        private List<Texture2D> _anim;

        private HashSet<Fixture> _collisions;
        public Coin(Game1 game, Vector2 position) :
            base("coin",
               position,
                Enumerable.Range(1, 4)
                    .Select(
                        n => game.Content.Load<Texture2D>(
                            $"tileset{n}")
                        )
                    .ToArray())
        {
            _collisions = new HashSet<Fixture>();
            _anim = _textures; // loaded by the base construtor
            _direction = Direction.Left;
            _game = game;

            AddRectangleBody(
                _game.Services.GetService<World>(),
                width: _size.X /2f);
          
           
            Body.IsSensor = true;
            
            Body.BodyType = BodyType.Static;
            Body.OnCollision = (a, b, contact) =>
            {
                _collisions.Add(b);  
                if (b.GameObject().Name == "player")
                {
                    game.coins++;
                    isDead = true;
                    Body.Enabled = false;
                    _currentTexture = 0;
                }
                
            };

        }
    }
}
