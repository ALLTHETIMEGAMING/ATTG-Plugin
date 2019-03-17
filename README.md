# ATTG-Plugin
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

# Item DLC
| Item ID       | Description |
| :-------------: | :---------: | 
| 200 | Medkit that changes player into SCP-106 |
| 201 | Medkit that changes player into SCP-049 |
| 202 | Medkit that changes player into SCP-939 |
| 203 | Medkit that changes player into SCP-173 |
| 204 | Medkit that changes player into SCP-096 |
| 205 | Medkit that changes player into SCP-049-2 |
| 206 | Medkit that changes player into SCP-079 |
| 207 | Medkit that teleports player back to where he picked up the item after 1 min |

# Commands
| Command      | Parameters| Description |
| :-------------: | :---------: | :---------: | 
| AGTL | NONE | Turns on all Tesla |
| AGEL | NONE | Toggles Elevator locking |
| AGTLR | NONE | Toggles Tesla Gates |
| AGELR | Float | Changes elevator speed |
| AGP079 | NONE | Opens SCP-079 Generators |
| AGE079 | NONE | Ejects all tablets from generators |
| AGL079 | NONE | Levels up 079 and gives AP |
| AGVOTET | NONE | Toggles Voting  |
| AGVOTEC | NONE| Clears Vote |
| AGVOTES | NONE | Displays Vote |
| AGVOTEBC| NONE | Displays how to vote |
| AGAMMO | Players Name | Gives player 100000 ammo|
| AGUP | Players Name | TPs player up from there position |
| AGDISABLE | NONE | Disables Event Plugin |
| AGSHAKE | NONE | Shakes screens of all players |
| AGSPEED | Player Name | Sets speed of player |
| AGSPEEDA | Float | Sets speed of all 939s |
| AG079T | Float | Sets Start time of all generators|



# Config


### Admin Config
| Config        | Value Type | Default | Description |
| :-------------: | :---------: | :---------: |:------ |
| attg_ranks | List | owner, coowner | Roles that can do all commands. |
| attg_vote_ranks | List | owner, coowner, admin | Roles that do all voting commands. |
| attg_command_disable_ranks | List | owner | Roles that can disable the ATTG Command Plugin. |
| attg_item_disable_ranks | List | owner | Roles that can disable the Item DLC

### Other Config
| Config        | Value Type | Default | Description |
| :-------------: | :---------: | :---------: |:------ |
| attg_command_disable | Bool | false | Disables ATTG Command Plugin |
| attg_item_disable | Bool | false | Disables ATTG Item Plugin |

# Thanks
Some of the code that I am using was made by 

* [Evan](https://github.com/Rnen)

  * https://github.com/Rnen/AdminToolbox
  
