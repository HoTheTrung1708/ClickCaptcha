using SkiaSharp;

namespace ClickableCaptcha.Questions
{
    /// Case-sensitive letter search question, prompts users to find the corresponding uppercase and Chữ cái thường củas based on the clues
    public class SearchQuestion : AbsQuestion
    {
        public SearchQuestion((string, SKColor)[] colorDict)
            : base(colorDict)
        {

        }

        public override (string, string)[] CandidateList => new (string, string)[]
            {
                ("Chữ cái thường của A","a"),
                ("Chữ cái thường của B","b"),
                ("Chữ cái thường của D","d"),
                ("Chữ cái thường của N","n"),
                ("Chữ cái thường của E","e"),
                ("Chữ cái thường của F","f"),
                ("Chữ cái thường của G","g"),
                ("Chữ cái thường của Q","q"),
                ("Chữ cái thường của R","r"),
                ("Chữ cái thường của T","t"),
                ("Chữ cái thường của Y","y"),
                ("Chữ cái thường của H","h"),
                ("Chữ cái in hoa của a","A"),
                ("Chữ cái in hoa của b","B"),
                ("Chữ cái in hoa của c","D"),
                ("Chữ cái in hoa của d","N"),
                ("Chữ cái in hoa của e","E"),
                ("Chữ cái in hoa của f","F"),
                ("Chữ cái in hoa của g","G"),
                ("Chữ cái in hoa của q","Q"),
                ("Chữ cái in hoa của r","R"),
                ("Chữ cái in hoa của t","T"),
                ("Chữ cái in hoa của y","Y"),
                ("Chữ cái in hoa của h","H"),
            };

        public override bool CheckAnswerValue => true;
    }
}
