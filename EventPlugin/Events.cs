using Smod2;
using Smod2.API;
using Smod2.Attributes;
using Smod2.Config;
using Smod2.Events;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.IO;
using System.Threading;
using System;
using Smod2.Commands;

namespace ATTG3
{
    public class Events
    {
        readonly ATTG3Plugin plugin;
        Server Server => PluginManager.Manager.Server;
        IConfigFile Config => ConfigManager.Manager.Config;
        public Events(ATTG3Plugin plugin) => this.plugin = plugin;
        public static IEnumerator<float> Invrandgive(Player player)
        {

            // bug Gives ran item
            int RandomInt = new System.Random().Next(ATTG3Plugin.Randoimitem.Count);
            if (ATTG3Plugin.Randoimitem[RandomInt] == "sniper")
            {
                yield return MEC.Timing.WaitForSeconds(2);
                GameObject sniper = (GameObject)player.GetGameObject();
                Inventory sniperinv = sniper.GetComponent<Inventory>();
                WeaponManager manager = sniper.GetComponent<WeaponManager>();
                int i = WeaponManagerIndex(manager, 20);
                sniperinv.AddNewItem(20, manager.weapons[i].maxAmmo, 4, 3, 1);
            }
            else if (ATTG3Plugin.Randoimitem[RandomInt] == "heavy")
            {
                yield return MEC.Timing.WaitForSeconds(1);
                player.GiveItem(ItemType.LOGICER);
            }
            else if (ATTG3Plugin.Randoimitem[RandomInt] == "grenade")
            {
                yield return MEC.Timing.WaitForSeconds(1);
                player.GiveItem(ItemType.FRAG_GRENADE);
                player.GiveItem(ItemType.FRAG_GRENADE);
                player.GiveItem(ItemType.FRAG_GRENADE);
                player.GiveItem(ItemType.FRAG_GRENADE);
                player.GiveItem(ItemType.FRAG_GRENADE);
                player.GiveItem(ItemType.FRAG_GRENADE);
            }
        }
        public static IEnumerator<float> MTFCIRESPAWN(Player player, Player Attacker)
        {

            foreach (Smod2.API.Item item in player.GetInventory())
            {
                if (item.ItemType == ItemType.FRAG_GRENADE)
                {
                    player.ThrowGrenade(GrenadeType.FRAG_GRENADE, false, Attacker.GetPosition(), true, player.GetPosition(), true, 0f, false);
                }
                item.Remove();
            }
            if (player.TeamRole.Role == Smod2.API.Role.CHAOS_INSURGENCY)
            {
                MTFCI.MTFKill++;
                PluginManager.Manager.Server.Map.ClearBroadcasts();
                PluginManager.Manager.Server.Map.Broadcast(5, "<color=#0080FF>MTF Has " + MTFCI.MTFKill + " Kills out of " + MTFCI.KillGoal + "</Color> <color=#0B7A00>CI Has " + MTFCI.CIKills + " Kills out of " + MTFCI.KillGoal + "</Color>", false);
                yield return MEC.Timing.WaitForSeconds(10);
                player.ChangeRole(Role.CHAOS_INSURGENCY, true, true, false, true);
            }
            else if (player.TeamRole.Role == Smod2.API.Role.NTF_COMMANDER)
            {
                MTFCI.CIKills++;
                PluginManager.Manager.Server.Map.ClearBroadcasts();
                PluginManager.Manager.Server.Map.Broadcast(5, "<color=#0080FF>MTF Has " + MTFCI.MTFKill + " Kills out of " + MTFCI.KillGoal + "</Color> <color=#0B7A00>CI Has " + MTFCI.CIKills + " Kills out of " + MTFCI.KillGoal + "</Color>", false);
                yield return MEC.Timing.WaitForSeconds(10);
                player.ChangeRole(Role.NTF_COMMANDER, true, true, false, true);
            }
        }
        public static IEnumerator<float> GiveAmmo(Player player)
        {
            yield return MEC.Timing.WaitForSeconds(1f);
            player.SetAmmo(AmmoType.DROPPED_5, 1000);
            player.SetAmmo(AmmoType.DROPPED_7, 1000);
            player.SetAmmo(AmmoType.DROPPED_9, 1000);
        }
        public static int TUTCOUNT(Role role)
        {
            int rolecount = 0;
            foreach (Player player in PluginManager.Manager.Server.GetPlayers())
            {
                if (player.TeamRole.Role == role)
                {
                    rolecount++;
                }
            }
            return rolecount;
        }
        public static void Inventoryset(Player player, int invpos)
        {
            List<Smod2.API.Item> inventory = player.GetInventory();
            #region Itemids
            ItemType inv1 = inventory[invpos].ItemType;
            if (inv1 == ItemType.JANITOR_KEYCARD)
            {
                ATTG3Plugin.itemid = 0;
            }
            else if (inv1 == ItemType.SCIENTIST_KEYCARD)
            {
                ATTG3Plugin.itemid = 1;
            }
            else if (inv1 == ItemType.MAJOR_SCIENTIST_KEYCARD)
            {
                ATTG3Plugin.itemid = 2;
            }
            else if (inv1 == ItemType.ZONE_MANAGER_KEYCARD)
            {
                ATTG3Plugin.itemid = 3;
            }
            else if (inv1 == ItemType.GUARD_KEYCARD)
            {
                ATTG3Plugin.itemid = 4;
            }
            else if (inv1 == ItemType.SENIOR_GUARD_KEYCARD)
            {
                ATTG3Plugin.itemid = 5;
            }
            else if (inv1 == ItemType.CONTAINMENT_ENGINEER_KEYCARD)
            {
                ATTG3Plugin.itemid = 6;
            }
            else if (inv1 == ItemType.MTF_LIEUTENANT_KEYCARD)
            {
                ATTG3Plugin.itemid = 7;
            }
            else if (inv1 == ItemType.MTF_COMMANDER_KEYCARD)
            {
                ATTG3Plugin.itemid = 8;
            }
            else if (inv1 == ItemType.FACILITY_MANAGER_KEYCARD)
            {
                ATTG3Plugin.itemid = 9;
            }
            else if (inv1 == ItemType.CHAOS_INSURGENCY_DEVICE)
            {
                ATTG3Plugin.itemid = 10;
            }
            else if (inv1 == ItemType.O5_LEVEL_KEYCARD)
            {
                ATTG3Plugin.itemid = 11;
            }
            else if (inv1 == ItemType.RADIO)
            {
                ATTG3Plugin.itemid = 12;
            }
            else if (inv1 == ItemType.COM15)
            {
                ATTG3Plugin.itemid = 13;
            }
            else if (inv1 == ItemType.MEDKIT)
            {
                ATTG3Plugin.itemid = 14;
            }
            else if (inv1 == ItemType.FLASHLIGHT)
            {
                ATTG3Plugin.itemid = 15;
            }
            else if (inv1 == ItemType.MICROHID)
            {
                ATTG3Plugin.itemid = 16;
            }
            else if (inv1 == ItemType.COIN)
            {
                ATTG3Plugin.itemid = 17;
            }
            else if (inv1 == ItemType.CUP)
            {
                ATTG3Plugin.itemid = 18;
            }
            else if (inv1 == ItemType.WEAPON_MANAGER_TABLET)
            {
                ATTG3Plugin.itemid = 19;
            }
            else if (inv1 == ItemType.E11_STANDARD_RIFLE)
            {
                ATTG3Plugin.itemid = 20;
            }
            else if (inv1 == ItemType.P90)
            {
                ATTG3Plugin.itemid = 21;
            }
            else if (inv1 == ItemType.DROPPED_5)
            {
                ATTG3Plugin.itemid = 22;
            }
            else if (inv1 == ItemType.MP4)
            {
                ATTG3Plugin.itemid = 23;
            }
            else if (inv1 == ItemType.LOGICER)
            {
                ATTG3Plugin.itemid = 24;
            }
            else if (inv1 == ItemType.FRAG_GRENADE)
            {
                ATTG3Plugin.itemid = 25;
            }
            else if (inv1 == ItemType.FLASHBANG)
            {
                ATTG3Plugin.itemid = 26;
            }
            else if (inv1 == ItemType.DISARMER)
            {
                ATTG3Plugin.itemid = 27;
            }
            else if (inv1 == ItemType.DROPPED_7)
            {
                ATTG3Plugin.itemid = 28;
            }
            else if (inv1 == ItemType.DROPPED_9)
            {
                ATTG3Plugin.itemid = 29;
            }
            else if (inv1 == ItemType.USP)
            {
                ATTG3Plugin.itemid = 30;
            }
            else if (inv1 == ItemType.NULL)
            {
                ATTG3Plugin.itemid = -1;
            }
            #endregion
            GameObject playerobject = (GameObject)player.GetGameObject();
            Inventory plainv = playerobject.GetComponent<Inventory>();
            plainv.SetCurItem(ATTG3Plugin.itemid);
        }
        #region Roomstuff
        public static void Noroom()
        {
            #region Noroom
            foreach (Room room in PluginManager.Manager.Server.Map.Get079InteractionRooms(Scp079InteractionType.CAMERA).Where(x => x.ZoneType == ZoneType.LCZ).ToArray())
            {
                if (room.RoomType == RoomType.AIRLOCK_00)
                {
                    Vector posvect = room.Position;
                    Vector3 posvec3 = new Vector3(posvect.x, posvect.y, posvect.z);
                    ATTG3Plugin.NoRooms.Add(posvec3);
                }
                else if (room.RoomType == RoomType.ENTRANCE_CHECKPOINT)
                {
                    Vector posvect = room.Position;
                    Vector3 posvec3 = new Vector3(posvect.x, posvect.y, posvect.z);
                    ATTG3Plugin.NoRooms.Add(posvec3);
                }
                else if (room.RoomType == RoomType.CHECKPOINT_B)
                {
                    Vector posvect = room.Position;
                    Vector3 posvec3 = new Vector3(posvect.x, posvect.y, posvect.z);
                    ATTG3Plugin.NoRooms.Add(posvec3);
                }
                else if (room.RoomType == RoomType.CHECKPOINT_A)
                {
                    Vector posvect = room.Position;
                    Vector3 posvec3 = new Vector3(posvect.x, posvect.y, posvect.z);
                    ATTG3Plugin.NoRooms.Add(posvec3);
                }
                else if (room.RoomType == RoomType.GATE_A)
                {
                    Vector posvect = room.Position;
                    Vector3 posvec3 = new Vector3(posvect.x, posvect.y, posvect.z);
                    ATTG3Plugin.NoRooms.Add(posvec3);
                }
                else if (room.RoomType == RoomType.GATE_B)
                {
                    Vector posvect = room.Position;
                    Vector3 posvec3 = new Vector3(posvect.x, posvect.y, posvect.z);
                    ATTG3Plugin.NoRooms.Add(posvec3);
                }
                else if (room.RoomType == RoomType.NUKE)
                {
                    Vector posvect = room.Position;
                    Vector3 posvec3 = new Vector3(posvect.x, posvect.y, posvect.z);
                    ATTG3Plugin.NoRooms.Add(posvec3);
                }
                else if (room.RoomType == RoomType.MICROHID)
                {
                    Vector posvect = room.Position;
                    Vector3 posvec3 = new Vector3(posvect.x, posvect.y, posvect.z);
                    ATTG3Plugin.NoRooms.Add(posvec3);
                }
                else if (room.RoomType == RoomType.UNDEFINED)
                {
                    Vector posvect = room.Position;
                    Vector3 posvec3 = new Vector3(posvect.x, posvect.y, posvect.z);
                    ATTG3Plugin.NoRooms.Add(posvec3);
                }
                else if (room.RoomType == RoomType.SCP_914)
                {
                    Vector posvect = room.Position;
                    Vector3 posvec3 = new Vector3(posvect.x, posvect.y, posvect.z);
                    ATTG3Plugin.NoRooms.Add(posvec3);
                }
                else if (room.RoomType == RoomType.SCP_106)
                {
                    Vector posvect = room.Position;
                    Vector3 posvec3 = new Vector3(posvect.x, posvect.y, posvect.z);
                    ATTG3Plugin.NoRooms.Add(posvec3);
                }
            }
            #endregion
            Events.RemoveNoRooms();
        }
        public static void GetRoundStartRoom()
        {
            GameObject[] array = GameObject.FindGameObjectsWithTag("RoomID");
            foreach (GameObject val2 in array)
            {
                if (val2.GetComponent<Rid>() != null)
                {
                    ATTG3Plugin.NoRoomTP.Add(val2.transform.position);
                }
            }
            GameObject[] array1 = GameObject.FindGameObjectsWithTag("RoomID");
            foreach (GameObject val2 in array1)
            {
                if (val2.GetComponent<Rid>() != null)
                {
                    ATTG3Plugin.TPRooms.Add(val2.transform.position);
                }
            }
            Events.Noroom();
        }
        public static void RemoveNoRooms()
        {
            foreach (Vector3 vector3 in ATTG3Plugin.NoRoomTP)
            {
                if (ATTG3Plugin.NoRooms.Contains(vector3))
                {
                    ATTG3Plugin.NoRoomTP.Remove(vector3);
                }
            }
        }
        public static void RemoveRandRoomTP(Player player)
        {
            Vector3 val4 = ATTG3Plugin.NoRoomTP[UnityEngine.Random.Range(0, ATTG3Plugin.NoRoomTP.Count)];
            val4.y += 2f;
            Vector TORoom = new Vector(val4.x, val4.y, val4.z);
            player.Teleport(TORoom, true);
            ATTG3Plugin.NoRoomTP.Remove(val4);
        }
        public static void FullRandRoomTP(Player player)
        {
            Vector3 val4 = ATTG3Plugin.TPRooms[UnityEngine.Random.Range(0, ATTG3Plugin.TPRooms.Count)];
            val4.y += 2f;
            Vector TORoom = new Vector(val4.x, val4.y, val4.z);
            player.Teleport(TORoom, true);
        }
        #endregion
        public static int WeaponManagerIndex(WeaponManager manager, int item)
        {
            // Get weapon index in WeaponManager
            int weapon = -1;
            for (int i = 0; i < manager.weapons.Length; i++)
            {
                if (manager.weapons[i].inventoryID == item)
                {
                    weapon = i;
                }
            }
            return weapon;
        }
        public static IEnumerator<float> CustomitemDoor(Smod2.API.Door door, string setting, ItemType item, Player player)
        {
            if (door.Locked == false)
            {
                ATTG3Plugin.Instance.Info("Door is not locked");
                if (item == ItemType.JANITOR_KEYCARD)
                {
                    ATTG3Plugin.Instance.Info("Item is Janitor Keycard");
                    if (setting == "10Lock")
                    {
                        ATTG3Plugin.Instance.Info("10lock");
                        yield return MEC.Timing.WaitForSeconds(1);
                        door.Locked = true;
                        door.Open = false;
                        yield return MEC.Timing.WaitForSeconds(10);
                        door.Locked = false;
                        door.Open = false;
                    }
                    else if (setting == "destroy")
                    {
                        ATTG3Plugin.Instance.Info("destroy");
                        yield return MEC.Timing.WaitForSeconds(1);
                        door.Destroyed = true;
                    }
                    else if (setting == "30Lock")
                    {
                        ATTG3Plugin.Instance.Info("30lock");
                        yield return MEC.Timing.WaitForSeconds(1);
                        door.Locked = true;
                        door.Open = false;
                        foreach (Smod2.API.Item iteminv in player.GetInventory().Where(i => i.ItemType != ItemType.NULL))
                        {
                            if (iteminv.ItemType == item)
                            {
                                iteminv.Remove();
                            }
                        }
                        yield return MEC.Timing.WaitForSeconds(60);
                        door.Locked = false;
                        door.Open = false;

                    }
                }
                // Add More Cards here
            }
        }
        public static void SCPMTF()
        {
            foreach (Player player in PluginManager.Manager.Server.GetPlayers())
            {
                if (player.TeamRole.Team == Smod2.API.Team.SCP)
                {
                    if (player.GetPosition().y >= 900)
                    {
                        player.Teleport(PluginManager.Manager.Server.Map.GetRandomSpawnPoint(player.TeamRole.Role));
                        player.PersonalBroadcast(5, "You were teleported to your spawn for spawn camping", false);
                    }
                }
            }
        }
        public static IEnumerator<float> RespawnSpawn(Player player, string eventnum)
        {
            if (eventnum == "infectcon")
            {
                yield return MEC.Timing.WaitForSeconds(30);
                player.ChangeRole(Role.NTF_COMMANDER, true, true, true, true);
            }
            else if (eventnum == "infect")
            {
                yield return MEC.Timing.WaitForSeconds(30);
                player.ChangeRole(Role.SCP_049_2, true, true, true, true);
                player.Teleport(PluginManager.Manager.Server.Map.GetRandomSpawnPoint(Role.SCP_049), true);
            }
        }
        #region Map/file Stuff
        public static void Setfile(string text)
        {
            
            File.AppendAllText(ATTG3Plugin.EventSpawn, text.Trim('(', ')', ' '));
        }
        public static void CheckMap(string text)
        {
            File.AppendAllText(ATTG3Plugin.Mapseeds, text);
        }
        public static void MapSpawnVec()
        {
            if (ATTG3Plugin.MapCusSpawn.Count > 0)
                ATTG3Plugin.MapCusSpawn.Clear();
            

            List<string> MapSpaVec = new List<string>();
            MapSpaVec = File.ReadAllLines(ATTG3Plugin.EventSpawn).ToList();
            if (MapSpaVec.Count() > 0)
            {
                GameObject val = GameObject.Find("Host");
                int num = -1;
                if (val != null)
                {
                    num = val.GetComponent<RandomSeedSync>().seed;
                }
                ATTG3Plugin.Instance.Info("Checking map Seeds and Custom Vectors");

                foreach (string spawnvec in MapSpaVec)
                {
                    if (spawnvec.Length > 0)
                    {
                        if (num == Int32.Parse(spawnvec.Split(':')[0]))
                        {
                            
                            ATTG3Plugin.Instance.Info("Cheking Vectors");
                            string line = spawnvec.Split(':')[0];
                            string line1 = spawnvec.Split(':')[1];
                            //ATTG3Plugin.Instance.Info(line);
                            float x = float.Parse(line1.Split(',')[0]);
                            float y = float.Parse(line1.Split(',')[1]);
                            float z = float.Parse(line1.Split(',')[2]);
                            //ATTG3Plugin.Instance.Info(x.ToString());
                            //ATTG3Plugin.Instance.Info(y.ToString());
                            //ATTG3Plugin.Instance.Info(z.ToString());
                            Vector Posspawn = new Vector(x, y, z);
                            ATTG3Plugin.MapCusSpawn.Add(Posspawn);
                            
                        }
                        /*else
                        {
                            ATTG3Plugin.Instance.Info("Error Map is not a match for vector ");
                        }*/
                    }
                }
                ATTG3Plugin.Instance.Info("Done Checking map Seeds and Custom Vectors");
            }
        }
       // public static void customfile()
        //{
         //   public static string MapSpawns = FileManager.GetAppFolder() + "ATTG" + Path.DirectorySeparatorChar + "MapSpawns" + Path.DirectorySeparatorChar + +".txt";
        //} 
        #endregion
    }
}
