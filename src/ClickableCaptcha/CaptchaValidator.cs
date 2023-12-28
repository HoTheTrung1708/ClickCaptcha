namespace ClickableCaptcha
{
    /// Graphic captcha validator
    public class CaptchaValidator
    {
        readonly CapthcaPoint[] _answerList;

        /// Constructs a graphic captcha validator
        /// <param name="realAnswer">The real answer</param>
        /// <exception cref="ArgumentNullException">Real answer cannot be NULL</exception>
        public CaptchaValidator(string realAnswer)
        {
            if (string.IsNullOrEmpty(realAnswer))
                throw new ArgumentNullException(nameof(realAnswer));

            _answerList = ToCapthcaPointArray(realAnswer);
        }
        /// Checks if the user's answer is correct based on the grid size and the real answer
        /// <param name="userAnswer"></param>
        /// <param name="GridSize"></param>
        /// <returns></returns>
        public bool Checking(string userAnswer, int GridSize)
        {
            if (_answerList == null)
                return false;

            if (string.IsNullOrEmpty(userAnswer))
                return false;

            CapthcaPoint[] inputs = ToCapthcaPointArray(userAnswer);
            if (inputs.Length != _answerList.Length)
                return false;

            return _answerList.All(
                answer => inputs.Any(
                    input => answer.X <= input.X && answer.X + GridSize >= input.X && answer.Y - GridSize <= input.Y && answer.Y >= input.Y));
        }

        private CapthcaPoint[] ToCapthcaPointArray(string code)
        {
            return code.Replace("\"", "")
                .Split(';')
                .Select(p =>
                {
                    string[] xy = p.Split(',');
                    if (xy.Length != 2)
                        return new CapthcaPoint();

                    if (int.TryParse(xy[0], out int inputX) && int.TryParse(xy[1], out int inputY))
                        return new CapthcaPoint() { X = inputX, Y = inputY };
                    else
                        return new CapthcaPoint();
                }).ToArray();
        }
    }
}
