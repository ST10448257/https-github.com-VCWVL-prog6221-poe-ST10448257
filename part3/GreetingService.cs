using System;
using System.Media;

namespace CyberSecurityChatBot
{
    class GreetingService
    {
        public void PlayWelcomeSound()
        {
            try
            {
                SoundPlayer player = new SoundPlayer("welcome.wav");
                player.Play();
            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Voice greeting not found. Add 'welcome.wav' to your project folder.");
                Console.ResetColor();
            }
        }
    }
}