using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ImageSharp.Memory;
using ImageSharp.PixelFormats;
using SixLabors.Primitives;

namespace ImageSharp.CLI.Actions
{
    public class Compare : ImageAction
    {
        public string Source { get; set; }

        public float Threshold { get; set; }
        
        public override void RunAction(ImageOperationContext imageOperationContext)
        {
            // we do a test thing here and output to appveyor if we can.
            var path = Source.Interpolate(imageOperationContext);
            var full = Path.GetFullPath(Path.Combine(".", path));
            if (!File.Exists(full))
            {
                throw new Exception($"comparision image could not be found at '{path}'");
            }
            using (var src = Image.Load(full))
            {
                var diff = ImageComparer.PercentageDifference(src, imageOperationContext.Image);
                if(diff > Threshold)
                {
                    throw new Exception($"images differ by {diff:P} which is greater then the threshold of {Threshold: P}");
                }
            }

        }

    }
    internal static class ImageComparer
    {
        const int DefaultScalingFactor = 32; // This is means the images get scaled into a 32x32 image to sample pixels
        const int DefaultSegmentThreshold = 3; // The greyscale difference between 2 segements my be > 3 before it influences the overall difference
        const float DefaultImageThreshold = 0.000F; // After segment thresholds the images must have no differences

        /// <summary>
        /// Does a visual comparison between 2 images and then and returns the percentage diffence between the 2
        /// </summary>
        /// <typeparam name="TPixelA">The color of the source image</typeparam>
        /// <typeparam name="TPixelB">The color type for the target image</typeparam>
        /// <param name="source">The source image</param>
        /// <param name="target">The target image</param>
        /// <param name="segmentThreshold">
        /// The threshold of the individual segments before it acumulates towards the overall difference.
        /// The default undefined value is <see cref="DefaultSegmentThreshold"/>
        /// </param>
        /// <param name="scalingFactor">
        /// This is a sampling factor we sample a grid of average pixels <paramref name="scalingFactor"/> width by <paramref name="scalingFactor"/> high
        /// The default undefined value is <see cref="ImageComparer.DefaultScalingFactor"/>
        /// </param>
        /// <returns>Returns a number from 0 - 1 which represents the difference focter between the images.</returns>
        public static float PercentageDifference<TPixelA, TPixelB>(this Image<TPixelA> source, Image<TPixelB> target, byte segmentThreshold = DefaultSegmentThreshold, int scalingFactor = DefaultScalingFactor)
            where TPixelA : struct, IPixel<TPixelA>
            where TPixelB : struct, IPixel<TPixelB>
        {
            // code adapted from https://www.codeproject.com/Articles/374386/Simple-image-comparison-in-NET
            Fast2DArray<byte> differences = GetDifferences(source, target, scalingFactor);

            int diffPixels = 0;

            foreach (byte b in differences.Data)
            {
                if (b > segmentThreshold) { diffPixels++; }
            }

            return diffPixels / (float)(scalingFactor * scalingFactor);
        }

        private static Fast2DArray<byte> GetDifferences<TPixelA, TPixelB>(Image<TPixelA> source, Image<TPixelB> target, int scalingFactor)
            where TPixelA : struct, IPixel<TPixelA>
            where TPixelB : struct, IPixel<TPixelB>
        {
            var differences = new Fast2DArray<byte>(scalingFactor, scalingFactor);
            Fast2DArray<byte> firstGray = source.GetGrayScaleValues(scalingFactor);
            Fast2DArray<byte> secondGray = target.GetGrayScaleValues(scalingFactor);

            for (int y = 0; y < scalingFactor; y++)
            {
                for (int x = 0; x < scalingFactor; x++)
                {
                    int diff = firstGray[x, y] - secondGray[x, y];
                    differences[x, y] = (byte)Math.Abs(diff);
                }
            }

            return differences;
        }

        private static Fast2DArray<byte> GetGrayScaleValues<TPixelA>(this Image<TPixelA> source, int scalingFactor)
            where TPixelA : struct, IPixel<TPixelA>
        {
            byte[] buffer = new byte[3];
            using (Image<TPixelA> img = new Image<TPixelA>(source).Resize(scalingFactor, scalingFactor).Grayscale())
            {
                using (PixelAccessor<TPixelA> pixels = img.Lock())
                {
                    var grayScale = new Fast2DArray<byte>(scalingFactor, scalingFactor);
                    for (int y = 0; y < scalingFactor; y++)
                    {
                        for (int x = 0; x < scalingFactor; x++)
                        {
                            pixels[x, y].ToXyzBytes(buffer, 0);
                            grayScale[x, y] = buffer[0];
                        }
                    }

                    return grayScale;
                }
            }
        }
    }
}
