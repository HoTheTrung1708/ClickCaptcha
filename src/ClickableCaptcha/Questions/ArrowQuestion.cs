using SkiaSharp;

namespace ClickableCaptcha.Questions
{
    /// Arrow question, prompts users to identify the correct direction of the arrow based on the text
    public class ArrowQuestion : AbsQuestion
    {
        public ArrowQuestion((string, SKColor)[] colorDict)
            : base(colorDict)
        {

        }

        public override (string, string)[] CandidateList => new (string, string)[]
            {
                ("Upward Arrow",   "↑"),
                ("Downward Arrow", "↓"),
                ("Leftward Arrow", "←"),
                ("Rightward Arrow","→"),
            };

        public override bool CheckAnswerValue => true;
    }
}
