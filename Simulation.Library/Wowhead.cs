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
            new(){ID = "2889", Stat = Modify.SpellPower, Amount = 5},
            new(){ID = "2900", Stat = Modify.SpellPower, Amount = 4},
            new(){ID = "2974", Stat = Modify.SpellPower, Amount = 3},     //7 Healing and SP
            new(){ID = "2872", Stat = Modify.SpellPower, Amount = 3},     //9 Healing and sp
            new(){ID = "3153", Stat = Modify.SpellPower, Amount = 2},
            new(){ID = "3098", Stat = Modify.SpellPower, Amount = 2},    //4 Healing and SP


            new(){ID = "2880", Stat = Modify.SpellCritRating, Amount = 2},
            new(){ID = "2875", Stat = Modify.SpellCritRating, Amount = 3},
            new(){ID = "2951", Stat = Modify.SpellCritRating, Amount = 4},


            new(){ID = "2909", Stat = Modify.SpellHitRating, Amount = 2},
            new(){ID = "3153", Stat = Modify.SpellHitRating, Amount = 3},


            new(){ID = "2881", Stat = Modify.MP5Flat, Amount = 1},
            new(){ID = "2865", Stat = Modify.MP5Flat, Amount = 2},


            new(){ID = "2863", Stat = Modify.IntellectFlat, Amount = 3},
            new(){ID = "94", Stat = Modify.IntellectFlat, Amount = 4},
        };
        public static Aura[] Auras = new Aura[]
        {
            new Aura("32837", "Spell Focus Trigger", AuraType.Buff) {Effects = new Effect[]{ new(){ AuraID = "32837", InternalCD = 35000, Modify = Modify.SpellHasteRating, Value = 320, ProcChance = 15 } }, Duration = 6000}
        };
        public static SetBonus[] SetBonus = new SetBonus[]
        {
            new SetBonus(){ID = 670, TwoSet = new(){AffectedSpells = new[]{ "Corruption", "Immolate"}, AuraID = "38394", ProcChance = 100, Value = 70, Modify = Modify.HealFlatOnPeriodicDamage }, FourSet = new(){AffectedSpells = new[]{ "Shadow Bolt", "Incinerate"}, AuraID = "38393", Value = 6, Modify = Modify.DamagePercent}},//Tier 6
            new SetBonus(){ID = 646, TwoSet = new(){AffectedSpells = new[]{ "Any"}, AuraID = "38394", ProcChance = 100, Value = 15, Modify = Modify.HealPercentOffDamageOnDamage }, FourSet = new(){AffectedSpells = new[]{ "Corruption", "Immolate"}, AuraID = "38393", Value = 10, Modify = Modify.Unique}},//Tier 5
            new SetBonus(){ID = 645, TwoSet = new(){AffectedSpells = new[]{ "Any"}, AuraID = "37377", ProcChance = 5, Value = 135, Modify = Modify.Unique }, FourSet = new(){AffectedSpells = new[]{ "Corruption", "Immolate"}, AuraID = "37380", Value = 3000, Modify = Modify.AuraDuration}},//Tier 4
            new SetBonus(){ID = 559, TwoSet = new(){AffectedSpells = new[]{ "Any"}, AuraID = "37377", ProcChance = 5, Value = 92, Modify = Modify.ProcOnHit }},//Spellstrike
            new SetBonus(){ID = 552, TwoSet = new(){AffectedSpells = new[]{ "Any"}, AuraID = "32196", Value = 7, Modify = Modify.Unique }},//Spellfire
        };
        public static Enchant[] WeaponEnchants = new Enchant[]
        {
            new(){ID = "35460", Name = "Soulfrost", EquipmentSlot = EquipmentSlot.Mainhand, Effects = new Effect[]{ new() { Modify = Modify.ShadowPower, Value = 54}, new() { Modify = Modify.FrostPower, Value = 54 } } },
            new(){ID = "35462", Name = "Sunfire", EquipmentSlot = EquipmentSlot.Mainhand, Effects = new Effect[]{ new() { Modify = Modify.FirePower, Value = 50}, new() { Modify = Modify.ArcanePower, Value = 50 } } },
            new(){ID = "35456", Name = "Major Spellpower", EquipmentSlot = EquipmentSlot.Mainhand, Effects = new Effect[]{ new() { Modify = Modify.SpellPower, Value = 40} } },
            new(){ID = "35455", Name = "Major Intellect", EquipmentSlot = EquipmentSlot.Mainhand, Effects = new Effect[]{ new() { Modify = Modify.IntellectFlat, Value = 30} } },
            new(){ID = "22749", Name = "Spell Power", EquipmentSlot = EquipmentSlot.Mainhand, Effects = new Effect[]{ new() { Modify = Modify.SpellPower, Value = 30} } }//Wrong ID
        };
        public static Enchant[] HeadEnchants = new Enchant[]
        {
            new(){ID = "35447", Name = "Glyph Of Power", EquipmentSlot = EquipmentSlot.Head, Effects = new Effect[]{ new() { Modify = Modify.ShadowPower, Value = 22}, new() { Modify = Modify.SpellHitRating, Value = 14 } } },
            new(){ID = "35445", Name = "Glyph Of Renewal", EquipmentSlot = EquipmentSlot.Head, Effects = new Effect[]{ new() { Modify = Modify.SpellPower, Value = 12}, new() { Modify = Modify.MP5Flat, Value = 7 }, new() { Modify = Modify.HealingPower, Value = 35 } } },
            new(){ID = "24165", Name = "Hoodoo Hex", EquipmentSlot = EquipmentSlot.Head, Effects = new Effect[]{ new() { Modify = Modify.SpellPower, Value = 18} } },
        };
        public static Enchant[] ShoulderEnchants = new Enchant[]
        {
            new(){ID = "35406", Name = "Greater Inscription of Discipline", EquipmentSlot = EquipmentSlot.Shoulders, Effects = new Effect[]{ new() { Modify = Modify.SpellPower, Value = 18}, new() { Modify = Modify.SpellCritRating, Value = 10 } } },
            new(){ID = "35437", Name = "Greater Inscription of the Orb", EquipmentSlot = EquipmentSlot.Shoulders, Effects = new Effect[]{ new() { Modify = Modify.SpellPower, Value = 12}, new() { Modify = Modify.SpellCritRating, Value = 15 } } },
            new(){ID = "35405", Name = "Inscription of Discipline", EquipmentSlot = EquipmentSlot.Shoulders, Effects = new Effect[]{ new() { Modify = Modify.SpellPower, Value = 15} } },
            new(){ID = "35436", Name = "Inscription of the Orb", EquipmentSlot = EquipmentSlot.Shoulders, Effects = new Effect[]{ new() { Modify = Modify.SpellCritRating, Value = 13} } },
            new(){ID = "29467", Name = "Power of the Scourge", EquipmentSlot = EquipmentSlot.Shoulders, Effects = new Effect[]{ new() { Modify = Modify.SpellPower, Value = 15 }, new() { Modify = Modify.SpellCritRating, Value = 14} } },
            new(){ID = "24421", Name = "Zandalar Signet of Mojo", EquipmentSlot = EquipmentSlot.Shoulders, Effects = new Effect[]{ new() { Modify = Modify.SpellPower, Value = 18} } }
        };
        public static Enchant[] ChestEnchants = new Enchant[]
        {
            new(){ID = "46502", Name = "Exceptional Stats", EquipmentSlot = EquipmentSlot.Chest, Effects = new Effect[]{ new() { Modify = Modify.IntellectFlat, Value = 6} } },
            new(){ID = "46497", Name = "Restore Mana Prime", EquipmentSlot = EquipmentSlot.Chest, Effects = new Effect[]{ new() { Modify = Modify.MP5Flat, Value = 6} } },
            new(){ID = "32399", Name = "Magister's Armor Kit", EquipmentSlot = EquipmentSlot.Chest, Effects = new Effect[]{ new() { Modify = Modify.MP5Flat, Value = 3} } },
            new(){ID = "19988", Name = "Greater Stats", EquipmentSlot = EquipmentSlot.Chest, Effects = new Effect[]{ new() { Modify = Modify.IntellectFlat, Value = 4} } },//Might be wrong ID
        };
        public static Enchant[] WristEnchants = new Enchant[]
        {
            new(){ID = "46498", Name = "Spellpower", EquipmentSlot = EquipmentSlot.Wrist, Effects = new Effect[]{ new() { Modify = Modify.SpellPower, Value = 15} } },
            new(){ID = "46500", Name = "Superior Healing", EquipmentSlot = EquipmentSlot.Wrist, Effects = new Effect[]{ new() { Modify = Modify.SpellPower, Value = 10 }, new() { Modify = Modify.HealingPower, Value = 30 } } },
            new(){ID = "46496", Name = "Major Intellect", EquipmentSlot = EquipmentSlot.Wrist, Effects = new Effect[]{ new() { Modify = Modify.IntellectFlat, Value = 12} } },
            new(){ID = "46499", Name = "Stats", EquipmentSlot = EquipmentSlot.Wrist, Effects = new Effect[]{ new() { Modify = Modify.IntellectFlat, Value = 4} } },
            new(){ID = "23802", Name = "Healing Power", EquipmentSlot = EquipmentSlot.Wrist, Effects = new Effect[]{ new() { Modify = Modify.SpellPower, Value = 8 }, new() { Modify = Modify.HealingPower, Value = 24 } } },//Wrong ID
        };
        public static Enchant[] HandEnchants = new Enchant[]
        {
            new(){ID = "46514", Name = "Major Spellpower", EquipmentSlot = EquipmentSlot.Hands, Effects = new Effect[]{ new() { Modify = Modify.SpellPower, Value = 20} } },
            new(){ID = "46516", Name = "Spell Strike" , EquipmentSlot = EquipmentSlot.Hands, Effects = new Effect[]{ new() { Modify = Modify.SpellHitRating, Value = 15} } },
            new(){ID = "46512", Name = "Blasting", EquipmentSlot = EquipmentSlot.Hands, Effects = new Effect[]{ new() { Modify = Modify.SpellCritRating, Value = 10} } },
            new(){ID = "32399", Name = "Magister's Armor Kit", EquipmentSlot = EquipmentSlot.Hands, Effects = new Effect[]{ new() { Modify = Modify.MP5Flat, Value = 3} } },
        };
        public static Enchant[] LegEnchants = new Enchant[]
        {
            new(){ID = "31372", Name = "Runic Spellthread", EquipmentSlot = EquipmentSlot.Legs, Effects = new Effect[]{ new() { Modify = Modify.SpellPower, Value = 35} } },
            new(){ID = "31371", Name = "Mystic Spellthread", EquipmentSlot = EquipmentSlot.Legs, Effects = new Effect[]{ new() { Modify = Modify.SpellPower, Value = 25} } },
            new(){ID = "32399", Name = "Magister's Armor Kit", EquipmentSlot = EquipmentSlot.Legs, Effects = new Effect[]{ new() { Modify = Modify.MP5Flat, Value = 3} } },
            new(){ID = "24165", Name = "Hoodoo Hex", EquipmentSlot = EquipmentSlot.Legs, Effects = new Effect[]{ new() { Modify = Modify.SpellPower, Value = 18} } },
        };
        public static Enchant[] FeetEnchants = new Enchant[]
        {
            new(){ID = "32399", Name = "Boar's Speed", EquipmentSlot = EquipmentSlot.Feet, Effects = new Effect[]{ new() { Modify = Modify.Unique, Value = 8} } },
            new(){ID = "32399", Name = "Magister's Armor Kit", EquipmentSlot = EquipmentSlot.Feet, Effects = new Effect[]{ new() { Modify = Modify.MP5Flat, Value = 3} } },
        };
        public static Enchant[] RingEnchants = new Enchant[]
        {
            new(){ID = "46518", Name = "Spellpower", EquipmentSlot = EquipmentSlot.Feet, Effects = new Effect[]{ new() { Modify = Modify.SpellPower, Value = 12} } },
            new(){ID = "46519", Name = "Stats", EquipmentSlot = EquipmentSlot.Feet, Effects = new Effect[]{ new() { Modify = Modify.IntellectFlat, Value = 4} } },
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
            while (jsonString.Contains('\n'))
            {
                jsonString = jsonString.Remove(jsonString.IndexOf('\n'));
            }
            while (jsonString.Contains('\r'))
            {
                jsonString = jsonString.Remove(jsonString.IndexOf('\r'));
            }
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
        [JsonIgnore]
        public IEnumerable<Gem> Gems
        {
            get
            {
                if (Gem1 is not null) yield return Gem1;
                if (Gem2 is not null) yield return Gem2;
                if (Gem3 is not null) yield return Gem3;
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

        [JsonIgnore]
        public Enchant Enchant { get; private set; }

        public void SocketGem(Gem gem, int gemslot)
        {
            switch (gemslot)
            {
                case 1:
                    if (Socket1 != SocketColor.Dummy)
                    {
                        if (Socket1 != SocketColor.Meta && gem.Color != "Meta")
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

        public void AddEnchant(Enchant enchant)
        {
            switch (enchant.EquipmentSlot)
            {
                case EquipmentSlot.Head:
                    if (InvSlot == "Head") Enchant = enchant;
                    break;
                case EquipmentSlot.Neck:
                    if (InvSlot == "Neck") Enchant = enchant;
                    break;
                case EquipmentSlot.Shoulders:
                    if (InvSlot == "Shoulders") Enchant = enchant;
                    break;
                case EquipmentSlot.Back:
                    if (InvSlot == "Back") Enchant = enchant;
                    break;
                case EquipmentSlot.Chest:
                    if (InvSlot == "Chest") Enchant = enchant;
                    break;
                case EquipmentSlot.Wrist:
                    if (InvSlot == "Wrist") Enchant = enchant;
                    break;
                case EquipmentSlot.Hands:
                    if (InvSlot == "Hands") Enchant = enchant;
                    break;
                case EquipmentSlot.Waist:
                    if (InvSlot == "Waist") Enchant = enchant;
                    break;
                case EquipmentSlot.Legs:
                    if (InvSlot == "Legs") Enchant = enchant;
                    break;
                case EquipmentSlot.Feet:
                    if (InvSlot == "Feet") Enchant = enchant;
                    break;
                case EquipmentSlot.Finger:
                    if (InvSlot.Contains("Finger")) Enchant = enchant;
                    break;
                case EquipmentSlot.Trinket:
                    if (InvSlot.Contains("Trinket")) Enchant = enchant;
                    break;
                case EquipmentSlot.Mainhand:
                    if (InvSlot == "Two-Hand" || InvSlot == "Main Hand" || InvSlot == "One-Hand") Enchant = enchant;
                    break;
                case EquipmentSlot.Offhand:
                    if (InvSlot == "Offhand") Enchant = enchant;
                    break;
                case EquipmentSlot.Ranged:
                    if (InvSlot == "Ranged") Enchant = enchant;
                    break;
                default:
                    break;
            }
        }
    }

    public class Enchant
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public EquipmentSlot EquipmentSlot { get; set; }
        public Effect[] Effects { get; set; }

    }

    public enum EquipmentSlot
    {
        Head,
        Neck,
        Shoulders,
        Back,
        Chest,
        Wrist,
        Hands,
        Waist,
        Legs,
        Feet,
        Finger,
        Trinket,
        Mainhand,
        Offhand,
        Ranged
    }

    public class Spell : ICloneable
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
        public object Clone()
        {
            return this.MemberwiseClone();
        }

        private double ParseFromSecondsToMiliSecDouble(string str)
        {
            var decimals = string.Empty;
            var integers = string.Empty;
            if (str.Contains(','))
                integers = str.Substring(0, str.IndexOf(','));
            if (str.Contains('.'))
                integers = str.Substring(0, str.IndexOf('.'));

            if (str.Contains(','))
                decimals = str.Substring(str.IndexOf(',') + 1);
            if (str.Contains('.'))
                decimals = str.Substring(str.IndexOf('.') + 1);

            while (decimals.Length < 3) decimals = decimals + '0';
            while (decimals.Length > 3) decimals = decimals.Remove(decimals.Length - 1);


            return double.Parse(integers + decimals);
        }

        private double ParseFromMinutesToMiliSecDouble(string str)
        {
            var decimals = string.Empty;
            var integers = string.Empty;
            if (str.Contains(','))
                integers = str.Substring(0, str.IndexOf(','));
            if (str.Contains('.'))
                integers = str.Substring(0, str.IndexOf('.'));

            if (str.Contains(','))
                decimals = str.Substring(str.IndexOf(',') + 1);
            if (str.Contains('.'))
                decimals = str.Substring(str.IndexOf('.') + 1);

            while (decimals.Length < 3) decimals = decimals + '0';
            while (decimals.Length > 3) decimals = decimals.Remove(decimals.Length - 1);


            return double.Parse(integers + decimals) * 60;
        }

        private double ParseFromHoursToMiliSecDouble(string str)
        {
            var decimals = string.Empty;
            var integers = string.Empty;
            if (str.Contains(','))
                integers = str.Substring(0, str.IndexOf(','));
            if (str.Contains('.'))
                integers = str.Substring(0, str.IndexOf('.'));

            if (str.Contains(','))
                decimals = str.Substring(str.IndexOf(',') + 1);
            if (str.Contains('.'))
                decimals = str.Substring(str.IndexOf('.') + 1);

            while (decimals.Length < 3) decimals = decimals + '0';
            while (decimals.Length > 3) decimals = decimals.Remove(decimals.Length - 1);


            return double.Parse(integers + decimals) * 60 * 60;
        }
    }

    public class Aura
    {
        public string SpellID { get; set; }
        public string Name { get; set; }
        public AuraType AuraType { get; set; }
        public Effect[] Effects { get; set; }
        public double Duration { get; set; }
        public double EndTimer { get; set; }
        public double NextTick { get; set; }

        public Aura(string spellID, string name, AuraType auraType)
        {
            SpellID = spellID;
            Name = name;
            AuraType = auraType;
            Effects = new Effect[0];
        }
    }

    public class Talent
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public Effect[] Effects { get; set; }
    }

    public class Effect
    {
        public Modify Modify { get; set; }
        public int Value { get; set; }
        public string[] AffectedSpells { get; set; } = new[] { "All" };
        public double InternalCD { get; set; }
        public double ProcChance { get; set; }
        public double LastProcTimeStamp { get; set; }
        public string AuraID { get; set; }
    }

    public enum Modify
    {
        Casttime,
        CasttimePercent,
        DamagePercent,
        DamageFlat,
        SpellPowerPercent,
        PeriodicDamagePercent,
        SpellHitChance,
        SpellHitRating,
        SpellCritRating,
        CritRating,
        SpellCritChance,
        CritChance,
        HitChance,
        CritDamagePercent,
        ManaPercent,
        ProcOnHit,
        PeriodicProc,
        Cooldown,
        LearnSpell,
        Unique,
        SpellEffectiveness,
        MaxMana,
        AuraDuration,
        SpellHasteRating,
        SpellHastePercent,
        HolyPower,
        HolyPercent,
        ShadowPower,
        ShadowPercent,
        ArcanePower,
        ArcanePercent,
        FirePower,
        FirePercent,
        NaturePower,
        NaturePercent,
        FrostPower,
        FrostPercent,
        HealingPower,
        SpellPower,
        ManaReturnFlat,
        ManaReturnPercentOfDamage,
        ManaReturnPercenttOfMaxMana,
        ManaReturnPercentOfBaseMana,
        IntellectPercent,
        IntellectFlat,
        MP5Flat,
        MP5Percent,
        SpiritFlat,
        SpirtPercent,
        HealFlatOnDirectDamage,
        HealFlatOnPeriodicDamage,
        HealFlatOnDamage,
        HealPercentOffDamageOnDirectDamage,
        HealPercentOffDamageOnPeriodicDamage,
        HealPercentOffDamageOnDamage
    }
    public enum AuraType
    {
        Buff, Debuff
    }

    public class SocketBonus
    {
        public string ID { get; set; }
        public Modify Stat { get; set; }
        public int Amount { get; set; }
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

    public class SetBonus
    {
        public int ID { get; set; }
        public Effect TwoSet { get; set; }
        public Effect ThreeSet { get; set; }
        public Effect FourSet { get; set; }
    }
}
