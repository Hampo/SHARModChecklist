# SHARModChecklist
A checklist designed to be used with modded The Simpsons: Hit & Run.

# Installation
1. Ensure you have the latest [.NET Desktop Runtime 8](https://dotnet.microsoft.com/en-us/download/dotnet/8.0) installed.
2. Download the latest version from [releases](https://github.com/Hampo/SHARModChecklist/releases/latest).
3. You should now be able to launch the checklist either before or during gameplay.

# How to use
The checklist will only work with mods that have a config.

When first detecting a mod, it will first check for a file named `<InternalModName>.json` in the local directory. If this fails, it will then look at the [mod configs](https://github.com/Hampo/SHARModChecklist/tree/main/ModConfigs) in this repo.

If no mod is detected, or if no config is found, the checklist will not function.

# Making a config
At this time, there are unfortunately no tools to assist in creating mod configs.

The manual process is:
1. Copy an existing mod config file.
2. Set `ModName` to your mod's internal name.
3. If necessary, set `DisplayName` to your mods display name, or remove the key/value.
4. If you have any relevant custom text configured in `CustomText.ini`, add them to `TextBibleDefaults`.
    - Revelant custom text would be reward or merchandise names, and card titles.
5. For each level, populate `Cards`, `Gags` and `Wasps`.
    - `Name` on Cards, `Filename` on Gags and `Locator` on Wasps are unused by the UI. They are only assist when modifying the config.
    - The order ***must*** match the load order in-game. This would be the order of `GagBegin` or `AddSpawnPointByLocator` functions in the MFK files.

Once tested locally, fork this repo and submit a PR to add your mod's config.
