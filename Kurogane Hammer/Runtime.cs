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

        public static void InitializeRuntime()
        {
            Characters = JsonConvert.DeserializeObject<List<Character>>(App.storage.Read("data/characters.json"));
            Attributes = JsonConvert.DeserializeObject<List<AttributeNameData>>(App.storage.Read("data/attributeNames.json")).Select(d => AttributeName.Convert(d)).ToList();
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
    }
}
