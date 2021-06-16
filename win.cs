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
    class win : AnimatedSprite
    {
        private Game1 _game;

        private List<Texture2D> _idleFrames;
        private List<Texture2D> _walkFrames;
        private Vector2 _startingPoint;

        private int Hp = 5;
        private bool isDead;
        public bool IsDead => isDead;
        private int _ccount = 0;
        private int dmg = 1;
        private HashSet<Fixture> _collisions;

        public win(Game1 game, Vector2 position) :
            base("win",
                position,
                Enumerable.Range(1, 12)
                    .Select(
                        n => game.Content.Load<Texture2D>(
                            $"win")
                        )
                    .ToArray())

        {
            _collisions = new HashSet<Fixture>();
            _idleFrames = _textures;
            _direction = Direction.Left;

            _game = game;

            AddRectangleBody(
                _game.Services.GetService<World>(),
                width: _size.X / 2f
            );



            Fixture sensor = FixtureFactory.AttachRectangle(
                _size.X / 3f, _size.Y * 0.05f,
                4, new Vector2(0, -_size.Y / 2f),
                Body);
            sensor.IsSensor = true;

            Body.Friction = 0f;

            sensor.OnCollision = (a, b, contact) =>
            {
                _collisions.Add(b);

                if (b.GameObject().Name == "player")
                {
                    game.Player.Die();
                }
            };
        }

    }

}
