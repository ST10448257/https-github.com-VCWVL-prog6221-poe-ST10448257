using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace CyberSecurityChatBot
{
    public class DisplayService
    {
        private TextBox outputTextBox;

        // Attach the WPF TextBox control for output
        public void AttachOutput(TextBox textBox)
        {
            outputTextBox = textBox;
            outputTextBox.Text = ""; // clear on attach
        }

        // Helper to append text safely on UI thread
        private void AppendText(string text)
        {
            if (outputTextBox == null) return;

            outputTextBox.Dispatcher.Invoke(() =>
            {
                outputTextBox.AppendText(text + Environment.NewLine);
                outputTextBox.ScrollToEnd();
            });
        }

        // Show ASCII Art (cyan color simulation with prefix)
        public void ShowAsciiArt()
        {
            string asciiArt = @"
 __    _____      _                                        _ _          
 \ \  / ____|    | |                                      (_) |         
  \ \| |    _   _| |__   ___ _ __ ___  ___  ___ _   _ _ __ _| |_ _   _  
   > > |   | | | | '_ \ / _ \ '__/ __|/ _ \/ __| | | | '__| | __| | | | 
  / /| |___| |_| | |_) |  __/ |  \__ \  __/ (__| |_| | |  | | |_| |_| | 
 /_/  \_____\__, |_.__/ \___|_|  |___/\___|\___|\__,_|_|  |_|\__|\__, | 
 \ \   /\    __/ |                                                __/ | 
  \ \ /  \__|___/ ____ _ _ __ ___ _ __   ___  ___ ___            |___/  
   > > /\ \ \ /\ / / _` | '__/ _ \ '_ \ / _ \/ __/ __|                  
  / / ____ \ V  V / (_| | | |  __/ | | |  __/\__ \__ \                  
 /_/_/____\_\_/\_/_\__,_|_|  \___|_| |_|\___||___/___/                  
 \ \ |  _ \      | |                                                    
  \ \| |_) | ___ | |_                                                   
   > >  _ < / _ \| __|                                                  
  / /| |_) | (_) | |_                                                   
 /_/ |____/ \___/ \__|                                                  
                                           ";

            AppendText("[AsciiArt - Cyan]\n" + asciiArt + "\n[/AsciiArt]");
        }

        // Show welcome message boxed with border lines
        public void ShowWelcome(string name)
        {
            string welcomeMessage = $"Hello, {name}! I'm your Cybersecurity Awareness bot.";
            string learnMessage = "What would you like to learn about?";

            int width = Math.Max(welcomeMessage.Length, learnMessage.Length) + 4;

            string border = new string('═', width);

            AppendText($"╔{border}╗");
            AppendText($"║ {welcomeMessage.PadRight(width - 2)} ║");
            AppendText($"║ {learnMessage.PadRight(width - 2)} ║");
            AppendText($"╚{border}╝");
            AppendText("");
        }

        // Show a message from bot in a distinct style (prefix)
        public void ShowBotMessage(string message)
        {
            AppendText($"[Bot]: {message}");
        }

        // Show a message from user in a distinct style (prefix)
        public void ShowUserMessage(string message)
        {
            AppendText($"[You]: {message}");
        }

        // Display boxed text lines with a "border" using ASCII chars
        public void ShowBox(string[] lines, Brush borderColor)
        {
            if (lines == null || lines.Length == 0) return;

            int width = 0;
            foreach (var line in lines)
                if (line.Length > width) width = line.Length;

            width += 4; // padding

            string border = new string('═', width);

            AppendText($"╔{border}╗");
            foreach (var line in lines)
            {
                AppendText($"║ {line.PadRight(width - 2)} ║");
            }
            AppendText($"╚{border}╝");
            AppendText("");
        }
    }
}
