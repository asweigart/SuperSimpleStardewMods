using System;
using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewModdingAPI.Utilities;
using StardewValley;

namespace KonamiCheatCodeMod
{
    public class ModEntry : Mod
    {
        // This array represents the sequence of button presses for the
        // Konami code which will give the player 1,000,000g:
        private readonly SButton[] konamiCode = {SButton.Up, SButton.Up, SButton.Down, SButton.Down,
            SButton.Left, SButton.Right, SButton.Left, SButton.Right,
            SButton.A, SButton.B, SButton.A, SButton.B};


        // This is the index for where in the Konami code input sequence we currently are:
        private int indexOfWhereWeAreInTheKonamiCodeInputSequence = 0;
        public override void Entry(IModHelper helper)
        {
            // Add our button press event handler:
            helper.Events.Input.ButtonPressed += this.OnButtonPressed;
        }
        private void OnButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            // Just return if player hasn't loaded a save yet
            if (!Context.IsWorldReady)
                return;

            if (e.Button == this.konamiCode[this.indexOfWhereWeAreInTheKonamiCodeInputSequence])
            {
                // Player has pressed the next button in the Konami code sequence:
                this.indexOfWhereWeAreInTheKonamiCodeInputSequence += 1;
                this.Monitor.Log($"{Game1.player.Name} entered {e.Button}", LogLevel.Debug);
                if (this.indexOfWhereWeAreInTheKonamiCodeInputSequence == this.konamiCode.Length)
                {
                    // Player has finished entered the Konami code sequence:
                    this.indexOfWhereWeAreInTheKonamiCodeInputSequence = 0;
                    Game1.player.Money += 1000000;
                    this.Monitor.Log($"{Game1.player.Name} entered the Konami code and got 1,000,000g.", LogLevel.Debug);
                }
            }
            else
            {
                // Player has entered something that isn't in the Konami code
                // sequence, so we restart the index at 0:
                if (this.indexOfWhereWeAreInTheKonamiCodeInputSequence > 0)
                {
                    this.Monitor.Log($"{Game1.player.Name} has reset the Konami code sequence.", LogLevel.Debug);
                }
                this.indexOfWhereWeAreInTheKonamiCodeInputSequence = 0;
            }

        }
    }
}
