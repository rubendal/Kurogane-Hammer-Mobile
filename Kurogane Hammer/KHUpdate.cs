using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Kurogane_Hammer.Classes;
using Newtonsoft.Json;
using System.IO;

namespace Kurogane_Hammer
{
    public static class KHUpdate
    {
        public static async Task<bool> Start()
        {

            try
            {
                string characters = await APIConnection.Request("/api/Characters");
                string attributeNames = await APIConnection.Request("/api/characterattributes/types");

                if (string.IsNullOrWhiteSpace(characters) && string.IsNullOrWhiteSpace(attributeNames))
                    return false;

                App.storage.Write("data/characters.json", characters);
                App.storage.Write("data/attributeNames.json", attributeNames);

                List<Character> characterList = JsonConvert.DeserializeObject<List<Character>>(characters);
                List<AttributeName> attributeList = JsonConvert.DeserializeObject<List<AttributeName>>(attributeNames);

                foreach (Character c in characterList)
                {
                    string moves = await APIConnection.Request($"/api/Characters/{c.OwnerId}/moves");
                    string attributes = await APIConnection.Request($"/api/Characters/{c.OwnerId}/characterattributes");
                    string movements = await APIConnection.Request($"/api/Characters/{c.OwnerId}/movements");

                    if (string.IsNullOrWhiteSpace(moves) && string.IsNullOrWhiteSpace(attributes) && string.IsNullOrWhiteSpace(movements))
                        return false;

                    App.storage.Write($"{c.OwnerId}/moves.json", moves);
                    App.storage.Write($"{c.OwnerId}/attributes.json", attributes);
                    App.storage.Write($"{c.OwnerId}/movements.json", movements);
                }

                foreach (AttributeName a in attributeList)
                {
                    string attributeData = await APIConnection.Request($"/api/characterattributes/name/{a.name}");
                    if (string.IsNullOrWhiteSpace(attributeData))
                        return false;

                    App.storage.Write($"{a.name.ToLower()}/attributes.json", attributeData);
                }

                Runtime.Characters = characterList;
                Runtime.Attributes = attributeList;

                Runtime.Characters.Sort();
                Runtime.Attributes.Sort();

                return true;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine($"Error on sync {e.Message}");
            }
            return false;
        }
    }
}
