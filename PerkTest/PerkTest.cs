using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using InfinityScript;

namespace RollTheDice
{
    public class RollTheDice : BaseScript
    {
        public const int NumOfRolls = 40;
        public List<string> PlayerStop = new List<string>();
        public int tickcount = 0;
        public RollTheDice()
        {
            PlayerConnected += RollTheDice_PlayerConnected;
            GSCFunctions.SetDvar("g_deadchat", 1);
            GSCFunctions.SetDvar("painVisionTriggerHealth", 0);
            Tick += RollTheDice_Tick;
        }

        void RollTheDice_Tick()
        {
            tickcount++;
            if (tickcount % 10 == 0)
            {
                tickcount = 0;
            }
        }

        void RollTheDice_PlayerConnected(Entity obj)
        {
            obj.SpawnedPlayer += () => OnPlayerSpawned(obj);
            obj.OnNotify("disconnect", entity => PlayerStop.Add(obj.Name));
        }

        public override void OnPlayerKilled(Entity player, Entity inflictor, Entity attacker, int damage, string mod, string weapon, Vector3 dir, string hitLoc)
        {
            PlayerStop.Add(player.Name);
        }

        public void OnPlayerSpawned(Entity player)
        {
            if (PlayerStop.Contains(player.Name))
                PlayerStop.Remove(player.Name);
            ResetPlayer(player);
            player.ClearPerks();
            //player.Call(@"maps\mp\gametypes\_class::setKillstreaks", "none", "none", "none");
            DoRandom(player, null);
        }

