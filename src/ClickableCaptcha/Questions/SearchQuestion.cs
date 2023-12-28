using SkiaSharp;

namespace ClickableCaptcha.Questions
{
    /// Case-sensitive letter search question, prompts users to find the corresponding uppercase and lowercase letters based on the clues
    public class SearchQuestion : AbsQuestion
    {
        public SearchQuestion((string, SKColor)[] colorDict)
            : base(colorDict)
        {

        }

        public override (string, string)[] CandidateList => new (string, string)[]
            {
                ("Lowercase letter A","a"),
                ("Lowercase letter B","b"),
                ("Lowercase letter D","d"),
                ("Lowercase letter N","n"),
                ("Lowercase letter E","e"),
                ("Lowercase letter F","f"),
                ("Lowercase letter G","g"),
                ("Lowercase letter Q","q"),
                ("Lowercase letter R","r"),
                ("Lowercase letter T","t"),
                ("Lowercase letter Y","y"),
                ("Lowercase letter H","h"),
                ("Uppercase letter a","A"),
                ("Uppercase letter b","B"),
                ("Uppercase letter c","D"),
                ("Uppercase letter d","N"),
                ("Uppercase letter e","E"),
                ("Uppercase letter f","F"),
                ("Uppercase letter g","G"),
                ("Uppercase letter q","Q"),
                ("Uppercase letter r","R"),
                ("Uppercase letter t","T"),
                ("Uppercase letter y","Y"),
                ("Uppercase letter h","H"),
            };

        public override bool CheckAnswerValue => true;
    }
}
