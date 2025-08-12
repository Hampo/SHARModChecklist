using SHARMemory.SHAR.Classes;
using SHARMemory.SHAR.Structs;
using System.Diagnostics;
using static SHARModChecklist.ModConfig;

namespace SHARModChecklist;

public partial class FrmMain : Form
{
    private const string RegistrySettings = @"Software\SHARModChecklist";
    private static readonly Microsoft.Win32.RegistryKey RegistryKey = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(RegistrySettings, Microsoft.Win32.RegistryKeyPermissionCheck.ReadWriteSubTree);

    private ModConfig? _modConfig = null;
    private ModConfig? ModConfig
    {
        get => _modConfig;
        set
        {
            _modConfig = value;
            if (value != null)
            {
                Text = $"SHAR Mod Checklist - {value.DisplayName ?? value.ModName}";
                for (int i = 0; i < Buttons.Length; i++)
                {
                    Buttons[i].Visible = value.Levels.Count > i;
                }
            }
            else
            {
                Text = "SHAR Mod Checklist";
                foreach (var button in Buttons)
                    button.Visible = true;
            }
        }
    }
    private bool _updating = false;
    private readonly Dictionary<uint, int> _rewardMap = [];

    private Font DefaultButtonFont;
    private Font SelectedButtonFont;
    private readonly System.Windows.Forms.Button[] Buttons;

    public FrmMain()
    {
        InitializeComponent();

        DefaultButtonFont = BtnLevel1.Font;
        SelectedButtonFont = new Font(DefaultButtonFont, FontStyle.Bold | FontStyle.Italic | FontStyle.Underline);
        Buttons = [BtnLevel1, BtnLevel2, BtnLevel3, BtnLevel4, BtnLevel5, BtnLevel6, BtnLevel7];
    }

    private void FrmMain_Load(object sender, EventArgs e)
    {
        string[] names = RegistryKey.GetValueNames();
        bool darkMode = Array.IndexOf(names, "DarkMode") >= 0 && RegistryKey.GetValueKind("DarkMode") == Microsoft.Win32.RegistryValueKind.DWord && (int)RegistryKey.GetValue("DarkMode", 0) != 0;
        bool largeFont = Array.IndexOf(names, "LargeFont") >= 0 && RegistryKey.GetValueKind("LargeFont") == Microsoft.Win32.RegistryValueKind.DWord && (int)RegistryKey.GetValue("LargeFont", 0) != 0;
        CBDarkMode.Checked = darkMode;
        CBLargeFont.Checked = largeFont;

        if (Array.IndexOf(names, "LocationX") >= 0 && RegistryKey.GetValueKind("LocationX") == Microsoft.Win32.RegistryValueKind.DWord
            && Array.IndexOf(names, "LocationY") >= 0 && RegistryKey.GetValueKind("LocationY") == Microsoft.Win32.RegistryValueKind.DWord)
            Location = new Point((int)RegistryKey.GetValue("LocationX", 0), (int)RegistryKey.GetValue("LocationY", 0));

        if (Array.IndexOf(names, "SizeWidth") >= 0 && RegistryKey.GetValueKind("SizeWidth") == Microsoft.Win32.RegistryValueKind.DWord
            && Array.IndexOf(names, "SizeHeight") >= 0 && RegistryKey.GetValueKind("SizeHeight") == Microsoft.Win32.RegistryValueKind.DWord)
            Size = new Size((int)RegistryKey.GetValue("SizeWidth", 0), (int)RegistryKey.GetValue("SizeHeight", 0));

        CBLevel.SelectedIndex = -1;
        CBLevel.SelectedIndex = 0;
    }

    private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
    {
        if (WindowState == FormWindowState.Normal)
        {
            RegistryKey.SetValue("LocationX", Location.X, Microsoft.Win32.RegistryValueKind.DWord);
            RegistryKey.SetValue("LocationY", Location.Y, Microsoft.Win32.RegistryValueKind.DWord);
            RegistryKey.SetValue("SizeWidth", Size.Width, Microsoft.Win32.RegistryValueKind.DWord);
            RegistryKey.SetValue("SizeHeight", Size.Height, Microsoft.Win32.RegistryValueKind.DWord);
        }
    }

