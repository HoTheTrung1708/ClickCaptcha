using SkiaSharp;

namespace ClickableCaptcha
{
    /// Graphic captcha question interface, can be extended by inheriting this interface for more questions
    public interface ICaptchaQuestion
    {
        string GetQuestionName();
        /// Draw candidate answers (what is drawn inside the grid)
        /// <param name="canvas">Canvas</param>
        /// <param name="candidatePositions">Positions allocated for your candidate answers, draw your candidate answers at these positions</param>
        /// <param name="width">Width that the candidate answers should be drawn</param>
        /// <param name="height">Height that the candidate answers should be drawn</param>
        /// <param name="fontFamily">Font name to be used for the candidate answers</param>
        /// <param name="fontSize">Font size to be used for the candidate answers</param>
        /// <param name="dysopsia">Whether the candidate answer is in visual impairment mode</param>
        /// <returns></returns>
        CapthcaPoint[] DrawAnswerCandidate(
            SKCanvas       canvas,
            CapthcaPoint[] candidatePositions,
            int    width,
            int    height,
            string fontFamily,
            int    fontSize = 21,
            bool   dysopsia = false); 
    }
}
