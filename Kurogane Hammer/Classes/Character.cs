using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.IO;

namespace Kurogane_Hammer.Classes
{
    public class Character : IComparable<Character>
    {
        public string Name
        {
            get
            {
                return DisplayName;
            }
        }
        public string DisplayName, ThumbnailUrl, ColorTheme;
        public int OwnerId;
        public bool hasSpecificAttributes, favorite = false;
        public SpecificAttribute specificAttribute;

        public Character()
        {

        }

        public Character(int id, String name)
        {
            this.OwnerId = id;
            if (name == "Game & Watch")
                this.DisplayName = "Mr. " + name;
            else
                this.DisplayName = name;
        }

        public Character(int id, string name, string color) : this(id, name)
        {
            this.ColorTheme = color;
            CheckSpecificAttributes();
        }

        public void CheckSpecificAttributes()
        {
            hasSpecificAttributes = App.storage.FileExists($"{OwnerId}/specificAttributes.json");
            if (hasSpecificAttributes)
            {
                try
                {
                    specificAttribute = Newtonsoft.Json.JsonConvert.DeserializeObject<SpecificAttribute>(App.storage.Read($"{OwnerId}/specificAttributes.json"));
                }
                catch (Exception e)
                {
                    specificAttribute = null;
                    hasSpecificAttributes = false;
                }
            }
        }

        public string Image
        {
            get
            {
                return $"character_{OwnerId}.png";
            }
        }

        public override string ToString()
        {
            return DisplayName;
        }

        public string getCharacterTitleName()
        {
            switch (DisplayName)
            {
                case "King Dedede":
                    return "Perfection Incarnate";
                case "Zero Suit Samus":
                    return "Zero Skill Samus";
            }
            return DisplayName;
        }

        public string GetCharacterImageName()
        {
            switch (DisplayName)
            {
                case "Lucas":
                    return "Ridley";
            }
            return DisplayName;
        }

        public int CompareTo(Character other)
        {
            if(favorite == other.favorite)
            {
                if (OwnerId < other.OwnerId)
                    return -1;
                if (OwnerId > other.OwnerId)
                    return 1;

                return 0;
            }
            if (favorite)
                return -1;
            if (other.favorite)
                return 1;

            return 0;
        }
    }

    public class AttributeRank : IComparable<AttributeRank>, IEquatable<AttributeRank>
    {
        public Character character;
        public Attribute attribute;
        public int rank;
        public string name;
        public List<string> types = new List<string>();
        public Dictionary<string, string> values = new Dictionary<string, string>();

        public AttributeRank()
        {

        }

        public AttributeRank(string name, Attribute attribute, Character character)
        {
            this.name = name;
            this.attribute = attribute;
            this.character = character;
            rank = -1;

            foreach (AttributeValue v in attribute.attributes)
            {
                types.Add(v.name);
                values.Add(v.name, v.value);
            }
        }

        public AttributeRank(string name, Attribute attribute, Character character, int rank) : this(name, attribute, character)
        {
            this.rank = rank;
        }

        public void add(string type, string value)
        {
            values.Add(type, value);
        }

        public int CompareTo(AttributeRank other)
        {
            if (rank < other.rank)
                return -1;
            if (rank > other.rank)
                return 1;
            return 0;
        }

        public bool Equals(AttributeRank other)
        {
            return (character.OwnerId == other.character.OwnerId && attribute.name == other.attribute.name);
        }

        public string GetCharacterName()
        {
            switch (attribute.name)
            {
                case "Gravity":
                    switch (character.DisplayName)
                    {
                        case "Marth":
                            return "Lucina's worthless grandfather";
                        case "Lucina":
                            return "Marth's worthless granddaughter";
                        case "Mii Swordfighter":
                            return "Mii Swordspider";
                    }
                    break;

                case "RunSpeed":
                    switch (character.DisplayName)
                    {
                        case "Ness":
                            return "Ebola Back Throw";
                    }
                    break;
            }
            return character.DisplayName;
        }
    }
}
