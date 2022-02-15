using HtmlAgilityPack;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
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

        public Spell GetSpell(int id)
        {
            Spell spell = GetSpellFromFolder(id);
            if (spell is not null) return spell;
            return GetSpellFromWowhead(id);
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

        private void SaveSpellToLocalFolder(Spell spell)
        {
            string dir = Environment.CurrentDirectory + @"/Spells/";
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            string jsonString = JsonConvert.SerializeObject(spell);
            var fs = new FileStream(dir + spell.ID.ToString(), FileMode.Append, FileAccess.Write);
            var sw = new StreamWriter(fs);
            sw.WriteLine(jsonString);
            sw.Flush();
            sw.Close();
            fs.Close();
        }

        private Spell GetSpellFromFolder(int id)
        {
            string dir = Environment.CurrentDirectory + @"/Spells/";
            string jsonString = string.Empty;
            if(File.Exists(dir + id.ToString()))
                jsonString = File.ReadAllText(dir + id.ToString());

            if(!string.IsNullOrEmpty(jsonString))
                return JsonConvert.DeserializeObject<Spell>(jsonString);
            return null;
        }

        private Spell GetSpellFromWowhead(int id)
        {
            Spell spell = new();
            HtmlWeb web = new();
            HtmlDocument doc = web.Load($"https://tbc.wowhead.com/spell={id}");
            var SpellName = doc.DocumentNode.SelectSingleNode("//b[@class='whtt-name']").InnerText;
            var HeadersNames = doc.GetElementbyId("spelldetails");
            var ToolTipText = doc.DocumentNode.SelectSingleNode("//div[@class='q']").InnerText;
            List<string> informationTable = HeadersNames.InnerText.Split('\n').Select(x => x.Trim()).Where(x => !string.IsNullOrWhiteSpace(x) && !string.IsNullOrEmpty(x)).ToList();
            spell = new();
            spell.Setup(id.ToString(), SpellName, ToolTipText, informationTable);
            SaveSpellToLocalFolder(spell);
            return spell;
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

    public class Spell
    {
        public string ToolTipText { get; set; }
        public string ID { get; set; }
        public string Name { get; set; }
        public double Durtaion { get; set; }
        public string School { get; set; }
        public string Mechanic { get; set; }
        public string DispelType { get; set; }
        public string GCDCategory { get; set; }
        public int Cost { get; set; }
        public string RessourceCostType { get; set; }
        public string Range { get; set; }
        public double CastTime { get; set; }
        public double Cooldown { get; set; }
        public double GCD { get; set; }
        public string Flags { get; set; }
        public List<string> Effects { get; set; }

        public void Setup(string id, string spellName, string tooltipText, List<string> info)
        {
            ToolTipText = tooltipText;
            ID = id;
            Name = spellName;
            //Fix if it is minutes, seconds or hours
            string durtaiontxt = info[info.IndexOf("Duration") + 1];
            if (durtaiontxt == "n/a") Durtaion = 0;
            if (durtaiontxt.Contains("second")) Durtaion = ParseFromSecondsToMiliSecDouble(durtaiontxt.Split(' ')[0]);
            if (durtaiontxt.Contains("minute")) Durtaion = ParseFromMinutesToMiliSecDouble(durtaiontxt.Split(' ')[0]);
            if (durtaiontxt.Contains("hour")) Durtaion = ParseFromHoursToMiliSecDouble(durtaiontxt.Split(' ')[0]);

            School = info[info.IndexOf("School") + 1];
            Mechanic = info[info.IndexOf("Mechanic") + 1];
            DispelType = info[info.IndexOf("Dispel type") + 1];
            GCDCategory = info[info.IndexOf("GCD category") + 1];
            Cost = info[info.IndexOf("Cost") + 1] != "None" ? int.Parse(info[info.IndexOf("Cost") + 1].Split(' ')[0]) : 0;
            RessourceCostType = info[info.IndexOf("Cost") + 1] != "None" ? info[info.IndexOf("Cost") + 1].Split(' ')[1] : "n/a";
            Range = info[info.IndexOf("Range") + 1];
            CastTime = info[info.IndexOf("Cast time") + 1] != "Instant" ? double.Parse(info[info.IndexOf("Cast time") + 1].Split(' ')[0]) * 1000 : 0;
            //Fix if it is minutes, seconds or hours
            string cdtxt = info[info.IndexOf("Cooldown") + 1];
            if (cdtxt == "n/a") Cooldown = 0;
            if (cdtxt.Contains("second")) Cooldown = ParseFromSecondsToMiliSecDouble(cdtxt.Split(' ')[0]);
            if (cdtxt.Contains("minute")) Cooldown = ParseFromMinutesToMiliSecDouble(cdtxt.Split(' ')[0]);
            if (cdtxt.Contains("hour")) Cooldown = ParseFromHoursToMiliSecDouble(cdtxt.Split(' ')[0]);

            string gcdtxt = info[info.IndexOf("GCD") + 1];
            if (gcdtxt == "n/a") GCD = 0;
            if (gcdtxt.Contains("second")) GCD = ParseFromSecondsToMiliSecDouble(gcdtxt.Split(' ')[0]);
            if (gcdtxt.Contains("minute")) GCD = ParseFromMinutesToMiliSecDouble(gcdtxt.Split(' ')[0]);
            if (gcdtxt.Contains("hour")) GCD = ParseFromHoursToMiliSecDouble(gcdtxt.Split(' ')[0]);

            Effects = info.Where(x => x.Contains("Effect")).ToList();
            Flags = info[info.IndexOf("Flags") + 1];
        }

        private double ParseFromSecondsToMiliSecDouble(string str)
        {
            string intS = string.Empty;
            string decS = string.Empty;
            if(str.Contains(','))
            {
                intS = str.Split(',')[0];
                decS = str.Split(',')[1];
            }
            if (str.Contains('.'))
            {
                intS = str.Split('.')[0];
                decS = str.Split('.')[1];
            }
            while(decS.Length < 3)
            {
                decS += "0";
            }
            return double.Parse(intS + decS);
        }

        private double ParseFromMinutesToMiliSecDouble(string str)
        {
            string intS = string.Empty;
            string decS = string.Empty;
            if (str.Contains(','))
            {
                intS = str.Split(',')[0];
                decS = str.Split(',')[1];
            }
            if (str.Contains('.'))
            {
                intS = str.Split('.')[0];
                decS = str.Split('.')[1];
            }
            while (decS.Length < 3)
            {
                decS += "0";
            }
            return (double.Parse(intS) * 60) + (double.Parse(decS) * 60);
        }

        private double ParseFromHoursToMiliSecDouble(string str)
        {
            string intS = string.Empty;
            string decS = string.Empty;
            if (str.Contains(','))
            {
                intS = str.Split(',')[0];
                decS = str.Split(',')[1];
            }
            if (str.Contains('.'))
            {
                intS = str.Split('.')[0];
                decS = str.Split('.')[1];
            }
            while (decS.Length < 3)
            {
                decS += "0";
            }
            return (double.Parse(intS) * 3600) + (double.Parse(decS) * 3600);
        }
    }

    public class Aura
    {
        public string SpellID { get; set; }
        public string Name { get; set; }
        public AuraType AuraType { get; set; }
        public int FlatSpellMod { get; set; }
        public int FlatShadowMod { get; set; }
        public int FlatArcaneMod { get; set; }
        public int FlatFireMod { get; set; }
        public int FlatFrostMod { get; set; }
        public int FlatNatureMod { get; set; }
        public int FlatHolyMod { get; set; }
        public int SpellHasteRatingMod { get; set; }
        public int SpellCritRatingMod { get; set; }
        public int SpellHitRatingMod { get; set; }
        public int FlatManaRegenMod { get; set; }
        public int FlatIntMod { get; set; }
        public double IntModifer { get; set; }
        public double SpellModifer { get; set; }
        public double ShadowModifer { get; set; }
        public double FrostModifer { get; internal set; }
        public double ArcaneModifer { get; set; }
        public double FireModifer { get; set; }
        public double NatureModifer { get; set; }
        public double HolyModifer { get; set; }
        public double SpellHasteModifer { get; set; }
        public double SpellCritModifer { get; set; }
        public double SpellHitModifer { get; set; }
        public double Duration { get; set; }
        public double EndTimer { get; set; }
        public double NextTick { get; set; }

        public Aura(string spellID, string name, AuraType auraType)
        {
            SpellID = spellID;
            Name = name;
            AuraType = auraType;
        }
    }

    public class Talent
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public List<Effect> Effects { get; set; }
    }

    public class Effect
    {
        public Modify Modify { get; set; }
        public int Value { get; set; }
        public List<string> AffectedSpells { get; set; }
    }

    public enum Modify
    {
        Casttime,
        CasttimePercent,
        DamagePercent,
        DamageFlat,
        SpellPowerPercent,
        PeriodicDamagePercent,
        Hitchance,
        Critchance,
        CritDamagePercent,
        ManaPercent,
        ProcOnHit,
        Cooldown,
        LearnSpell
    }
    public enum AuraType
    {
        Buff, Debuff
    }
}
