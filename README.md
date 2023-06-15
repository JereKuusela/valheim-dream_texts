# Dream Texts

Allows changing dream texts and conditions.

Install on all clients and on the server if you want to sync the dreams (modding [guide](https://youtu.be/L9ljm2eKLrk)).

# Usage

After loading a world, `dream_texts.yaml` is generated to the config folder.

The file can be modified while in the game with a text editor. If installed on server, then the server file is applied to all clients.

When sleeping, the game randomly selects one of the available dreams and then does the random chance roll for that dream.

This means the random chances are divided by the number of dreams that are available.

# Examples

A dream with text "Sweet dreams". If this is the only dream, it would always appear.
```
- text: Sweet dreams!
  chance: 1
```

Before Eikthyr is defeated, the sweet dream would always appear (always gets selected with 1 chance).
After Eikthyr is defeated, the sweet dream would appear 50% of the time and the bad dream 25% of the time.
After Bonemass is defeated, the bad dream would appear 50% of the time.
```
- text: Sweet dreams!
  chance: 1
  forbiddenKeys: defeated_bonemass
- text: Bad nightmare!
  chance: 0.5
  requiredKeys: defeated_eikthyr
```

See [Global keys](https://valheim.fandom.com/wiki/Global_Keys) for a list of required or forbidden keys.

# Credits

Thanks for Azumatt for creating the mod icon!

Sources: [GitHub](https://github.com/JereKuusela/valheim-dream_texts)

Donations: [Buy me a computer](https://www.buymeacoffee.com/jerekuusela)

