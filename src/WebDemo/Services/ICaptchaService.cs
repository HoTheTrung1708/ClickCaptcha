namespace WebDemo.Services
{
    public interface ICaptchaService
    {
        /// <param name="identify"></param>
        /// <param name="dysopsia"></param>
        Task<CaptchaPayload> GenerateCaptchaAsync(bool dysopsia);

        /// <param name="captchaId"></param>
        /// <param name="answer"   ></param>
        /// <param name="retryMax" ></param>
        Task<bool> VerifyCaptchaAsync(string captchaId, string answer, int retryMax = 5);
    }

    public record class CaptchaPayload(
        byte[] ImageBytes, string CaptchaId);
}