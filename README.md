# VAPatches

[Valheim Ascended](https://thunderstore.io/c/valheim/p/kpttr/Valheim_Ascended/) is a great mod but still under development. Some things are annoying to me but seem like they will take a good amount of work to fix or implement properly OR they just don't fit the vision of the original mod. This collection of patches is to adjust some of that behavior in the meantime. I made it for myself and will keep working on it but wanted to share.

## So Far
I've only fixed INT scaling for cooldowns and I've only tested the patch locally. It's also hacky and still doesn't work with all of the talents that claim to reduce CDs directly. If kpttr changes the mechanism that cooldowns are checked with, this falls apart. 

## The Future
Some things I'm planning on but don't know if I'll actually get around to:
- Implement proper CD reduction handling for talents, status effects (ie. armor passives), etc.
- Have some abilities' damage be the same damage types as the currently equipped weapon/weapons. Warriors would be able to cut down a swathe of forest with a single whirlwind or mine a whole ore vein with a stomp. The idea so far is anything that deals (someFactor x Weapon Damage).
- Expand ability list between classes where it makes sense
- Configurable ability slots?? ie more or slottable with other classes' abilities
- More talent points so you can crosspath in a class?
- more fixes where I see them.
- whatever else I come up with.

## Caveats
All of this is subject to change at any moment for any reason. I'm never going to take this repo down (I'll probably forget about it) but it may become useless if enough things in kpttr's mod change and break this.

## Credits
- Obviously kpttr and baldy.
- The Valheim dev team.
- The BepInEx and Harmony devs.
