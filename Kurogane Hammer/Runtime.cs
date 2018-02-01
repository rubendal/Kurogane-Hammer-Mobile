using Kurogane_Hammer.Classes;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kurogane_Hammer
{
    public static class Runtime
    {
        public static List<string> FavoriteCharacters { get; set; }
        public static List<string> FavoriteAttributes { get; set; }

        public static void InitializeRuntime()
        {

            if (!App.storage.FileExists("fav_characters.bin"))
            {
                App.storage.Write("fav_characters.bin", JsonConvert.SerializeObject(new List<string>()));
                FavoriteCharacters = new List<string>();
            }
            else
                FavoriteCharacters = JsonConvert.DeserializeObject<List<string>>(App.storage.Read("fav_characters.bin"));

            if (!App.storage.FileExists("fav_attributes.bin"))
            {
                App.storage.Write("fav_attributes.bin", JsonConvert.SerializeObject(new List<string>()));
                FavoriteAttributes = new List<string>();
            }
            else
                FavoriteAttributes = JsonConvert.DeserializeObject<List<string>>(App.storage.Read("fav_attributes.bin"));
        }

        public static List<Character> GetCharacters()
        {
            List<Character> list = JsonConvert.DeserializeObject<List<Character>>(App.storage.Read("data/characters.json"));

            foreach (Character c in list)
            {
                c.CheckSpecificAttributes();
                c.favorite = FavoriteCharacters.Contains(c.Name);
            }

            list.Sort();
            return list;
        }

        public static List<AttributeName> GetAttributes()
        {
            List<AttributeName> list = JsonConvert.DeserializeObject<List<AttributeName>>(App.storage.Read("data/attributeNames.json"));

            foreach (AttributeName a in list)
            {
                a.favorite = FavoriteAttributes.Contains(a.name);
            }

            list.Sort();

            return list;
        }

        public static Dictionary<MoveType, List<Move>> ConvertMoveJson(string json)
        {
            Dictionary<MoveType, List<Move>> dict = new Dictionary<MoveType, List<Move>>();

            dict.Add(MoveType.Ground, new List<Move>());
            dict.Add(MoveType.Throw, new List<Move>());
            dict.Add(MoveType.Evasion, new List<Move>());
            dict.Add(MoveType.Aerial, new List<Move>());
            dict.Add(MoveType.Special, new List<Move>());

            List<Move> moves = JsonConvert.DeserializeObject<List<MoveData>>(json).Select(d => Move.Convert(d)).ToList();

            foreach(Move move in moves)
            {
                dict[move.moveType].Add(move);
            }

            return dict;
        }

        public static Dictionary<MoveType, List<Move>> GetMoves(int characterId)
        {
            return JsonConvert.DeserializeObject<Dictionary<MoveType, List<Move>>>(App.storage.Read($"{characterId}/moves.json"), new JsonSerializerSettings {
                TypeNameHandling = TypeNameHandling.Objects,
                TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple
            });
        }

        public static List<Movement> GetMovements(int characterId)
        {
            return JsonConvert.DeserializeObject<List<MovementData>>(App.storage.Read($"{characterId}/movements.json")).Select(d => Movement.Convert(d)).ToList();
        }

        public static List<Attribute> GetAttributes(int characterId)
        {
            return JsonConvert.DeserializeObject<List<AttributeData>>(App.storage.Read($"{characterId}/attributes.json")).Select(d => Attribute.Convert(d)).ToList();
        }

        public static List<Attribute> GetAttributeToRank(string name)
        {
            return JsonConvert.DeserializeObject<List<AttributeData>>(App.storage.Read($"{name.ToLower()}/attributes.json")).Select(d => Attribute.Convert(d)).ToList();
        }

        public static void UpdateFavorite(Character character, bool fav)
        {
            if (fav)
                FavoriteCharacters.Add(character.DisplayName);
            else
                if (FavoriteCharacters.Contains(character.DisplayName))
                FavoriteCharacters.Remove(character.DisplayName);

            App.storage.Write("fav_characters.bin", JsonConvert.SerializeObject(FavoriteCharacters));
        }

        public static void UpdateFavorite(AttributeName attribute, bool fav)
        {
            if (fav)
                FavoriteAttributes.Add(attribute.name);
            else
                if (FavoriteAttributes.Contains(attribute.name))
                FavoriteAttributes.Remove(attribute.name);

            App.storage.Write("fav_attributes.bin", JsonConvert.SerializeObject(FavoriteAttributes));
        }
    }
}
