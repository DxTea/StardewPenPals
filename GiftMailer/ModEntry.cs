﻿using GiftMailer.Data;
using HarmonyLib;
using StardewModdingAPI.Events;
using StardewUI;

namespace GiftMailer;

internal sealed class ModEntry : Mod
{
    private string CustomRulesAssetName => $"{ModManifest.UniqueID}/Rules";

    // Initialized in Entry
    private ModConfig config = null!;

    // Reloaded on save
    private ModData data = new();

    public override void Entry(IModHelper helper)
    {
        I18n.Init(helper.Translation);
        config = helper.ReadConfig<ModConfig>();

        Logger.Monitor = Monitor;

        Helper.Events.Content.AssetRequested += Content_AssetRequested;

        MailboxPatches.ConfigSelector = () => config;
        MailboxPatches.CustomRulesSelector = () =>
            helper.GameContent.Load<CustomRules>(CustomRulesAssetName);
        MailboxPatches.DataSelector = () => data;
        MailboxPatches.Monitor = Monitor;
        var harmony = new Harmony(ModManifest.UniqueID);
        harmony.Patch(
            AccessTools.Method(typeof(GameLocation), nameof(GameLocation.mailbox)),
            transpiler: new(typeof(MailboxPatches), nameof(MailboxPatches.MailboxTranspiler))
        );
    }

    private void Content_AssetRequested(object? sender, AssetRequestedEventArgs e)
    {
        if (e.NameWithoutLocale.IsEquivalentTo(CustomRulesAssetName))
        {
            e.LoadFromModFile<CustomRules>("assets/rules.json", AssetLoadPriority.Low);
        }
    }
}