    private async Task UpdateModConfig(SHARMemory.SHAR.Memory mem)
    {
        var mainMod = mem.GetMainMod();
        if (mainMod == null)
        {
            // I don't want to support vanilla SHAR right now
            ModConfig = null;
            CBLevel.SelectedIndex = -1;
            return;
        }

        if (ModConfig?.ModName == mainMod)
            return;

        ModConfig? config = null;
        try
        {
            config = FromFile($"{mainMod}.json");
        }
        catch (Exception ex)
        {
            Debugger.Break();
        }

        try
        {
            config ??= await FromURL($"https://raw.githubusercontent.com/Hampo/SHARModChecklist/refs/heads/main/ModConfigs/{mainMod}.json");
        }
        catch (Exception ex)
        {
            Debugger.Break();
        }

        ModConfig = config;
    }

    private void CLB_DisableCheck(object sender, ItemCheckEventArgs e)
    {
        if (_updating)
            return;

        e.NewValue = e.CurrentValue;
    }

    private void TmrUpdater_Tick(object sender, EventArgs e)
    {
        var level = CBLevel.SelectedIndex;
        if (_updating || level == -1)
            return;

        var p = SHARMemory.SHAR.Memory.GetSHARProcess();
        if (p == null)
        {
            CBLevel.SelectedIndex = -1;
            return;
        }

        using var mem = new SHARMemory.SHAR.Memory(p);
        if (mem.Singletons.CharacterSheetManager?.CharacterSheet is not CharacterSheet characterSheet)
        {
            CBLevel.SelectedIndex = -1;
            return;
        }

        if (mem.Singletons.RewardsManager is not RewardsManager rewardsManager)
        {
            CBLevel.SelectedIndex = -1;
            return;
        }

        _updating = true;

        var levelListArray = characterSheet.LevelList.ToArray();
        var levelRewardsArray = rewardsManager.RewardsList.ToArray();
        var levelTokenStoreListArray = rewardsManager.LevelTokenStoreList.ToArray();
        var persistentObjectStates = characterSheet.PersistentObjectStates.ToArray();

        for (int i = 0; i < 7; i++)
        {
            var levelData = levelListArray[i];
            var levelRewards = levelRewardsArray[i];
            var levelMerchandiseCount = levelTokenStoreListArray[i].Counter;
            var levelMerchandise = new List<Merchandise>(levelMerchandiseCount);
            for (int j = 0; j < levelMerchandiseCount; j++)
                levelMerchandise.Add(mem.Functions.GetMerchandise(i, j));

            var buttonText = $"Level {i + 1}";
            if (LevelComplete(i, levelData, levelRewards, levelMerchandise, persistentObjectStates))
                buttonText += "*";
            SetControlText(Buttons[i], buttonText);

            if (i == level)
            {
                UpdateData(i, levelData, levelRewards, levelMerchandise, persistentObjectStates);
                var progress = LevelProgress(i, levelData, levelRewards, levelMerchandise, persistentObjectStates);
                SetControlText(GBStats, $"Stats - Level {i + 1} ({progress * 100:F1}%)");
            }
        }

        _updating = false;
    }

