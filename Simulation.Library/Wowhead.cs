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
        public static SocketBonus[] SocketBonuses = new SocketBonus[]
        {
            new(){ID = "2889", Stat = BonusStat.spellpower, Amount = 5},
            new(){ID = "2900", Stat = BonusStat.spellpower, Amount = 4},
            new(){ID = "2974", Stat = BonusStat.spellpower, Amount = 3},     //7 Healing and SP
            new(){ID = "2872", Stat = BonusStat.spellpower, Amount = 3},     //9 Healing and sp
            new(){ID = "3153", Stat = BonusStat.spellpower, Amount = 2},
            new(){ID = "3098", Stat = BonusStat.spellpower, Amount = 2},    //4 Healing and SP


            new(){ID = "2880", Stat = BonusStat.spellcrit, Amount = 2},
            new(){ID = "2875", Stat = BonusStat.spellcrit, Amount = 3},
            new(){ID = "2951", Stat = BonusStat.spellcrit, Amount = 4},


            new(){ID = "2909", Stat = BonusStat.spellhit, Amount = 2},
            new(){ID = "3153", Stat = BonusStat.spellhit, Amount = 3},


            new(){ID = "2881", Stat = BonusStat.MP5, Amount = 1},
            new(){ID = "2865", Stat = BonusStat.MP5, Amount = 2},


            new(){ID = "2863", Stat = BonusStat.intellect, Amount = 3},
            new(){ID = "94", Stat = BonusStat.intellect, Amount = 4},
        };
        public Equipment GetEquipment(int id)
        {
            var equipment = GetEquipementFromFolder(id);
            if (equipment is not null) return equipment;
            var json = XmlStringToJson($"https://tbc.wowhead.com/item={id}&xml");
            equipment = JsonConvert.DeserializeObject<Equipment>(json);
            equipment.ID = id.ToString();
            SaveEquipmentToLocalFolder(equipment);
            return equipment;
        }

        public Gem GetGem(int id)
        {
            Gem gem = GetGemFromFolder(id);
            if (gem is not null) return gem;
            string color = string.Empty;
            string output = string.Empty;
            XmlTextReader reader = new XmlTextReader($"https://tbc.wowhead.com/item={id}&xml");
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element)
                {
                    if (reader.Name == "subclass")
                    {
                        reader.Read();
                        color = reader.Value.ToString();
                    }

                    if (reader.Name == "jsonEquip")
                    {
                        reader.Read();
                        output = reader.Value.ToString();
                    }
                }
            }
            gem = JsonConvert.DeserializeObject<Gem>("{" + output + "}");
            gem.Color = color.Replace(" Gems", "");
            gem.ID = id;
            SaveGemToLocalFolder(gem);
            return gem;
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

        private void SaveGemToLocalFolder(Gem gem)
        {
            string dir = Environment.CurrentDirectory + @"/Gems/";
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            string jsonString = JsonConvert.SerializeObject(gem);
            var fs = new FileStream(dir + gem.ID.ToString(), FileMode.Append, FileAccess.Write);
            var sw = new StreamWriter(fs);
            sw.WriteLine(jsonString);
            sw.Flush();
            sw.Close();
            fs.Close();
        }

        private void SaveEquipmentToLocalFolder(Equipment equipment)
        {
            string dir = Environment.CurrentDirectory + @"/Equipment/";
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            string jsonString = JsonConvert.SerializeObject(equipment);
            var fs = new FileStream(dir + equipment.ID.ToString(), FileMode.Append, FileAccess.Write);
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
            if (File.Exists(dir + id.ToString()))
                jsonString = File.ReadAllText(dir + id.ToString());

            if (!string.IsNullOrEmpty(jsonString))
                return JsonConvert.DeserializeObject<Spell>(jsonString);
            return null;
        }

        private Gem GetGemFromFolder(int id)
        {
            string dir = Environment.CurrentDirectory + @"/Gems/";
            string jsonString = string.Empty;
            if (File.Exists(dir + id.ToString()))
                jsonString = File.ReadAllText(dir + id.ToString());

            if (!string.IsNullOrEmpty(jsonString))
                return JsonConvert.DeserializeObject<Gem>(jsonString);
            return null;
        }

        private Equipment GetEquipementFromFolder(int id)
        {
            string dir = Environment.CurrentDirectory + @"/Equipment/";
            string jsonString = string.Empty;
            if (File.Exists(dir + id.ToString()))
                jsonString = File.ReadAllText(dir + id.ToString());

            if (!string.IsNullOrEmpty(jsonString))
                return JsonConvert.DeserializeObject<Equipment>(jsonString);
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
        [JsonProperty("id")]
        public string ID { get; set; }
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
        public SocketColor Socket1 { get; set; }

        [JsonProperty("socket2")]
        public SocketColor Socket2 { get; set; }
        [JsonProperty("socket3")]
        public SocketColor Socket3 { get; set; }

        [JsonIgnore]
        public Gem Gem1 { get; private set; }
        [JsonIgnore]
        public Gem Gem2 { get; private set; }
        [JsonIgnore]
        public Gem Gem3 { get; private set; }

        [JsonProperty("socketbonus")]
        public string SocketBonus { get; set; }
        [JsonIgnore]
        public bool IsSocketBonusActive
        {
            get
            {
                if (Socket1 != SocketColor.Dummy && Socket1 != SocketColor.Meta)
                    if (!DoesSocketMatchGem(Socket1, Gem1)) return false;
                if (Socket2 != SocketColor.Dummy && Socket2 != SocketColor.Meta)
                    if (!DoesSocketMatchGem(Socket2, Gem2)) return false;
                if (Socket3 != SocketColor.Dummy && Socket3 != SocketColor.Meta)
                    if (!DoesSocketMatchGem(Socket3, Gem3)) return false;
                return true;
            }
        }

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

        public void SocketGem(Gem gem, int gemslot)
        {
            switch (gemslot)
            {
                case 1:
                    if(Socket1 != SocketColor.Dummy)
                    {
                        if(Socket1 != SocketColor.Meta && gem.Color != "Meta")
                            Gem1 = gem;
                        if (Socket1 == SocketColor.Meta && gem.Color == "Meta")
                            Gem1 = gem;
                    }
                    break;
                case 2:
                    if (Socket2 != SocketColor.Dummy)
                    {
                        if (Socket2 != SocketColor.Meta && gem.Color != "Meta")
                            Gem2 = gem;
                        if (Socket2 == SocketColor.Meta && gem.Color == "Meta")
                            Gem2 = gem;
                    }
                    break;
                case 3:
                    if (Socket3 != SocketColor.Dummy)
                    {
                        if (Socket3 != SocketColor.Meta && gem.Color != "Meta")
                            Gem3 = gem;
                        if (Socket3 == SocketColor.Meta && gem.Color == "Meta")
                            Gem1 = gem;
                    }
                    break;
            }
        }

        private bool DoesSocketMatchGem(SocketColor socket, Gem gem)
        {
            if (gem is null) return false;
            string[] redGems = new[] { "Red", "Purple", "Orange" };
            string[] YellowGems = new[] { "Yellow", "Orange", "Green" };
            string[] BlueGems = new[] { "Blue", "Purple", "Green" };
            if (socket != SocketColor.Dummy && socket != SocketColor.Meta)
            {
                if (socket == SocketColor.Red)
                {
                    if (!redGems.Contains(gem.Color)) return false;
                }
                if (socket == SocketColor.Yellow)
                {
                    if (!YellowGems.Contains(gem.Color)) return false;
                }
                if (socket == SocketColor.Blue)
                {
                    if (!BlueGems.Contains(gem.Color)) return false;
                }
            }
            return true;
        }
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
            if (gcdtxt.Contains("milisecond")) GCD = ParseFromMiliToMiliSecDouble(gcdtxt.Split(' ')[0]);
            if (gcdtxt.Contains("second")) GCD = ParseFromSecondsToMiliSecDouble(gcdtxt.Split(' ')[0]);
            if (gcdtxt.Contains("minute")) GCD = ParseFromMinutesToMiliSecDouble(gcdtxt.Split(' ')[0]);
            if (gcdtxt.Contains("hour")) GCD = ParseFromHoursToMiliSecDouble(gcdtxt.Split(' ')[0]);

            Effects = info.Where(x => x.Contains("Effect")).ToList();
            Flags = info[info.IndexOf("Flags") + 1];
        }
        private double ParseFromMiliToMiliSecDouble(string str)
        {
            return double.Parse(str.Replace(",", "").Replace(".", ""));
        }

        private double ParseFromSecondsToMiliSecDouble(string str)
        {
            return double.Parse(str.Replace(",", "").Replace(".", "")) * 1000;
        }

        private double ParseFromMinutesToMiliSecDouble(string str)
        {
            return double.Parse(str.Replace(",", "").Replace(".", "")) * 60 * 1000;
        }

        private double ParseFromHoursToMiliSecDouble(string str)
        {
            return double.Parse(str.Replace(",", "").Replace(".", "")) * 60 * 60 * 1000;
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
        PeriodicProc,
        Cooldown,
        LearnSpell,
        Unique,
        SpellEffectiveness,
        MaxMana,
        AuraDuration
    }
    public enum AuraType
    {
        Buff, Debuff
    }

    public class SocketBonus
    {
        public string ID { get; set; }
        public BonusStat Stat { get; set; }
        public int Amount { get; set; }
    }

    public enum BonusStat
    {
        spellpower,
        spellcrit,
        spellhaste,
        spellhit,
        intellect,
        MP5
    }

    public enum SocketColor
    {
        Dummy,
        Meta,
        Red,
        Yellow,
        Blue
    }

    public class Gem
    {
        [JsonProperty("id")]
        public int ID { get; set; }
        [JsonProperty("color")]
        public string Color { get; set; }
        [JsonProperty("sta")]
        public int Stamina { get; set; }
        [JsonProperty("int")]
        public int Intellect { get; set; }
        [JsonProperty("agi")]
        public int Agility { get; set; }
        [JsonProperty("str")]
        public int Strength { get; set; }
        [JsonProperty("spi")]
        public int Spirit { get; set; }
        [JsonProperty("manargn")]
        public int MP5 { get; set; }
        [JsonProperty("splpen")]
        public int SpellPenetration { get; set; }
        [JsonProperty("spldmg")]
        public int SpellPower { get; set; }
        [JsonProperty("splheal")]
        public int HealingPower { get; set; }
        [JsonProperty("mleatkpwr")]
        public int MeleeAttackPower { get; set; }
        [JsonProperty("rgdatkpwr")]
        public int RangedAttackPower { get; set; }
        [JsonProperty("dodgertng")]
        public int DodgeRating { get; set; }
        [JsonProperty("parryrtng")]
        public int ParryRating { get; set; }
        [JsonProperty("rgdhitrtng")]
        public int RangedHitRating { get; set; }
        [JsonProperty("mlehitrtng")]
        public int MeleeHitRating { get; set; }
        [JsonProperty("mlecritstrkrtng")]
        public int MeleeCritRating { get; set; }
        [JsonProperty("rgdcritstrkrtng")]
        public int RangedCritRating { get; set; }
        [JsonProperty("resirtng")]
        public int ResilienceRating { get; set; }
        [JsonProperty("splhitrtng")]
        public int SpellHitRating { get; set; }
        [JsonProperty("splhastertng")]
        public int SpellHasteRating { get; set; }
        [JsonProperty("splcritstrkrtng")]
        public int SpellCritRating { get; set; }
        [JsonProperty("defrtng")]
        public int DefenseRating { get; set; }
    }
}
