using System;
using StardewModdingAPI;
using StardewValley;

namespace StaminaHealthMonitorMod
{
    public class ModEntry : Mod
    {
        private int lastSeenHealth = -1;
        private float lastSeenStamina = -1.0f;

        public override void Entry(IModHelper helper)
        {
            helper.Events.GameLoop.UpdateTicked += this.OnUpdateTicked;
        }

        private void OnUpdateTicked(object sender, EventArgs e)
        {
            // Just return if player hasn't loaded a save yet
            if (!Context.IsWorldReady)
                return;

            // Check if the health has changed, and if so, log this change.
            if (Game1.player.health != lastSeenHealth)
            {
                lastSeenHealth = Game1.player.health;
                this.Monitor.Log($"Health changed to {lastSeenHealth} / {Game1.player.maxHealth}", LogLevel.Debug);
            }

            // Check if the stamina has changed, and if so, log this change.
            if (Game1.player.stamina != lastSeenStamina)
            {
                lastSeenStamina = Game1.player.stamina;
                this.Monitor.Log($"Stamina changed to {lastSeenStamina} / {Game1.player.maxStamina}", LogLevel.Debug);
            }
        }
    }
}