    private async void CBLevel_SelectedIndexChanged(object sender, EventArgs e)
    {
        var levelIndex = CBLevel.SelectedIndex;
        SetSelectedButton(levelIndex);

        if (levelIndex == -1)
        {
            GBStats.Text = "Stats";
            _updating = true;

            LblMissions.Text = "Missions: ?/7";
            LblStreetRaces.Text = "Street Races: ?/3";
            LblBonusMission.Text = "Bonus Mission: ?/1";

            CLBCards.BeginUpdate();
            for (int i = 0; i < 7; i++)
            {
                CLBCards.Items[i] = TSMIShowCardLocationSpoilers.Checked ? "Unknown Location" : $"Card {i + 1}";
                CLBCards.SetItemChecked(i, false);
            }
            GBCards.Text = "Cards (?/7)";
            CLBCards.EndUpdate();

            CLBRewards.Items.Clear();
            GBRewards.Text = "Rewards (?/?)";
            _rewardMap.Clear();

            CLBGags.Items.Clear();
            GBGags.Text = "Gags (?/?)";

            CLBWasps.Items.Clear();
            GBWasps.Text = "Wasps (?/?)";

            _updating = false;

            for (int i = 0; i < Buttons.Length; i++)
                Buttons[i].Text = $"Level {i + 1}";

            return;
        }

        var p = SHARMemory.SHAR.Memory.GetSHARProcess();
        if (p == null)
        {
            CBLevel.SelectedIndex = -1;
            return;
        }

        using var mem = new SHARMemory.SHAR.Memory(p);
        await UpdateModConfig(mem);
        if (ModConfig == null)
        {
            CBLevel.SelectedIndex = -1;
            return;
        }

        if (mem.Singletons.CharacterSheetManager?.CharacterSheet is not CharacterSheet characterSheet)
        {
            CBLevel.SelectedIndex = -1;
            return;
        }

        if (mem.Singletons.RewardsManager is not RewardsManager rewardsManager)
        {
            CBLevel.SelectedIndex = -1;
            return;
        }

        var textBible = mem.Globals.TextBible?.CurrentLanguage;
        if (textBible == null)
        {
            CBLevel.SelectedIndex = -1;
            return;
        }

        _updating = true;
        GBStats.Text = $"Stats - Level {levelIndex + 1}";

        var level = ModConfig.Levels[levelIndex];
        var levelData = characterSheet.LevelList[levelIndex];
        var levelRewards = rewardsManager.RewardsList[levelIndex];

        CLBCards.BeginUpdate();
        for (int cardIndex = 0; cardIndex < 7; cardIndex++)
        {
            if (TSMIShowCardLocationSpoilers.Checked)
                CLBCards.Items[cardIndex] = level.Cards.Count > cardIndex ? level.Cards[cardIndex].Location : "Unknown Location";
            else
                CLBCards.Items[cardIndex] = GetString(textBible, $"CARD_TITLE_{(levelIndex * 8 + cardIndex).ToString().PadLeft(2, '0')}", $"Card {cardIndex + 1}");
        }
        CLBCards.EndUpdate();

        CLBRewards.BeginUpdate();
        CLBRewards.Items.Clear();
        _rewardMap.Clear();

        void handleReward(Reward reward)
        {
            if (reward == null)
                return;
            if (reward.RewardType == Reward.RewardTypes.Null)
                return;
            if (reward.QuestType == Reward.QuestTypes.DefaultCar || reward.QuestType == Reward.QuestTypes.DefaultSkin)
                return;

            var index = CLBRewards.Items.Add($"{GetString(textBible, reward.Name.ToUpper(), reward.Name)}", reward.Earned);
            _rewardMap[reward.Address] = index;
        }
        handleReward(levelRewards.BonusMission);
        handleReward(levelRewards.StreetRace);
        handleReward(levelRewards.Cards);
        handleReward(levelRewards.DefaultCar);
        handleReward(levelRewards.DefaultSkin);
        handleReward(levelRewards.GoldCards);

        var merchandiseCount = rewardsManager.LevelTokenStoreList[levelIndex].Counter;
        List<Merchandise> levelMerchandise = new(merchandiseCount);
        for (int i = 0; i < merchandiseCount; i++)
        {
            var merchandise = mem.Functions.GetMerchandise(levelIndex, i);
            levelMerchandise.Add(merchandise);
            handleReward(merchandise);
        }

        CLBRewards.EndUpdate();

        CLBGags.BeginUpdate();
        CLBGags.Items.Clear();

        for (int gagIndex = 0; gagIndex < levelRewards.TotalGagsInLevel; gagIndex++)
            CLBGags.Items.Add(level.Gags.Count > gagIndex ? level.Gags[gagIndex].Location : $"Gag {gagIndex + 1}", (levelData.GagMask & (1 << gagIndex)) != 0);

        CLBGags.EndUpdate();

        CLBWasps.BeginUpdate();
        CLBWasps.Items.Clear();
        var persistentObjectStates = characterSheet.PersistentObjectStates.ToArray();

        for (int waspIndex = 0; waspIndex < levelRewards.TotalWaspsInLevel; waspIndex++)
            CLBWasps.Items.Add(level.Wasps.Count > waspIndex ? level.Wasps[waspIndex].Location : $"Wasp {waspIndex + 1}", IsPersistentObjectDestroyed(persistentObjectStates, 75 + levelIndex, waspIndex));

        CLBWasps.EndUpdate();

        UpdateData(levelIndex, levelData, levelRewards, levelMerchandise, persistentObjectStates);

        _updating = false;
    }

