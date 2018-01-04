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
        public static List<Character> Characters { get; set; }
        public static List<AttributeName> Attributes { get; set; }
        public static List<string> FavoriteCharacters { get; set; }
        public static List<string> FavoriteAttributes { get; set; }

        public static void InitializeRuntime()
        {
            Characters = JsonConvert.DeserializeObject<List<Character>>(App.storage.Read("data/characters.json"));
            Attributes = JsonConvert.DeserializeObject<List<AttributeName>>(App.storage.Read("data/attributeNames.json"));

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

            foreach (Character c in Characters)
            {
                c.favorite = FavoriteCharacters.Contains(c.Name);
            }

            foreach(AttributeName a in Attributes)
            {
                a.favorite = FavoriteAttributes.Contains(a.name);
            }

            Characters.Sort();
            Attributes.Sort();
        }

        public static List<Move> GetMoves(int characterId, MoveType moveType = MoveType.Any)
        {
            List<Move> list = new List<Move>();

            List<Move> moves = JsonConvert.DeserializeObject<List<MoveData>>(App.storage.Read($"{characterId}/moves.json")).Select(d => Move.Convert(d)).ToList();

            if (moveType == MoveType.Any)
                list = moves;
            else
                list = moves.Where(m => m.moveType == moveType).ToList();

            return list;
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

            Characters.Sort();
        }

        public static void UpdateFavorite(AttributeName attribute, bool fav)
        {
            if (fav)
                FavoriteAttributes.Add(attribute.name);
            else
                if (FavoriteAttributes.Contains(attribute.name))
                FavoriteAttributes.Remove(attribute.name);

            App.storage.Write("fav_attributes.bin", JsonConvert.SerializeObject(FavoriteAttributes));

            Attributes.Sort();
        }
    }
}