        public void DoRandom(Entity player, int? desiredNumber)
        {
            int? roll = new Random().Next(NumOfRolls);
            if (desiredNumber != null)
                roll = desiredNumber;
            var rollname = "";
            switch (roll)
            {
                case 0:
                    rollname = "Nothing";
                    player.SetPerk("specialty_null", true, true);
                    AfterDelay(2000, () =>
                        player.IPrintLnBold(string.Format("It does nothing...")));
                    break;
                case 1:
                    rollname = "Eavesdrop";
                    player.SetPerk("specialty_parabolic", true, true);
                    AfterDelay(2000, () =>
                        player.IPrintLnBold(string.Format("Eavesdrop lets you hear enemy voicechat.")));
                    break;
                case 2:
                    rollname = "GPS Jammer";
                    player.SetPerk("specialty_gpsjammer", true, true);
                    AfterDelay(2000, () =>
                        player.IPrintLnBold(string.Format("GPS Jammer keeps you off the enemy radar.")));
                    break;
                case 3:
                    rollname = "Marksman Pro";
                    player.SetPerk("specialty_holdbreath", true, true);
                    player.ShowHudSplash("specialty_autospot_ks_pro", 0);
                    AfterDelay(2000, () =>
    player.IPrintLnBold(string.Format("Marksman Pro lets you hold your breath longer.")));
                    break;
                case 4:
                    rollname = "Dead Silence";
                    player.SetPerk("specialty_quieter", true, true);
                    player.ShowHudSplash("specialty_quieter_ks", 0);
                    AfterDelay(2000, () =>
    player.IPrintLnBold(string.Format("Dead Silence makes your footsteps silent.")));
                    break;
                case 5:
                    rollname = "Sitrep";
                    player.SetPerk("specialty_detectexplosive", true, true);
                    player.SetPerk("specialty_sitrep", true, true);
                    player.SetPerk("specialty_sabotuer", true, true);
                    player.ShowHudSplash("specialty_detectexplosive_ks", 0);
                    AfterDelay(2000, () =>
    player.IPrintLnBold(string.Format("Sitrep lets you see enemy explosives.")));
                    break;
                case 6:
                    rollname = "Explosive Bullets";
                    player.SetPerk("specialty_explosivebullets", true, true);
                    player.ShowHudSplash("explosive_ammo", 0);
                    AfterDelay(2000, () =>
    player.IPrintLnBold(string.Format("Explosive Bullets simply gives you explosive bullets.")));
                    break;
                case 7:
                    rollname = "Steady Aim";
                    player.SetPerk("specialty_bulletaccuracy", true, true);
                    player.ShowHudSplash("specialty_bulletaccuracy_ks", 0);
                    AfterDelay(2000, () =>
    player.IPrintLnBold(string.Format("Steady Aim makes your hipfire more accurate.")));
                    break;
                case 8:
                    rollname = "Double Tap";
                    player.SetPerk("specialty_rof", true, true);
                    AfterDelay(2000, () =>
    player.IPrintLnBold(string.Format("Double Tap increases your rate of fire.")));
                    break;
                case 9:
                    rollname = "Sleight of Hand";
                    player.SetPerk("specialty_fastreload", true, true);
                    player.ShowHudSplash("specialty_fastreload_ks", 0);
                    AfterDelay(2000, () =>
    player.IPrintLnBold(string.Format("Sleight of Hand lets you reload faster.")));
                    break;
                case 10:
                    rollname = "Shield";
                    player.SetPerk("specialty_shield", true, true);
                    player.ShowHudSplash("_specialty_blastshield_ks", 0);
                    AfterDelay(150, () =>
                            player.AttachShieldModel("weapon_riot_shield_mp", "tag_shield_back"));
                    AfterDelay(2000, () =>
    player.IPrintLnBold(string.Format("Shield gives you a Riot Shield to block your back.")));
                    break;
                case 11:
                    rollname = "Jumpdive";
                    player.SetPerk("specialty_jumpdive", true, true);
                    AfterDelay(2000, () =>
    player.IPrintLnBold(string.Format("Jumpdive lets your Dolphin Dive by jumping.")));
                    break;
                case 12:
                    rollname = "Scrambler";
                    player.SetPerk("specialty_localjammer", true, true);
                    AfterDelay(2000, () =>
    player.IPrintLnBold(string.Format("Scrambler scrambles nearby enemies radar.")));
                    break;
                case 13:
                    rollname = "Extreme Conditioning Pro";
                    player.SetPerk("specialty_fastmantle", true, true);
                    player.ShowHudSplash("specialty_longersprint_ks_pro", 0);
                    AfterDelay(2000, () =>
    player.IPrintLnBold(string.Format("Extreme Conditioning Pro lets you climb walls faster.")));
                    break;
                case 14:
                    rollname = "Lightweight";
                    player.SetPerk("specialty_lightweight", true, true);
                    AfterDelay(2000, () =>
    player.IPrintLnBold(string.Format("Lightweight lets you run faster.")));
                    break;
                case 15:
                    rollname = "Quickdraw";
                    player.SetPerk("specialty_quickdraw", true, true);
                    player.SetPerk("specialty_fastsnipe", true, true);
                    player.ShowHudSplash("specialty_quickdraw_ks", 0);
                    AfterDelay(2000, () =>
    player.IPrintLnBold(string.Format("Quickdraw lets you aim faster.")));
                    break;
                case 16:
                    rollname = "Scavenger";
                    player.SetPerk("specialty_scavenger", true, true);
                    player.ShowHudSplash("specialty_scavenger_ks", 0);
                    AfterDelay(2000, () =>
    player.IPrintLnBold(string.Format("Scavenger lets you pickup extra ammo via Scavenger Packs.")));
                    break;
                case 17:
                    rollname = "Amplify";
                    player.SetPerk("specialty_amplify", true, true);
                    AfterDelay(2000, () =>
    player.IPrintLnBold(string.Format("Amplify makes enemy noise louder.")));
                    break;
                case 18:
                    rollname = "Extended Mags";
                    player.SetPerk("specialty_extendedmags", true, true);
                    player.ShowHudSplash("ammo", 0);
                    AfterDelay(2000, () =>
    player.IPrintLnBold(string.Format("Extended Mags adds extra bullets to your gun.")));
                    break;
                case 19:
                    rollname = "Marathon";
                    player.SetPerk("specialty_marathon", true, true);
                    player.ShowHudSplash("specialty_longersprint_ks", 0);
                    AfterDelay(2000, () =>
    player.IPrintLnBold(string.Format("Marathon lets you sprint forever.")));
                    break;
                case 20:
                    rollname = "Commando";
                    player.SetPerk("specialty_extendedmelee", true, true);
                    AfterDelay(2000, () =>
    player.IPrintLnBold(string.Format("Commando increases your Melee range.")));
                    break;
                case 21:
                    rollname = "Recon Pro";
                    player.SetPerk("specialty_paint_pro", true, true);
                    player.ShowHudSplash("specialty_paint_ks_pro", 0);
                    AfterDelay(2000, () =>
    player.IPrintLnBold(string.Format("Recon Pro lets you paint enemies with bullet damage.")));
                    break;
                case 22:
                    rollname = "Assassin Pro";
                    player.SetPerk("specialty_spygame", true, true);
                    player.ShowHudSplash("specialty_coldblooded_ks_pro", 0);
                    AfterDelay(2000, () =>
    player.IPrintLnBold(string.Format("Assassin Pro keeps the Red name and Red Crosshairs from showing to enemies.")));
                    break;
                case 23:
                    rollname = "Automantle";
                    player.SetPerk("specialty_automantle", true, true);
                    AfterDelay(2000, () =>
    player.IPrintLnBold(string.Format("Automantle lets you Automatically climb walls.")));
                    break;
                case 24:
                    rollname = "Last Stand";
                    player.SetPerk("specialty_pistoldeath", true, true);
                    player.SetPerk("specialty_laststandoffhand", true, true);
                    AfterDelay(2000, () =>
    player.IPrintLnBold(string.Format("Last Stand lets you drop to your pistol before you die.")));
                    break;
                case 25:
                    rollname = "Danger Close";
                    player.SetPerk("specialty_dangerclose", true, true);
                    player.SetPerk("specialty_explosivedamage", true, true);
                    player.ShowHudSplash("_specialty_blastshiled_ks", 0);
                    AfterDelay(2000, () =>
player.IPrintLnBold(string.Format("Danger Close increases your Explosive Damage and Range.")));
                    break;
                case 26:
                    rollname = "Black Box";
                    player.SetPerk("specialty_blackbox", true, true);
                    player.ShowHudSplash("helicopter_blackbox", 0);
                    AfterDelay(2000, () =>
player.IPrintLnBold(string.Format("Black Box increases the length your helicopters stay in the air.")));
                    break;
                case 27:
                    rollname = "Stalker";
                    player.SetPerk("specialty_stalker", true, true);
                    player.ShowHudSplash("specialty_stalker_ks", 0);
                    AfterDelay(2000, () =>
player.IPrintLnBold(string.Format("Stalker lets you walk faster while aiming.")));
                    break;
                case 28:
                    rollname = "Specialist Bonus!";
                    player.SetPerk("specialty_quieter", true, true);
                    player.SetPerk("specialty_longersprint", true, true);
                    player.SetPerk("specialty_detectexplosive", true, true);
                    player.SetPerk("specialty_bulletaccuracy", true, true);
                    player.SetPerk("specialty_quickswap", true, true);
                    player.SetPerk("specialty_fastreload", true, true);
                    player.SetPerk("specialty_extraammo", true, true);
                    player.SetPerk("specialty_falldamage", true, true);
                    player.SetPerk("specialty_delaymine", true, true);
                    player.SetPerk("specialty_fastmantle", true, true);
                    player.SetPerk("specialty_lightweight", true, true);
                    player.SetPerk("specialty_scavenger", true, true);
                    player.SetPerk("specialty_blindeye", true, true);
                    player.SetPerk("specialty_fasterlockon", true, true);
                    player.SetPerk("specialty_armorpiercing", true, true);
                    player.SetPerk("specialty_paint", true, true);
                    player.SetPerk("specialty_paint_pro", true, true);
                    player.SetPerk("specialty_hardline", true, true);
                    player.SetPerk("specialty_assists", true, true);
                    player.SetPerk("specialty_coldblooded", true, true);
                    player.SetPerk("specialty_spygame", true, true);
                    player.SetPerk("specialty_quickdraw", true, true);
                    player.SetPerk("specialty_fastoffhand", true, true);
                    player.SetPerk("_specialty_blastshield", true, true);
                    player.SetPerk("specialty_empimmune", true, true);
                    player.SetPerk("specialty_stun_resistance", true, true);
                    player.SetPerk("specialty_fastsprintrecovery", true, true);
                    player.SetPerk("specialty_selectivehearing", true, true);
                    player.SetPerk("specialty_autospot", true, true);
                    player.SetPerk("specialty_improvedholdbreath", true, true);
                    player.SetPerk("specialty_stalker", true, true);
                    player.SetPerk("specialty_bulletpenetration", true, true);
                    player.SetPerk("specialty_marksman", true, true);
                    player.SetPerk("specialty_sharp_focus", true, true);
                    player.SetPerk("specialty_holdbreathwhileads", true, true);
                    player.SetPerk("specialty_longerrange", true, true);
                    player.SetPerk("specialty_fastermelee", true, true);
                    player.SetPerk("specialty_reducedsway", true, true);
                    player.ShowHudSplash("all_perks_bonus", 0);
                    AfterDelay(2000, () =>
player.IPrintLnBold(string.Format("Specialist Bonus gives you All of the Perks!")));
                    break;
                case 29:
                    rollname = "Low Profile";
                    player.SetPerk("specialty_lowprofile", true, true);
                    AfterDelay(2000, () =>
player.IPrintLnBold(string.Format("Low Profile lets you run while crouched.")));
                    break;
                case 30:
                    rollname = "Throwback";
                    player.SetPerk("specialty_throwback", true, true);
                    AfterDelay(2000, () =>
player.IPrintLnBold(string.Format("Throwback lets you reset the fuse of a grenade you throw back.")));
                    break;
                case 31:
                    rollname = "Impact";
                    player.SetPerk("specialty_bulletpenetration", true, true);
                    AfterDelay(2000, () =>
player.IPrintLnBold(string.Format("Impact lets you shoot through surfaces easier.")));
                    break;
                case 32:
                    rollname = "Kick";
                    player.SetPerk("specialty_marksman", true, true);
                    AfterDelay(2000, () =>
player.IPrintLnBold(string.Format("Kick Reduces your gun Recoil.")));
                    break;
                case 33:
                    rollname = "Focus";
                    player.SetPerk("specialty_sharp_focus", true, true);
                    AfterDelay(2000, () =>
player.IPrintLnBold(string.Format("Focus reduces your players flinch when shot.")));
                    break;
                case 34:
                    rollname = "Breath";
                    player.SetPerk("specialty_holdbreathwhileads", true, true);
                    AfterDelay(2000, () =>
player.IPrintLnBold(string.Format("Breath lets you hold your breath on any weapon.")));
                    break;
                case 35:
                    rollname = "Range";
                    player.SetPerk("specialty_longerrange", true, true);
                    AfterDelay(2000, () =>
player.IPrintLnBold(string.Format("Range increases your Weapon's range.")));
                    break;
                case 36:
                    rollname = "Melee";
                    player.SetPerk("specialty_fastermelee", true, true);
                    AfterDelay(2000, () =>
player.IPrintLnBold(string.Format("Melee makes your Melee faster.")));
                    break;
                case 37:
                    rollname = "Stability";
                    player.SetPerk("specialty_reducedsway", true, true);
                    AfterDelay(2000, () =>
player.IPrintLnBold(string.Format("Stability stabilizes your gun sway.")));
                    break;
                case 38:
                    rollname = "Damage";
                    player.SetPerk("specialty_moredamage", true, true);
                    AfterDelay(2000, () =>
player.IPrintLnBold(string.Format("Damage increases your Weapon's damage.")));
                    break;
                case 39:
                    rollname = "Marksman";
                    player.SetPerk("specialty_autospot", true, true);
                    player.ShowHudSplash("specialty_autospot_ks", 0);
                    AfterDelay(2000, () =>
player.IPrintLnBold(string.Format("Marksman spots enemies from further ranges.")));
                    break;
            }
            PrintRollNames(player, rollname, 0, roll);
        }

        public void PrintRollNames(Entity player, string name, int index, int? roll)
        {
            HudElem elem = player.HasField("rtd_rolls") ? player.GetField<HudElem>("rtd_rolls") : HudElem.CreateFontString(player, HudElem.Fonts.HudBig, 0.6f);
            elem.SetPoint("RIGHT", "RIGHT", -90, 165 - ((index - 1) * 13));
            elem.SetText(string.Format("You Have {1}", roll + 1, name));
            player.SetField("rtd_rolls", new Parameter(elem));
            player.IPrintLnBold(string.Format("You Got {1}", roll + 1, name));
            GSCFunctions.IPrintLn(string.Format("{0} got {2}", player.Name, roll + 1, name));
        }

        public void ResetPlayer(Entity player)
        {
            player.SetMoveSpeedScale(1f);
        }

        public static string GetWeaponName(string name)
        {
            var parts = name.Split('_');
            return parts[0] == "iw5" ? parts[1] : parts[0];
        }
    }
}