using System;
using System.IO;
using System.Threading.Tasks;
using SkiaSharp;
using SkiaSharp.Views.Desktop;

namespace MathCaptchaExample
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var captchaImage = GenerateMathCaptcha();

            // Lưu hình ảnh captcha vào một tệp
            await File.WriteAllBytesAsync("math_captcha.png", captchaImage);
        }

        static byte[] GenerateMathCaptcha()
        {
            var random = new Random();
            var number1 = random.Next(1, 10);
            var number2 = random.Next(1, 10);
            var answer = number1 + number2;

            var question = $"{number1} + {number2} = ?";

            using (var surface = SKSurface.Create(new SKImageInfo(200, 100)))
            {
                var canvas = surface.Canvas;

                // Vẽ nền trắng
                canvas.Clear(SKColors.White);

                // Vẽ câu hỏi
                using (var paint = new SKPaint())
                {
                    paint.Color = SKColors.Black;
                    paint.TextSize = 40;
                    paint.IsAntialias = true;

                    canvas.DrawText(question, 20, 60, paint);
                }

                // TODO: Có thể thêm thêm các yếu tố như đường viền, màu sắc, ...

                // Lưu hình ảnh SKSurface thành mảng byte
                using (var image = surface.Snapshot())
                using (var data = image.Encode(SKEncodedImageFormat.Png, 100))
                using (var stream = new MemoryStream())
                {
                    data.SaveTo(stream);
                    return stream.ToArray();
                }
            }
        }
    }
}
