using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace Forum.Helpers
{
    public static class ImageHelper
    {
        /// <summary>
        /// 按照短边缩放图像
        /// </summary>
        /// <param name="sourceImagePath">源图像路径</param>
        /// <param name="destinationImagePath">目标图像路径</param>
        /// <param name="targetShortSideLength">目标短边长</param>
        public static async Task ResizeImage(string sourceImagePath, string destinationImagePath, int targetShortSideLength)
        {
            using var image = await Image.LoadAsync(sourceImagePath);

            float ratio = (float)targetShortSideLength / Math.Min(image.Width, image.Height);
            int newWidth = (int)(image.Width * ratio);
            int newHeight = (int)(image.Height * ratio);

            image.Mutate(x => x.Resize(new ResizeOptions
            {
                Size = new Size(newWidth, newHeight),
                Sampler = new SixLabors.ImageSharp.Processing.Processors.Transforms.BoxResampler()
            }));

            await image.SaveAsJpegAsync(destinationImagePath);
        }
    }
}