    private static float LevelProgress(int level, LevelRecord levelData, LevelRewardRecord levelRewards, List<Merchandise> levelMerchandise, byte[] persistentObjectStates)
    {
        var numMissionsComplete = levelData.Missions.List.Where(x => x.Name != "m0" && x.Name != "m8" && x.Name != "NULL" && x.Completed).Count();
        var missions = numMissionsComplete / 7f;

        var bonusMission = levelData.BonusMission.Completed ? 1 : 0;

        var numStreetRacesComplete = levelData.StreetRaces.List.Where(x => x.Completed).Count();
        var streetRaces = numStreetRacesComplete / 3f;

        var numSkinsPurchased = 0;
        var numSkinsTotal = 0f;

        var numCarsPurchased = 0;
        var numCarsTotal = 0f;

        void handleReward(Reward reward)
        {
            if (reward == null)
                return;
            if (reward.QuestType == Reward.QuestTypes.DefaultCar || reward.QuestType == Reward.QuestTypes.DefaultSkin)
                return;

            switch (reward.RewardType)
            {
                case Reward.RewardTypes.SkinOther:
                    numSkinsTotal++;
                    if (reward.Earned)
                        numSkinsPurchased++;
                    break;
                case Reward.RewardTypes.PlayerCar:
                    numCarsTotal++;
                    if (reward.Earned)
                        numCarsPurchased++;
                    break;
            }
        }
        handleReward(levelRewards.BonusMission);
        handleReward(levelRewards.StreetRace);
        handleReward(levelRewards.Cards);
        handleReward(levelRewards.DefaultCar);
        handleReward(levelRewards.DefaultSkin);
        handleReward(levelRewards.GoldCards);
        foreach (var merchandise in levelMerchandise)
            handleReward(merchandise);

        var skins = numSkinsPurchased / numSkinsTotal;
        var cars = numCarsPurchased / numCarsTotal;

        var numCardsCollected = levelData.Cards.List.Where(x => x.Completed).Count();
        var cards = numCardsCollected / 7f;

        var numGagsCollected = 0;
        for (int i = 0; i < levelRewards.TotalGagsInLevel; i++)
            if ((levelData.GagMask & (1 << i)) == 1)
                numGagsCollected++;
        var gags = numGagsCollected / (float)levelRewards.TotalGagsInLevel;

        var numWaspsDestroyed = 0;
        for (int i = 0; i < levelRewards.TotalWaspsInLevel; i++)
            if (IsPersistentObjectDestroyed(persistentObjectStates, 75 + level, i))
                numWaspsDestroyed++;
        var wasps = numWaspsDestroyed / (float)levelRewards.TotalWaspsInLevel;

        return (missions + bonusMission + streetRaces + skins + cars + cards + wasps + gags) / 8f;
    }

    private bool LevelComplete(int level, LevelRecord levelData, LevelRewardRecord levelRewards, List<Merchandise> levelMerchandise, byte[] persistentObjectStates)
    {
        if (levelData.Missions.List.Any(x => x.Name != "m0" && x.Name != "m8" && x.Name != "NULL" && !x.Completed))
            return false;

        if (levelData.StreetRaces.List.Any(x => !x.Completed))
            return false;

        if (!levelData.BonusMission.Completed)
            return false;

        for (int i = 0; i < 7; i++)
            if (!levelData.Cards.List[i].Completed)
                return false;

        bool handleReward(Reward reward)
        {
            if (reward == null)
                return true;
            if (!_rewardMap.TryGetValue(reward.Address, out var index))
                return true;

            return reward.Earned;
        }
        if (!handleReward(levelRewards.BonusMission))
            return false;
        if (!handleReward(levelRewards.StreetRace))
            return false;
        if (!handleReward(levelRewards.Cards))
            return false;
        if (!handleReward(levelRewards.DefaultCar))
            return false;
        if (!handleReward(levelRewards.DefaultSkin))
            return false;
        if (!handleReward(levelRewards.GoldCards))
            return false;

        for (int i = 0; i < levelMerchandise.Count; i++)
            if (!handleReward(levelMerchandise[i]))
                return false;

        for (int i = 0; i < levelRewards.TotalGagsInLevel; i++)
            if ((levelData.GagMask & (1 << i)) == 0)
                return false;

        for (int i = 0; i < levelRewards.TotalWaspsInLevel; i++)
            if (!IsPersistentObjectDestroyed(persistentObjectStates, 75 + level, i))
                return false;

        return true;
    }

