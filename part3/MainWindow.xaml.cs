using System;
using System.IO;
using System.Media;
using System.Windows;

namespace CyberSecurityChatBot
{
    public partial class MainWindow : Window
    {
        private CyberBot bot;

        public MainWindow()
        {
            InitializeComponent();
            bot = new CyberBot();
            ChatDisplay.Text = "Bot: Hello! Ask me anything about cybersecurity topics like phishing, password safety, suspicious links, or privacy.\n";

            PlayWelcomeAudio(); // Play audio on app start
        }

        private void PlayWelcomeAudio()
        {
            try
            {
                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "voicebooking-speech.wav");
                SoundPlayer player = new SoundPlayer(path);
                player.Load();
                player.Play(); // or use PlaySync() for blocking
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error playing welcome audio: " + ex.Message);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string input = UserInput.Text.Trim();
            if (!string.IsNullOrEmpty(input))
            {
                ChatDisplay.Text += $"\nYou: {input}";
                string response = bot.GetResponse(input);
                ChatDisplay.Text += $"\nBot: {response}\n";
                UserInput.Clear();
                ChatScrollViewer.ScrollToEnd(); // Scrolls to latest message
            }
        }
    }
}
