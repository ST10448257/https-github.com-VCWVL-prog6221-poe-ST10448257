using System;
using System.Threading;

namespace CyberSecurityChatBot
{
    class DisplayService
    {
        public void DisplayAsciiArt()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(@" __    _____      _                                        _ _          
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
                                           ");
            Console.ResetColor();
        }

        public void TypeText(string text, int delay = 50)
        {
            foreach (char c in text)
            {
                Console.Write(c);
                Thread.Sleep(delay);
            }
            Console.WriteLine();
        }

        public void DisplayWelcomeMessage(string name)
        {
            string welcomeMessage = $" Hello, {name}! I'm your Cybersecurity Awareness bot.";
            string learnMessage = " What would you like to learn about?";

            int boxWidth = Math.Max(welcomeMessage.Length, learnMessage.Length) + 4;
            string border = new string('═', boxWidth);

            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine($"╔{border}╗");
            TypeText($"║ {welcomeMessage.PadRight(boxWidth - 2)} ║");
            TypeText($"║ {learnMessage.PadRight(boxWidth - 2)} ║");
            Console.WriteLine($"╚{border}╝");
            Console.ResetColor();
            Console.WriteLine();
        }

        public void DisplayGoodbyeMessage(string userName)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            TypeText($"Thank you for chatting, {userName}! Come Back Soon! :)");
            Console.ResetColor();
        }

        public void DisplayInvalidChoiceMessage()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            TypeText("I'm not sure I understand. Can you try rephrasing by typing a valid topic (e.g., phishing, password safety, suspicious links).");
            Console.ResetColor();
        }

        public void DisplayTipsWithBox(string[] lines, ConsoleColor borderColor)
        {
            int width = 0;
            foreach (string line in lines)
                if (line.Length > width) width = line.Length;

            width += 4;
            string border = new string('═', width);

            Console.ForegroundColor = borderColor;
            Console.WriteLine($"╔{border}╗");
            foreach (string line in lines)
            {
                Console.WriteLine($"║ {line.PadRight(width - 2)} ║");
            }
            Console.WriteLine($"╚{border}╝\n");
            Console.ResetColor();
        }
    }
}