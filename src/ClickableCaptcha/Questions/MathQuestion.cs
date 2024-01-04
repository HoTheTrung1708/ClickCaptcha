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
                ("Bội số của 3","3"),
                ("Bội số của 3","6"),
                ("Bội số của 3","9"),
                ("Bội số của 3","12"),
                ("Bội số của 3","18"),
                ("Bội số của 3","21"),
                ("Bội số của 3","24"),
                ("Bội số của 3","27"),
                ("Bội số của 3","42"),

                ("Bội số của 5","5"),
                ("Bội số của 5","10"),
                ("Bội số của 5","20"),
                ("Bội số của 5","25"),
                ("Bội số của 5","35"),
                ("Bội số của 5","40"),
                ("Bội số của 5","45"),
            };

        public override bool CheckAnswerValue => false;
    }
}
