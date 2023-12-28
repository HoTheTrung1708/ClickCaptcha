using SkiaSharp;

namespace ClickableCaptcha.Questions
{
    /// Math question, prompts users to think about the correct answer through a simple mathematical problem
    /// (This class can be extended through random methods)
    public class MathQuestion : AbsQuestion
    {
        public MathQuestion((string, SKColor)[] colorDict)
            : base(colorDict)
        {

        }

        public override (string, string)[] CandidateList => new (string, string)[]
            {
                ("Multiple of 3","3"),
                ("Multiple of 3","6"),
                ("Multiple of 3","9"),
                ("Multiple of 3","12"),
                ("Multiple of 3","18"),
                ("Multiple of 3","21"),
                ("Multiple of 3","24"),
                ("Multiple of 3","27"),
                ("Multiple of 3","42"),

                ("Multiple of 5","5"),
                ("Multiple of 5","10"),
                ("Multiple of 5","20"),
                ("Multiple of 5","25"),
                ("Multiple of 5","35"),
                ("Multiple of 5","40"),
                ("Multiple of 5","45"),
            };

        public override bool CheckAnswerValue => false;
    }
}
