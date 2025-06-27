using System.Media;
using System.Windows;

namespace CyberSecurityChatBot
{
    public class GreetingService
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
                MessageBox.Show("Voice greeting not found. Add 'welcome.wav' to your project folder.", "Missing Sound", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
