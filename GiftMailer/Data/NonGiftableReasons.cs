﻿namespace GiftMailer.Data;

/// <summary>
/// Possible reasons why sending a gift would not be allowed.
/// </summary>
[Flags]
public enum NonGiftableReasons
{
    /// <summary>
    /// No reason specified, i.e. gifting should be allowed.
    /// </summary>
    None = 0,

    /// <summary>
    /// The player hasn't met this NPC yet.
    /// </summary>
    /// <remarks>
    /// Unmet NPCs are usually suppressed for both immersion and balance, similar to the NPCs tab in
    /// the main menu which obscures NPCs whom the player hasn't met.
    /// </remarks>
    Unmet = 1,

    /// <summary>
    /// The NPC is not allowed to receive any gifts.
    /// </summary>
    /// <remarks>
    /// Corresponds to the result of <see cref="NPC.CanReceiveGifts"/>. Having this flag means that
    /// the way the game is currently configured, there is no item at all that can be given to them.
    /// </remarks>
    CannotReceiveGifts = 2,

    /// <summary>
    /// The NPC is the player's current spouse.
    /// </summary>
    /// <remarks>
    /// Generally an immersion decision; it doesn't make sense to mail a gift to a spouse who is
    /// already living under the same roof. Where would you mail it to?
    /// </remarks>
    Spouse = 4,

    /// <summary>
    /// The NPC is divorced from the player (previously spouse).
    /// </summary>
    /// <remarks>
    /// Divorced NPCs generally refuse gifts or can't increase friendship.
    /// </remarks>
    Divorced = 8,

    /// <summary>
    /// The NPC is one of the farmer's own children.
    /// </summary>
    /// <remarks>
    /// Same immersion considerations as <see cref="Spouse"/> and also has compatibility
    /// considerations with child mods.
    /// </remarks>
    Child = 16,

    /// <summary>
    /// NPC has already been gifted the maximum number of times this week.
    /// </summary>
    /// <remarks>
    /// Since gifts are sent the <em>next</em> day, this also means that the day of the week is not
    /// Saturday, because the limits reset on Sunday.
    /// </remarks>
    WeeklyLimit = 32,

    /// <summary>
    /// The player has already maxed friendship with this NPC. May be used as a "soft" restriction,
    /// since this does not prevent gift-giving in the game, only makes it pointless from an
    /// advancement perspective.
    /// </summary>
    MaxFriendship = 64,

    /// <summary>
    /// The NPC would reject the gift if given in person.
    /// </summary>
    /// <remarks>
    /// Rejection means they literally do not accept the gift. They "react" to it with a custom
    /// dialogue response but no transfer takes place.
    /// </remarks>
    Rejection = 128,
}
