using ClickableCaptcha.Questions;

using SkiaSharp;

namespace ClickableCaptcha
{
    public class CaptchaGenerate
    {
        public static (string colorName, SKColor color)[] ColorDict = new (string colorName, SKColor color)[]
        {
            ("đỏ",      SKColors.Red),
            ("lục",     SKColors.Green),
            ("lam",     SKColors.Blue),
            ("đỏ",      SKColors.Red),
            ("lục",     SKColors.Green),
            ("lam",     SKColors.Blue),
            ("đỏ",      SKColors.Red),
            ("lục",     SKColors.Green),
            ("lam",     SKColors.Blue),
            ("đỏ",      SKColors.Red),
            ("lục",     SKColors.Green),
            ("lam",     SKColors.Blue),
            //("White",  SKColors.Yellow),
            ("đen",  SKColors.Black),
            //("Pink",   SKColors.Pink),
            //("Purple", SKColors.Purple),
            //("Gray",   SKColors.Gray),
        };

        readonly ICaptchaQuestion[] _questions;
        readonly Random _random;

        readonly int    _gridCount;
        readonly int    _gridMargin;
        readonly int    _gridSize;
        readonly int    _gridStrokeWidth;
        readonly int    _textFontSize;
        readonly string _fontName;

        /// Captcha generator constructor (Changing values in this constructor requires updating hardcoded width and height parameters in the frontend)
        /// <param name="questions"      >List of questions, can be customized and extended, but not recommended to have too many</param>
        /// <param name="gridSize"       >Size of each grid cell (pixels)</param>
        /// <param name="gridMargin"     >Margin around the grid (pixels)</param>
        /// <param name="gridCount"      >Number of grid cells (gridCount * gridCount)</param>
        /// <param name="gridStrokeWidth">Width of the grid lines</param>
        /// <param name="textFontSize"   >Default font size</param>
        /// <param name="fontName"       >Font name, it is recommended not to use Microsoft YaHei; if the generated captcha looks like "口口口," then change it to a font supported on your machine</param>
        public CaptchaGenerate(ICaptchaQuestion[] questions,
            int gridSize        = 30,
            int gridMargin      = 20,
            int gridCount       = 6,
            int gridStrokeWidth = 1, 
            int textFontSize    = 14,
            string fontName     = "Source Han Sans CN")
        {
            _questions = questions;
            _random = new Random();

            _gridSize        = gridSize;
            _gridMargin      = gridMargin;
            _gridCount       = gridCount;
            _gridStrokeWidth = gridStrokeWidth;
            _textFontSize    = textFontSize;
            _fontName        = fontName;
        }

        public static ICaptchaQuestion[] GetDefaultQuestions()
        {
            return new ICaptchaQuestion[]
            {
                new MathQuestion  (ColorDict),
                new ShapeQuestion (ColorDict),
                new SearchQuestion(ColorDict),
                new ArrowQuestion (ColorDict),
            };
        }

        /// Get the graphical captcha
        /// <param name="questionIndex">Index of the selected question</param>
        /// <param name="dysopsia">Visual impairment mode</param>
        /// <returns></returns>
        /// <exception cref="IndexOutOfRangeException"></exception>
        public (byte[], string) GetCaptcha(int questionIndex, bool dysopsia)
        {
            if (questionIndex >= _questions.Length)
                throw new IndexOutOfRangeException();

            // Save answer coordinates
            CapthcaPoint[]? answerList = null;

            int width  = (_gridSize * _gridCount) + (_gridMargin * 2);
            int height = width + (_textFontSize * 2);

            // Create bitmap
            using (SKBitmap image2d = new SKBitmap(width, height, SKColorType.Bgra8888, SKAlphaType.Premul))
            {
                // Create canvas
                using (SKCanvas canvas = new SKCanvas(image2d))
                {
                    // Fill background color with white
                    canvas.DrawColor(SKColors.White);

                    CapthcaPoint[] positions;

                    // Draw border and grid
                    using (SKPaint borderPaint = new SKPaint())
                    {
                        borderPaint.Color = SKColors.Black;
                        borderPaint.StrokeWidth = _gridStrokeWidth;

                        // Draw square borders
                        positions = DrawRectGrid(canvas, borderPaint);
                    }

                    // Shuffle positions
                    Queue<CapthcaPoint> positionQueues = DisturbPosition(positions);

                    for (int i = 0; i < _questions.Length; i++)
                    {
                        // Draw candidate answers
                        var answer = _questions[i].DrawAnswerCandidate(
                            canvas,
                            AssignPosition(positionQueues, positions.Length, i == _questions.Length - 1).ToArray(),
                            _gridSize,
                            _gridSize,
                            _fontName,
                            Convert.ToInt32(_gridSize * 0.5),
                            dysopsia);
                        if (i == questionIndex)
                            answerList = answer;
                    }

                    // Draw question title
                    DrawQuesitonTitle(canvas, _questions[questionIndex].GetQuestionName());

                    // TODO: Additional noise and random lines can be added to enhance the difficulty of machine recognition

                    // Return image bytes
                    using (SKImage img = SKImage.FromBitmap(image2d))
                    {
                        using (SKData p = img.Encode(SKEncodedImageFormat.Png, 100))
                        {
                            return (p.ToArray(), SerializeAnswerList(answerList));
                        }
                    }
                }
            }
        }