    private void UpdateData(int level, LevelRecord levelData, LevelRewardRecord levelRewards, List<Merchandise> levelMerchandise, byte[] persistentObjectStates)
    {
        SetControlText(LblMissions, $"Missions: {levelData.Missions.List.Where(x => x.Name != "m0" && x.Completed).Count()}/7");
        SetControlText(LblStreetRaces, $"Street Races: {levelData.StreetRaces.List.Where(x => x.Completed).Count()}/3");
        SetControlText(LblBonusMission, $"Bonus Mission: {(levelData.BonusMission.Completed ? "1" : "0")}/1");

        bool[] cardStates = new bool[7];
        for (int i = 0; i < 7; i++)
        {
            cardStates[i] = levelData.Cards.List[i].Completed;
        }
        SetCLBCheckStates(CLBCards, cardStates);
        SetControlText(GBCards, $"Cards ({CLBCards.CheckedItems.Count}/{CLBCards.Items.Count})");

        bool[] rewardStates = new bool[CLBRewards.Items.Count];

        void handleReward(Reward reward)
        {
            if (reward == null)
                return;
            if (!_rewardMap.TryGetValue(reward.Address, out var index))
                return;

            rewardStates[index] = reward.Earned;
        }
        handleReward(levelRewards.BonusMission);
        handleReward(levelRewards.StreetRace);
        handleReward(levelRewards.Cards);
        handleReward(levelRewards.DefaultCar);
        handleReward(levelRewards.DefaultSkin);
        handleReward(levelRewards.GoldCards);

        for (int i = 0; i < levelMerchandise.Count; i++)
            handleReward(levelMerchandise[i]);
        SetCLBCheckStates(CLBRewards, rewardStates);
        SetControlText(GBRewards, $"Rewards ({CLBRewards.CheckedItems.Count}/{CLBRewards.Items.Count})");

        bool[] gagStates = new bool[levelRewards.TotalGagsInLevel];
        for (int i = 0; i < levelRewards.TotalGagsInLevel; i++)
            gagStates[i] = (levelData.GagMask & (1 << i)) != 0;
        SetCLBCheckStates(CLBGags, gagStates);
        SetControlText(GBGags, $"Gags ({CLBGags.CheckedItems.Count}/{levelRewards.TotalGagsInLevel})");

        bool[] waspStates = new bool[levelRewards.TotalWaspsInLevel];
        for (int i = 0; i < levelRewards.TotalWaspsInLevel; i++)
            waspStates[i] = IsPersistentObjectDestroyed(persistentObjectStates, 75 + level, i);
        SetCLBCheckStates(CLBWasps, waspStates);
        SetControlText(GBWasps, $"Wasps ({CLBWasps.CheckedItems.Count}/{levelRewards.TotalWaspsInLevel})");
    }

    private static bool IsPersistentObjectDestroyed(byte[] persistentObjectStates, int sector, int index)
    {
        if (sector < 0 || sector > 81)
            throw new ArgumentOutOfRangeException(nameof(sector), "Invalid sector");

        if (index < 0 || index > 127)
            throw new ArgumentOutOfRangeException(nameof(index), "Invalid index");

        int sectorOffset = sector * 16;
        int byteIndex = index / 8;
        int bitIndex = index % 8;

        return (persistentObjectStates[sectorOffset + byteIndex] & (1 << bitIndex % 8)) == 0;
    }

