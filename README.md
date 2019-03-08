# Development Paused


# Event-Admin-Plugin
An Smod plugin that adds Admin commands to SCP:SL

# Installation
**[Smod2](https://github.com/Grover-c13/Smod2) and [ItemManager](https://github.com/probe4aiur/ItemManager) must be installed for this to work.**

Place the "ATTG3.dll" file in your sm_plugins folder.
# Request Forms

[Command Request](https://goo.gl/forms/GW8Ic4UplluDDP592) 

[Item Request](https://goo.gl/forms/yMY8dRiiafXGqW3y2)

# Support

[Event Plugin Discord](https://discord.gg/8bjsvST)

[Smod Discord](https://discord.gg/nJRA2CT)

[SCP: Secret Laboratory Discord](https://discord.gg/scpsl )

[Trello Page](https://trello.com/b/YpKW1b8p/event-plugin)

# Items
| Item ID       | Description |
| :-------------: | :---------: | 
| ~~200~~ | ~~Taser~~ |
| ~~201~~ | ~~Grenade Launcher~~ |
| ~~207~~ | ~~Medkit that changes player into SCP-106~~ |
| ~~208~~ | ~~Medkit that changes player into SCP-049~~ |
| ~~209~~ | ~~Medkit that changes player into SCP-939~~ |
| ~~210~~ | ~~Medkit that changes player into SCP-173~~ |
| ~~211~~ | ~~Medkit that changes player into SCP-096~~ |
| ~~212~~ | ~~Medkit that changes player into SCP-049-2~~ |
| ~~213~~ | ~~Medkit that changes player into SCP-079~~ |
| ~~214~~ | ~~Medkit that teleports player back to where he picked up the item after 1 min~~ |

# Commands
| Command      | Parameters| Description |
| :-------------: | :---------: | :---------: | 
| AGTL | NONE | Turns on all Tesla |
| AGEL | NONE | Toggles Elevator locking |
| AGTLR | NONE | Toggles Tesla Gates |
| AGELR | Float | Changes elevator speed |
| AG106D | NONE | Toggles recontaining SCP-106 |
| AGS079 | NONE | SCP-079s generators time to 10 sec |
| AGP079 | NONE | Opens SCP-079 Generators |
| AGE079 | NONE | Ejects all tablets from generators |
| AGL079 | NONE | Levels up 079 and gives AP |
| AGVOTET | NONE | Toggles Voting  |
| AGVOTEC | NONE| Clears Vote |
| AGVOTES | NONE | Displays Vote |
| AGVOTEBC| NONE | Displays how to vote |
| ~~AGCIMTF~~ | enable / disable |~~Turns on CI VS MTF next round~~ |
| AGAMMO | Players name |Gives player 100000 ammo|
| AGUP | Players name | TPs player up from there position |
| ~~AGCITEM~~ | NONE | ~~Enables custom items~~ |
| AGDISABLE | NONE | Disables Event Plugin |
| AGSHAKE | NONE | Shakes screens of all players |


# Config


### Admin Config
| Config        | Value Type | Default | Description |
| :-------------: | :---------: | :---------: |:------ |
| attg_ranks | List | owner, coowner | Roles that can do all commands. |
| ~~attg_item_ranks~~ | ~~List~~ | ~~owner~~ | ~~Roles that can actavate the custom items.~~ |
| attg_vote_ranks | List | owner, coowner, admin | Roles that do all voting commands. |
| attg_disable_ranks | List | owner | Roles that can disable the plugin. |


### Grenade Config
| Config        | Value Type | Default | Description |
| :-------------: | :---------: | :---------: |:------ |
| ~~attg_grenade_fire_rate~~ | Float | 3f | Firerate for grenade launcher. |
| ~~attg_grenade_magazine~~ | Integer | 1 | Number of grenades per magazine. |
| ~~attg_grenade_reserve_ammo~~ | Integer | 10 | How much grenade launcher ammo each player gets. |
| ~~attg_grenade_krakatoa~~ | Integer | 10 | Number of shoot sounds to play when shot. |
| ~~attg_grenade_suppressed_krakatoa~~ | Integer | 7 | Number of shoot sounds to play when shot with a suppressor. |

### Taser Config
| Config        | Value Type | Default | Description |
| :-------------: | :---------: | :---------: |:------ |
| ~~attg_taze_fire_rate~~ | Float | 0.5f | Firerate for taser |
| ~~attg_taze_magazine~~ | Integer | 1 | Number of taser shots per magazine. |
| ~~attg_taze_reserve_ammo~~ | Integer | 10 | How much taser ammo each player gets. |
| ~~attg_taze_krakatoa~~ | Integer | 4 | Number of shoot sounds to play when shot. |
| ~~attg_taze_tag_time~~ | Float | 2f | How log after player is hit to be tazed. |
| ~~attg_taze_tag_glitches~~ | Integer | 15 | Number of screen effects to play when player is hit |

### Other Config
| Config        | Value Type | Default | Description |
| :-------------: | :---------: | :---------: |:------ |
| attg_disable | Bool | false | Disables Event Plugin |

# Thanks
Some of the code that I am using was made by 


* [Joker119](https://github.com/galaxy119)

  * https://github.com/galaxy119/JokerAnnouncements

* [Evan](https://github.com/Rnen)

  * https://github.com/Rnen/AdminToolbox
  