        private string SerializeAnswerList(CapthcaPoint[] points)
        {
            return string.Join(';', points.Select(p => $"{p.X},{p.Y}"));
        }

        /// Shuffle positions and return as a queue
        /// <param name="positions"></param>
        /// <returns></returns>
        private Queue<CapthcaPoint> DisturbPosition(CapthcaPoint[] positions)
        {
            (int x, int y, int rand)[] rands = positions.Select(p => (p.X, p.Y, _random.Next(1000, 9999))).ToArray();

            return new Queue<CapthcaPoint>(rands.OrderByDescending(p => p.rand).Select(p => new CapthcaPoint() { X = p.x, Y = p.y }));
        }

        /// Assign coordinates
        /// <param name="positionQueues"></param>
        /// <param name="totalPosition"></param>
        /// <param name="last"></param>
        private IEnumerable<CapthcaPoint> AssignPosition(Queue<CapthcaPoint> positionQueues, int totalPosition, bool last)
        {
            if (last)
            {
                while (positionQueues.TryDequeue(out CapthcaPoint pos))
                {
                    yield return pos;
                }
            }
            else
            {
                int min = totalPosition / _questions.Length / 2;
                int max = (totalPosition / _questions.Length) + 3;

                int dequeueCount = _random.Next(min, max);
                for (int i = 0; i < dequeueCount; i++)
                {
                    if (positionQueues.TryDequeue(out CapthcaPoint pos))
                    {
                        yield return pos;
                    }
                }
            }
        }
        /// Draw question title
        /// <param name="canvas"></param>
        /// <param name="text"></param>
        private void DrawQuesitonTitle(SKCanvas canvas, string text)
        {
            using (SKPaint paint = new SKPaint())
            {
                paint.IsAntialias = true;
                paint.Color = ColorDict[_random.Next(0, ColorDict.Length - 1)].color;
                paint.Typeface = SKTypeface.FromFamilyName(_fontName, SKFontStyleWeight.Bold, SKFontStyleWidth.Normal, SKFontStyleSlant.Upright);
                paint.TextSize = _textFontSize;

                canvas.DrawText(text, _gridMargin, (_gridSize * _gridCount) + (_gridMargin * 2) + _textFontSize, paint);
            }
        }
        /// Draw square grid
        /// <param name="canvas"></param>
        /// <param name="borderPaint"></param>
        private CapthcaPoint[] DrawRectGrid(SKCanvas canvas, SKPaint borderPaint)
        {
            List<CapthcaPoint> positions = new List<CapthcaPoint>(_gridCount * _gridCount);

            // Draw grid lines (horizontal)
            for (int i = 0; i <= _gridCount; i++)
            {
                canvas.DrawLine(
                    new SKPoint(_gridMargin, (i * _gridSize) + _gridMargin),
                    new SKPoint((_gridSize * _gridCount) + _gridMargin, (i * _gridSize) + _gridMargin),
                    borderPaint);
            }

            // Draw grid lines (vertical)
            for (int i = 0; i <= _gridCount; i++)
            {
                canvas.DrawLine(
                    new SKPoint((i * _gridSize) + _gridMargin, _gridMargin),
                    new SKPoint((i * _gridSize) + _gridMargin, (_gridSize * _gridCount) + _gridMargin + _gridStrokeWidth),
                    borderPaint);
            }
            for (int x = 0; x < _gridCount; x++)
            {
                for (int y = 0; y < _gridCount; y++)
                {
                    positions.Add(new CapthcaPoint()
                    {
                        X = (x * _gridSize) + _gridMargin,
                        Y = (y * _gridSize) + _gridMargin + _gridSize
                    });
                }
            }
            return positions.ToArray();
        }
    }
}