    private string GetString(FeLanguage feLanguage, string name, string defaultValue)
    {
        if (ModConfig != null && ModConfig.TextBibleDefaults.TryGetValue(name, out var result))
            return result;

        return feLanguage.GetString(name) ?? defaultValue;
    }

    private static void SetControlText(Control ctrl, string labelText)
    {
        if (ctrl.Text != labelText)
            ctrl.Text = labelText;
    }

    private static void SetCLBCheckStates(CheckedListBox checkedListBox, bool[] checkedStates)
    {
        if (checkedListBox.Items.Count != checkedStates.Length)
            return;

        bool needsUpdate = false;
        for (int i = 0; i < checkedListBox.Items.Count; i++)
        {
            if (checkedListBox.GetItemChecked(i) != checkedStates[i])
            {
                needsUpdate = true;
                break;
            }
        }

        if (!needsUpdate)
            return;

        checkedListBox.BeginUpdate();
        for (int i = 0; i < checkedListBox.Items.Count; i++)
            checkedListBox.SetItemChecked(i, checkedStates[i]);
        checkedListBox.EndUpdate();
    }

    private void CBTheming_CheckedChanged(object sender, EventArgs e)
    {
        RegistryKey.SetValue("DarkMode", CBDarkMode.Checked ? 1 : 0, Microsoft.Win32.RegistryValueKind.DWord);
        RegistryKey.SetValue("LargeFont", CBLargeFont.Checked ? 1 : 0, Microsoft.Win32.RegistryValueKind.DWord);

        Theming.ApplyTheme(this, CBDarkMode.Checked ? Theming.ThemeMode.Dark : Theming.ThemeMode.Light, CBLargeFont.Checked ? Theming.FontMode.Large : Theming.FontMode.Normal);

        DefaultButtonFont = CBLargeFont.Checked ? Theming.LargeFont : Theming.NormalFont;
        SelectedButtonFont = new Font(DefaultButtonFont, FontStyle.Bold | FontStyle.Italic | FontStyle.Underline);

        SetSelectedButton(CBLevel.SelectedIndex);
    }

    private void SetSelectedButton(int level)
    {
        for (int i = 0; i < Buttons.Length; i++)
        {
            Buttons[i].Font = i == level ? SelectedButtonFont : DefaultButtonFont;
        }
    }

    private void BtnLevel_Click(object sender, EventArgs e)
    {
        if (sender is not Control ctrl || !int.TryParse(ctrl.Tag?.ToString(), out int index))
            return;

        if (ModConfig != null && index >= ModConfig.Levels.Count)
            return;

        CBLevel.SelectedIndex = index;
        GBStats.Focus();
    }

    private void TSMIShowCardLocationSpoilers_CheckedChanged(object sender, EventArgs e)
    {
        var p = SHARMemory.SHAR.Memory.GetSHARProcess();
        if (p == null)
        {
            CBLevel.SelectedIndex = -1;
            return;
        }

        using var mem = new SHARMemory.SHAR.Memory(p);

        var textBible = mem.Globals.TextBible?.CurrentLanguage;
        if (textBible == null)
        {
            CBLevel.SelectedIndex = -1;
            return;
        }

        var levelIndex = CBLevel.SelectedIndex;

        CLBCards.BeginUpdate();

        if (ModConfig == null || levelIndex == -1)
        {
            for (int i = 0; i < 7; i++)
                CLBCards.Items[i] = TSMIShowCardLocationSpoilers.Checked ? "Unknown Location" : $"Card {i + 1}";
        }
        else
        {
            var level = ModConfig.Levels[levelIndex];
            for (int cardIndex = 0; cardIndex < 7; cardIndex++)
            {
                if (TSMIShowCardLocationSpoilers.Checked)
                    CLBCards.Items[cardIndex] = level.Cards.Count > cardIndex ? level.Cards[cardIndex].Location : "Unknown Location";
                else
                    CLBCards.Items[cardIndex] = GetString(textBible, $"CARD_TITLE_{(levelIndex * 8 + cardIndex).ToString().PadLeft(2, '0')}", $"Card {cardIndex + 1}");
            }
        }

        CLBCards.EndUpdate();
    }
}
