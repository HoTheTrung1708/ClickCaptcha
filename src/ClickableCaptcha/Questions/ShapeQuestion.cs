using SkiaSharp;

namespace ClickableCaptcha.Questions
{
    /// Shape question, prompts users to find the corresponding shape patterns based on four distinct shapes
    public class ShapeQuestion : AbsQuestion
    {
        public ShapeQuestion((string, SKColor)[] colorDict)
            : base(colorDict)
        {

        }

        public override (string, string)[] CandidateList => new (string, string)[]
            {
                ("Solid Triangle" ,"▲"),
                ("Hollow Triangle","△"),
                ("Solid Square"   ,"■"),
                ("Hollow Square"  ,"□"),
                ("Solid Circle"   ,"●"),
                ("Hollow Circle"  ,"○"),
                ("Solid Diamond"  ,"◆"),
                ("Hollow Diamond" ,"◇"),
            };

        public override bool CheckAnswerValue => true;
    }
}
