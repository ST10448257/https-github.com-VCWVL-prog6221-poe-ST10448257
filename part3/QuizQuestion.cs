using System;

namespace CyberSecurityChatBot
{
    public class QuizQuestion
    {
        public string Question { get; set; }
        public string[] Options { get; set; }
        public int CorrectOptionIndex { get; set; }
        public string Explanation { get; set; }
        public string Category { get; set; }
        public bool IsTrueFalseQuestion => Options.Length == 2 &&
                                          Options[0].Equals("True", StringComparison.OrdinalIgnoreCase) &&
                                          Options[1].Equals("False", StringComparison.OrdinalIgnoreCase);

        public QuizQuestion(string question, string[] options, int correctOptionIndex, string explanation, string category = "General")
        {
            Question = question;
            Options = options;
            CorrectOptionIndex = correctOptionIndex;
            Explanation = explanation;
            Category = category;
        }

        public bool IsCorrectAnswer(string answer)
        {
            if (string.IsNullOrWhiteSpace(answer)) return false;

            // Handle true/false questions
            if (IsTrueFalseQuestion)
            {
                if (bool.TryParse(answer, out bool boolAnswer))
                {
                    return (boolAnswer && CorrectOptionIndex == 0) ||
                           (!boolAnswer && CorrectOptionIndex == 1);
                }
                return answer.Equals(Options[CorrectOptionIndex], StringComparison.OrdinalIgnoreCase);
            }

            // Check if answer is a letter (A, B, C, D)
            if (answer.Length == 1 && char.IsLetter(answer[0]))
            {
                int index = char.ToUpper(answer[0]) - 'A';
                return index == CorrectOptionIndex;
            }

            // Check if answer is a number (1, 2, 3, 4)
            if (int.TryParse(answer, out int numericAnswer))
            {
                return numericAnswer - 1 == CorrectOptionIndex;
            }

            // Check if answer matches the text of the correct option
            return answer.Trim().Equals(Options[CorrectOptionIndex], StringComparison.OrdinalIgnoreCase);
        }

        public string GetFormattedQuestion()
        {
            string result = $"{Question}\n\n";

            for (int i = 0; i < Options.Length; i++)
            {
                result += $"{(char)('A' + i)}) {Options[i]}\n";
            }

            return result;
        }

        public string GetCorrectAnswerText()
        {
            return Options[CorrectOptionIndex];
        }

        public string GetCorrectAnswerLetter()
        {
            return ((char)('A' + CorrectOptionIndex)).ToString();
        }
    }
}