using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Xamarin.Forms;
using System.IO;

namespace Kurogane_Hammer.Classes
{
    public class Attribute
    {
        public string name, formattedName = "";
        public List<AttributeValue> attributes;
        public int ownerId;

        public Attribute()
        {

        }

        public Attribute(string name, List<AttributeValue> attributes, int ownerId)
        {
            this.name = name;
            Regex regexp = new Regex("(?<=.)(?=\\p{Lu})");
            string[] w = regexp.Split(name);
            foreach(string s in w){
                if (s != null)
                    formattedName += $"{s} ";
            }
            formattedName = formattedName.TrimEnd();
            this.attributes = attributes;
            this.ownerId = ownerId;
        }

        public List<string> GetAttributeValues()
        {
            if (attributes.Count == 0)
                return null;

            List<string> s = new List<string>();
            foreach(AttributeValue a in attributes)
            {
                s.Add(a.name);
            }

            return s;
        }

        public string Image
        {
            get
            {
                return $"attribute_{name.ToLower()}.png";
            }
        }

        public static Attribute Convert(AttributeData data)
        {
            return new Attribute(data.Name, data.Values.Select(d => AttributeValue.Convert(d)).ToList(), data.OwnerId);
        }
    }

    public class AttributeValue
    {
        public string name, value;
        public int ownerId;

        public AttributeValue()
        {

        }

        public AttributeValue(int ownerId, string name, string value)
        {
            this.ownerId = ownerId;
            this.name = name;
            this.value = value;
        }

        public static AttributeValue Convert(AttributeValueData data)
        {
            return new AttributeValue(data.OwnerId, data.Name, data.Value);
        }
    }

    public class AttributeName : IComparable<AttributeName>
    {
        public string name;

        public string formattedName
        {
            get
            {
                Regex regexp = new Regex("(?<=.)(?=\\p{Lu})");
                string[] w = regexp.Split(name);
                string n = "";
                foreach (string s in w)
                {
                    if (s != null)
                        n += $"{s} ";
                }
                return n.TrimEnd();
            }
        }

        public bool favorite = false;

        public AttributeName()
        {

        }

        public AttributeName(string name)
        {
            this.name = name;
            
        }

        public string Image
        {
            get
            {
                return $"attribute_{name.ToLower()}.png";
            }
        }

        //public ImageSource getImage(PlatformDependent.AssetResourceManager assetManager)
        //{
        //    try
        //    {
        //        return ImageSource.FromStream(() =>
        //        {
        //            return new MemoryStream(assetManager.ReadAssetData($"Images/attributes/{name.ToLower()}/image.png"));
        //        });

        //    }
        //    catch (Exception e)
        //    {
        //        try
        //        {
        //            return ImageSource.FromStream(() =>
        //            {
        //                return new MemoryStream(assetManager.ReadAssetData($"Images/attributes/{name.ToLower()}/image.jpg"));
        //            });
        //        }
        //        catch (Exception e2)
        //        {

        //        }
        //    }
        //    return null;
        //}

        public int CompareTo(AttributeName other)
        {
            if (favorite == other.favorite)
                return name.CompareTo(other.name);
            if (favorite)
                return -1;
            if (other.favorite)
                return 1;
            return name.CompareTo(other.name);
        }

        //public static AttributeName Convert(AttributeNameData data)
        //{
        //    return new AttributeName(data.Name);
        //}
    }

    //public class AttributeNameData
    //{
    //    public string Name;

    //    public AttributeNameData()
    //    {

    //    }
    //}

    public static class AttributeRankMaker
    {
        private static List<Character> characters = null;

        private static readonly List<string> ASC_ATTRIBUTES_SORT = new List<string>(){
            "AerialJump",
            "AirAcceleration",
            "AirFriction",
            "FallSpeed",
            "FullHop",
            "Gravity",
            "LedgeHop",
            "ShortHop",
            "Traction",
            "WalkSpeed",
            "AirSpeed",
            "RunSpeed",
            "ShieldSize",
            "Weight"

        };

        private static readonly List<string> FAF_SORT = new List<string>(){
            "AirDodge",
            "Rolls",
            "Spotdodge"

        };

        private static readonly List<string> THIRD_COLUMN_SORT = new List<string>(){
            "AirAcceleration"

        };

        private static void Initialize()
        {
            if (characters != null)
                return;

            try
            {
                characters = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Character>>(App.storage.Read("data/characters.json"));
                characters.Sort();
            }
            catch (Exception e)
            {
                characters = null;
            }
        }

        private static Character getCharacter(int id)
        {
            if (characters == null)
                return null;

            try
            {
                return characters.Find((c) => c.OwnerId == id);
            }
            catch
            {

            }
            return null;
        }

        private static Comparison<Attribute> thirdColumnSort = new Comparison<Attribute>((a, b) =>
        {
            string v1 = a.attributes[2].value.Split(' ')[0].Replace("?", "");
            string v2 = b.attributes[2].value.Split(' ')[0].Replace("?", "");

            bool containsEndFrame = v1.Contains("-") && v2.Contains("-");

            float f1, f2;
            if (!float.TryParse(v1.Split('-')[0], out f1))
                return -1;
            if (!float.TryParse(v2.Split('-')[0], out f2))
                return 1;

            if (f1 < f2)
                return 1;
            if (f1 > f2)
                return -1;

            if (containsEndFrame)
            {
                if (!float.TryParse(v1.Split('-')[1], out f1))
                    return -1;
                if (!float.TryParse(v2.Split('-')[1], out f2))
                    return 1;

                if (f1 < f2)
                    return -1;
                if (f1 > f2)
                    return 1;
            }

            return 0;
        });

