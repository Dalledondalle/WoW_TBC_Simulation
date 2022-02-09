using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml;

namespace Simulation.Library
{
    public class Wowhead
    {
        public Equipment GetItem(int id)
        {
            var json = XmlStringToJson($"https://tbc.wowhead.com/item={id}&xml");
            Equipment equipment = JsonConvert.DeserializeObject<Equipment>(json);
            return equipment;
        }

        private string XmlStringToJson(string xmlPath)
        {
            string slot = string.Empty;
            string output = string.Empty;
            XmlTextReader reader = new XmlTextReader(xmlPath);
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element)
                {
                    if (reader.Name == "inventorySlot")
                    {
                        reader.Read();
                        slot = reader.Value.ToString();
                    }

                    if (reader.Name == "jsonEquip")
                    {
                        reader.Read();
                        output = reader.Value.ToString();
                    }
                }
            }
            return "{\"invSlot\":\"" + slot + "\"," + output + "}";
        }
    }




    public class Equipment
    {
        [JsonProperty("invSlot")]
        public string InvSlot { get; set; }

        [JsonProperty("armor")]
        public int Armor { get; set; }

        [JsonProperty("classes")]
        public int Classes { get; set; }

        [JsonProperty("displayid")]
        public int Displayid { get; set; }

        [JsonProperty("dura")]
        public int Dura { get; set; }

        [JsonProperty("int")]
        public int Intellect { get; set; }

        [JsonProperty("itemset")]
        public int Itemset { get; set; }

        [JsonProperty("nsockets")]
        public int Nsockets { get; set; }

        [JsonProperty("reqlevel")]
        public int Reqlevel { get; set; }

        [JsonProperty("slotbak")]
        public int Slotbak { get; set; }

        [JsonProperty("socket1")]
        public int Socket1 { get; set; }

        [JsonProperty("socket2")]
        public int Socket2 { get; set; }
        [JsonProperty("socket3")]
        public int Socket3 { get; set; }

        [JsonProperty("socketbonus")]
        public int SocketBonus { get; set; }

        [JsonProperty("splcritstrkrtng")]
        public int SpellCritRating { get; set; }

        [JsonProperty("spldmg")]
        public int SpellPower { get; set; }

        [JsonProperty("arcsplpwr")]
        public int ArcaneSpellPower { get; set; }
        [JsonProperty("firsplpwr")]
        public int FireSpellPower { get; set; }
        [JsonProperty("frosplpwr")]
        public int FrostSpellPower { get; set; }
        [JsonProperty("shasplpwr")]
        public int ShadowSpellPower { get; set; }
        [JsonProperty("splhastertng")]
        public int SpellHasteRating { get; set; }

        [JsonProperty("splheal")]
        public int SpellHeal { get; set; }

        [JsonProperty("splhitrtng")]
        public int SpellHitRating { get; set; }

        [JsonProperty("sta")]
        public int Stamina { get; set; }

        [JsonProperty("manargn")]
        public int ManaRegn { get; set; }
    }
}
