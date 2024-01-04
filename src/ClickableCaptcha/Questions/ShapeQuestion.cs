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
                ("Tam giác đặc" ,"▲"),
                ("Tam giác rỗng","△"),
                ("Hình vuông đặc "   ,"■"),
                ("Hình vuông rỗng "  ,"□"),
                ("Hình tròn đặc "   ,"●"),
                ("Hình tròn rỗng"  ,"○"),
                ("Kim cương đặc"  ,"◆"),
                ("Kim cương rỗng " ,"◇"),
            };

        public override bool CheckAnswerValue => true;
    }
}
