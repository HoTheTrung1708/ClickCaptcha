using ClickableCaptcha;

using Microsoft.Extensions.Caching.Distributed;

namespace WebDemo.Services
{
    public class ClickableCaptchaService : ICaptchaService
    {
        const string CaptchaCacheKeyFormat = "CAPTCHA_{0}";
        const int GridSize = 40;

        readonly IDistributedCache _cache;

        public ClickableCaptchaService(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task<CaptchaPayload> GenerateCaptchaAsync(bool dysopsia)
        {
            string cid = Guid.NewGuid().ToString("N");

            var questions = CaptchaGenerate
                .GetDefaultQuestions();

            (byte[] imageBytes, string answer) = new CaptchaGenerate(questions, GridSize)
                .GetCaptcha(new Random().Next(questions.Length), dysopsia);

            await _cache.SetStringAsync(
            string.Format(CaptchaCacheKeyFormat, cid), $"{answer}|0");

            return new CaptchaPayload(imageBytes, cid);
        }

        public async Task<bool> VerifyCaptchaAsync(string captchaId, string answer, int retryMax = 5)
        {
            string cacheKey = string.Format(
                CaptchaCacheKeyFormat, captchaId);

            string? realAnswer = await _cache
                .GetStringAsync(cacheKey);
            if (realAnswer == null) return false;

            string[] realAnswerSplited = realAnswer.Split('|');

            string realAnswerPair = realAnswerSplited[0];
            int retryCount = Convert.ToInt32(realAnswerSplited[1]);

            bool match = new CaptchaValidator(realAnswerPair)
                .Checking(answer, GridSize);
            if (match)
            {
                // Fully matched, delete verification data
                await _cache.RemoveAsync(cacheKey);
                return true;
            }
            else if (retryCount < retryMax)
            {
                // Not matched, but less than the maximum retry count, update the error retry count
                await _cache.SetStringAsync(
                    cacheKey, $"{realAnswerPair}|{retryCount + 1}");
                return false;
            }
            else
            {
                // Not matched, and exceeds the maximum retry count, delete verification data
                await _cache.RemoveAsync(cacheKey);
                return false;
            }
        }
    }
}
