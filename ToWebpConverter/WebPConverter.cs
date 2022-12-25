using ImageProcessor;
using ImageProcessor.Imaging.Formats;
using ImageProcessor.Plugins.WebP.Imaging.Formats;

namespace ImageConverterToWebp
{
    public class WebPConverter
    {
        public static void ConvertToWebP(string inputPath, string outputPath, int imgQualityPercent)
        {
            ISupportedImageFormat webpFormat = new WebPFormat { Quality = imgQualityPercent };
            ConvertImageToWebP(inputPath, outputPath, webpFormat);
            Console.WriteLine();
        }

        private static void ConvertImageToWebP(string inputPath, string outputPath, ISupportedImageFormat outputImageFormat)
        {
            byte[] imageBytes;
            Console.WriteLine($"Входной путь: {inputPath}");
            Console.WriteLine($"Выходной путь: {outputPath}");
            try
            {
                imageBytes = File.ReadAllBytes(inputPath);
            }
            catch (Exception e)
            {
                Console.WriteLine("Проблема с чтением файла");
                Console.WriteLine(e.StackTrace);
                Console.WriteLine(e.Message);
                Console.WriteLine();
                return;
            }

            using MemoryStream memoryStream = new(imageBytes);
            using ImageFactory imageFactory = new(preserveExifData: true);
            var load = imageFactory.Load(memoryStream);
            var format = load.Format(outputImageFormat);
            var save = format.Save(outputPath);
        }
    }
}
