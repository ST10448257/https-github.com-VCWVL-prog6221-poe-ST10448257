namespace CyberSecurityChatBot
{
    public class ResponseService
    {
        public string GetResponse(string input)
        {
            string lower = input.ToLower();
            return lower switch
            {
                "phishing" => "Phishing is a method attackers use to trick you into giving up personal information.",
                "password safety" => "Use strong, unique passwords and consider using a password manager.",
                "suspicious links" => "Avoid clicking on suspicious links. Hover to preview the URL first.",
                "privacy settings" => "Review privacy settings regularly on your apps and social platforms.",
                "firewalls" => "Firewalls help block unauthorized access to your computer or network.",
                _ => "Sorry, I don't understand that. Please type a known topic or command like 'quiz'."
            };
        }
    }
}
