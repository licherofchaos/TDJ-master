using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using Genbox.VelcroPhysics.Dynamics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TDJ;


namespace IPCA.MonoGame
{
    public class Scene
    {
        private List<Sprite> _sprites;
        private List<NPC> _npcs;
        private List<NPC2> _npcs2;
        private List<Coin> _coins;
        private List<win> _win;

        public Scene(Game1 game, string name)
        {
            //le ficheiro do level
            string filename = $"Content/scenes/{name}.dt";
            _sprites = new List<Sprite>();
            using (StreamReader reader = File.OpenText(filename))
            {
                JObject sceneJson = (JObject) JToken.ReadFrom(new JsonTextReader(reader));
                JArray spriteJson = (JArray) sceneJson["composite"]["sImages"];
                foreach (JObject image in spriteJson)
                {
                    float x = (float) (image["x"] ?? 0);
                    float y = (float) (image["y"] ?? 0);
                    string imageName = (string) image["imageName"];
                    string imageFilename = $"assets/orig/images/{imageName}";

                    // Load texture here, and send it to the sprite object
                     Texture2D texture = game.Content.Load<Texture2D>(imageFilename);
                    Sprite sprite = new Sprite(imageFilename, texture, new Vector2(x, y), true);
                    
                    _sprites.Add(sprite);
                    sprite.AddRectangleBody(game.Services.GetService<World>(), 
                        isKinematic: true);
                }
 
            }
            
            //construcao de NPCS
            _npcs = new List<NPC>();
            _npcs.Add(new NPC(game, new Vector2(4.5f, 2f)));
            _npcs.Add(new NPC(game, new Vector2(7.5f, 4f)));
            _npcs.Add(new NPC(game, new Vector2(14f, -10f)));
            _npcs.Add(new NPC(game, new Vector2(14f, -16f)));

            _npcs2 = new List<NPC2>();
            _npcs2.Add(new NPC2(game, new Vector2(2.5f, 2f)));
            _npcs2.Add(new NPC2(game, new Vector2(22f, -10f)));
            _npcs2.Add(new NPC2(game, new Vector2(22f, -16f)));
            _npcs2.Add(new NPC2(game, new Vector2(32f, -8.5f)));

            _coins = new List<Coin>();
            _coins.Add(new Coin(game, new Vector2(2.5f, 2f)));
            _coins.Add(new Coin(game, new Vector2(3.5f, 2f)));
            _coins.Add(new Coin(game, new Vector2(14f, -12.5f)));
            _coins.Add(new Coin(game, new Vector2(22f, -12.5f)));
            _coins.Add(new Coin(game, new Vector2(14f, -20.5f)));
            _coins.Add(new Coin(game, new Vector2(22f, -20.5f)));


            _win = new List<win>();
            _win.Add(new win(game, new Vector2(22f, -20.5f)));
        }


        public void Update(GameTime gameTime)
        {
            for (int i = 0; i < _npcs.Count; i++)
            {
                _npcs[i].Update(gameTime);
                if (_npcs[i].IsDead == true) _npcs.Remove(_npcs[i]);
            }

            for (int i = 0; i < _npcs2.Count; i++)
            {
                _npcs2[i].Update(gameTime);
                if (_npcs2[i].IsDead == true) _npcs2.Remove(_npcs2[i]);
            }

            for (int i = 0; i < _coins.Count; i++)
            {
                _coins[i].Update(gameTime);
                if (_coins[i].IsDead == true) _coins.Remove(_coins[i]);
            }



        }
        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            foreach (Sprite sprite in _sprites)
            {
                sprite.Draw(spriteBatch, gameTime);
            }
            foreach (NPC npc in _npcs) npc.Draw(spriteBatch, gameTime);

            foreach (NPC2 npc2 in _npcs2) npc2.Draw(spriteBatch, gameTime);

            foreach (Coin coin in _coins) coin.Draw(spriteBatch, gameTime);
        }

       
    }
}