        private static Comparison<Attribute> usesFAF(bool usesAsc)
        {
            Comparison<Attribute> comparator = new Comparison<Attribute>((a, b) => {
                int value = usesAsc ? -1 : 1;
                string v1, v2;
                float f1, f2;
                bool containsEndFrame = false;
                if (a.attributes.Count >= 2)
                {
                    v1 = a.attributes[1].value.Split(' ')[0].Replace("?", "");
                    v2 = b.attributes[1].value.Split(' ')[0].Replace("?", "");

                    containsEndFrame = v1.Contains("-") && v2.Contains("-");

                    if (!float.TryParse(v1.Split('-')[0], out f1))
                        return 1 * value;
                    if (!float.TryParse(v2.Split('-')[0], out f2))
                        return -1 * value;

                    if (f1 < f2)
                        return -1 * value;
                    if (f1 > f2)
                        return 1 * value;

                    if (containsEndFrame)
                    {
                        if (!float.TryParse(v1.Split('-')[1], out f1))
                            return 1 * value;
                        if (!float.TryParse(v2.Split('-')[1], out f2))
                            return -1 * value;

                        if (f1 < f2)
                            return 1 * value;
                        if (f1 > f2)
                            return -1 * value;
                    }



                }

                v1 = a.attributes[0].value.Split(' ')[0].Replace("?", "");
                v2 = b.attributes[0].value.Split(' ')[0].Replace("?", "");

                

                containsEndFrame = v1.Contains("-") && v2.Contains("-");

                if (!float.TryParse(v1.Split('-')[0], out f1))
                    return 1 * value;
                if (!float.TryParse(v2.Split('-')[0], out f2))
                    return -1 * value;

                if (f1 < f2)
                    return -1 * value;
                if (f1 > f2)
                    return 1 * value;

                if (containsEndFrame)
                {
                    if (!float.TryParse(v1.Split('-')[1], out f1))
                        return 1 * value;
                    if (!float.TryParse(v2.Split('-')[1], out f2))
                        return -1 * value;

                    if (f1 < f2)
                        return 1 * value;
                    if (f1 > f2)
                        return -1 * value;
                }


                return 0;
            });

            return comparator;
        }

        private static Comparison<Attribute> normalComparator(bool usesAsc)
        {
            Comparison<Attribute> comparator = new Comparison<Attribute>((a, b) => {
                int value = usesAsc ? -1 : 1;
                string v1, v2;
                float f1, f2;
                bool containsEndFrame = false;

                v1 = a.attributes[0].value.Split(' ')[0].Replace("?", "");
                v2 = b.attributes[0].value.Split(' ')[0].Replace("?", "");



                containsEndFrame = v1.Contains("-") && v2.Contains("-");

                if (!float.TryParse(v1.Split('-')[0], out f1))
                    return 1 * value;
                if (!float.TryParse(v2.Split('-')[0], out f2))
                    return -1 * value;

                if (f1 < f2)
                    return -1 * value;
                if (f1 > f2)
                    return 1 * value;

                if (containsEndFrame)
                {
                    if (!float.TryParse(v1.Split('-')[1], out f1))
                        return 1 * value;
                    if (!float.TryParse(v2.Split('-')[1], out f2))
                        return -1 * value;

                    if (f1 < f2)
                        return 1 * value;
                    if (f1 > f2)
                        return -1 * value;
                }

                if (a.attributes.Count >= 2)
                {
                    v1 = a.attributes[1].value.Split(' ')[0].Replace("?", "");
                    v2 = b.attributes[1].value.Split(' ')[0].Replace("?", "");

                    containsEndFrame = v1.Contains("-") && v2.Contains("-");

                    if (!float.TryParse(v1.Split('-')[0], out f1))
                        return 1 * value;
                    if (!float.TryParse(v2.Split('-')[0], out f2))
                        return -1 * value;

                    if (f1 < f2)
                        return -1 * value;
                    if (f1 > f2)
                        return 1 * value;

                    if (containsEndFrame)
                    {
                        if (!float.TryParse(v1.Split('-')[1], out f1))
                            return 1 * value;
                        if (!float.TryParse(v2.Split('-')[1], out f2))
                            return -1 * value;

                        if (f1 < f2)
                            return 1 * value;
                        if (f1 > f2)
                            return -1 * value;
                    }



                }



                return 0;
            });

            return comparator;
        }

        public static List<AttributeRank> BuildRankList(List<Attribute> list)
        {
            Initialize();

            List<AttributeRank> ranks = new List<AttributeRank>();

            

            try
            {
                if (THIRD_COLUMN_SORT.Contains(list[0].name))
                {
                    list.Sort(thirdColumnSort);
                }
                if (FAF_SORT.Contains(list[0].name))
                {
                    list.Sort(usesFAF(ASC_ATTRIBUTES_SORT.Contains(list[0].name)));
                }
                else
                {
                    list.Sort(normalComparator(ASC_ATTRIBUTES_SORT.Contains(list[0].name)));
                }

                int i = 1;
                foreach(Attribute a in list)
                {
                    ranks.Add(new AttributeRank(a.name, a, getCharacter(a.ownerId), i));
                    i++;
                }

            }
            catch(Exception e)
            {
                ranks = new List<AttributeRank>();
                int i = 1;
                foreach (Attribute a in list)
                {
                    ranks.Add(new AttributeRank(a.name, a, getCharacter(a.ownerId), i));
                    i++;
                }
                System.Diagnostics.Debug.WriteLine($"Using no comparator {e.Message}");
            }

            return ranks;
        }


    }

    public class AttributeData
    {
        public string Name, Owner;
        public int OwnerId;
        public List<AttributeValueData> Values;

        public AttributeData()
        {

        }

    }

    public class AttributeValueData
    {
        public string Value, Owner, Name;
        public int OwnerId;

        public AttributeValueData()
        {

        }
    }
}
