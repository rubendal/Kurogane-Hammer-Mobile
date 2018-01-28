using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kurogane_Hammer.PlatformDependent;
using Kurogane_Hammer.Classes;
using Newtonsoft.Json;

namespace Kurogane_Hammer
{
    public static class Initialization
    {
        private static readonly string VERSION = "2.0";

        public static void Initialize(Storage storage, AssetResourceManager assetManager)
        {
            if (!storage.FileExists("init/version.bin"))
                WriteInitialData(storage, assetManager);
            else
            {
                string version = storage.Read("init/version.bin");
                System.Diagnostics.Debug.WriteLine($"Storage version {version}");
                if (version != VERSION)
                    WriteInitialData(storage, assetManager);
            }
            System.Diagnostics.Debug.WriteLine($"Storage Checked");
        }

        private static void WriteInitialData(Storage storage, AssetResourceManager assetManager)
        {
            string json = assetManager.ReadAsset("characters.json");
            storage.Write("data/characters.json", json);
            
            try
            {
                List<Character> characterList = JsonConvert.DeserializeObject<List<Character>>(json);
                foreach(Character c in characterList)
                {
                    json = assetManager.ReadAsset($"Data/{c.OwnerId}/moves.json");
                    storage.Write($"{c.OwnerId}/moves.json", JsonConvert.SerializeObject(Runtime.ConvertMoveJson(json), new JsonSerializerSettings {
                        TypeNameHandling = TypeNameHandling.Objects,
                        TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple
                    }));
                    json = assetManager.ReadAsset($"Data/{c.OwnerId}/attributes.json");
                    storage.Write($"{c.OwnerId}/attributes.json", json);
                    json = assetManager.ReadAsset($"Data/{c.OwnerId}/movements.json");
                    storage.Write($"{c.OwnerId}/movements.json", json);
                    try
                    {
                        json = assetManager.ReadAsset($"Data/{c.OwnerId}/specificAttributes.json");
                        if (json != null)
                            storage.Write($"{c.OwnerId}/specificAttributes.json", json);
                    }
                    catch
                    {

                    }
                }
                json = assetManager.ReadAsset("formulas.json");
                storage.Write("data/formulas.json", json);

                json = assetManager.ReadAsset("Data/attributeNames.json");
                storage.Write("data/attributeNames.json", json);

                List<AttributeName> attributes = JsonConvert.DeserializeObject<List<AttributeName>>(json);
                foreach(AttributeName a in attributes)
                {
                    json = assetManager.ReadAsset($"Data/Attributes/{a.name.ToLower()}/attributes.json");
                    storage.Write($"{a.name.ToLower()}/attributes.json", json);
                }
                storage.Write("init/version.bin", VERSION);
            }
            catch(Exception e)
            {
                System.Diagnostics.Debug.WriteLine($"Error initializing storage: {e.Message}");
            }
            System.Diagnostics.Debug.WriteLine($"Storage Initialized");
        }
    }
}
