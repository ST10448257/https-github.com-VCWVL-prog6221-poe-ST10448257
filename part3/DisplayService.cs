using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace CyberSecurityChatBot
{
    public class DisplayService
    {
        private TextBlock outputText;

        public void AttachOutput(TextBlock textBlock)
        {
            outputText = textBlock;
        }

        public void ShowBotMessage(string message)
        {
            AppendMessage("🤖 Bot: " + message, Brushes.Lime);
        }

        public void ShowUserMessage(string message)
        {
            AppendMessage("👤 You: " + message, Brushes.Cyan);
        }

        public void ShowAsciiArt()
        {
            AppendMessage(
                @"
           __  ___    ___    __                        ___  ___              __   ___       __   ___     __       __   ___  __   __   __  ___
   /\  |__)  |  | |__  | /  ` |  /\  |       | |\ |  |  |__  |    |    | / _` |__  |\ | /  ` |__     /  ` \ / |__) |__  |__) |__) /  \  | 
  /~~\ |  \  |  | |    | \__, | /~~\ |___    | | \|  |  |___ |___ |___ | \__> |___ | \| \__, |___    \__,  |  |__) |___ |  \ |__) \__/  | 
", Brushes.Gray);
        }

        public void ShowWelcome(string userName)
        {
            ShowBotMessage($"Welcome, {userName}! 👋");
        }

        public void ShowBox(string[] lines, Brush color)
        {
            string border = new string('-', 40);
            AppendMessage(border, color);
            foreach (var line in lines)
            {
                AppendMessage("| " + line.PadRight(36) + " |", color);
            }
            AppendMessage(border, color);
        }

        private void AppendMessage(string message, Brush color)
        {
            if (outputText == null) return;

            outputText.Inlines.Add(new Run(message + "\n") { Foreground = color });
        }
    }
}
