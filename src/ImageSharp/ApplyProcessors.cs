// <copyright file="RotateFlip.cs" company="James Jackson-South">
// Copyright (c) James Jackson-South and contributors.
// Licensed under the Apache License, Version 2.0.
// </copyright>

namespace ImageSharp
{
    using System;

    using ImageSharp.PixelFormats;

    using ImageSharp.Processing;

    /// <summary>
    /// Extension methods for the <see cref="Image{TPixel}"/> type.
    /// </summary>
    public static partial class ImageExtensions
    {
        /// <summary>
        /// Mutates the image by applying the operations to it.
        /// </summary>
        /// <param name="source">The image to rotate, flip, or both.</param>
        /// <param name="operations">The operations to perform on the source.</param>
        public static void Mutate(this IImage source, Action<IImageOperations> operations)
        {
            Guard.NotNull(operations, nameof(operations));

            // TODO: add parameter to Configuration to configure how this is created, create an IImageOperationsFactory that cna be used to switch this out with a fake for testing
            var operationsRunner = new ImageOperations(source);
            operations(operationsRunner);
        }

        /// <summary>
        /// Mutates the image by applying the operations to it.
        /// </summary>
        /// <param name="source">The image to rotate, flip, or both.</param>
        /// <param name="operations">The operations to perform on the source.</param>
        public static void Mutate(this IImage source, params IImageProcessor[] operations)
        {
            Guard.NotNull(operations, nameof(operations));

            // TODO: add parameter to Configuration to configure how this is created, create an IImageOperationsFactory that cna be used to switch this out with a fake for testing
            var operationsRunner = new ImageOperations(source);
            operationsRunner.ApplyProcessors(operations);
        }

        /// <summary>
        /// Mutates the image by applying the operations to it.
        /// </summary>
        /// <typeparam name="TPixel">The pixel format.</typeparam>
        /// <param name="source">The image to rotate, flip, or both.</param>
        /// <param name="operations">The operations to perform on the source.</param>
        /// <returns>Anew Image which has teh data from the <paramref name="source"/> but with the <paramref name="operations"/> applied.</returns>
        public static Image<TPixel> Generate<TPixel>(this Image<TPixel> source, Action<IImageOperations> operations)
            where TPixel : struct, IPixel<TPixel>
        {
            Guard.NotNull(operations, nameof(operations));
            var generated = new Image<TPixel>(source);

            // TODO: add parameter to Configuration to configure how this is created, create an IImageOperationsFactory that cna be used to switch this out with a fake for testing
            var operationsRunner = new ImageOperations(generated);
            operations(operationsRunner);
            return generated;
        }

        /// <summary>
        /// Mutates the image by applying the operations to it.
        /// </summary>
        /// <param name="source">The image to rotate, flip, or both.</param>
        /// <param name="operations">The operations to perform on the source.</param>
        /// <returns>Anew Image which has teh data from the <paramref name="source"/> but with the <paramref name="operations"/> applied.</returns>
        public static IImage Generate(this IImage source, Action<IImageOperations> operations)
        {
            Guard.NotNull(operations, nameof(operations));
            var generated = source.Clone();

            // TODO: add parameter to Configuration to configure how this is created, create an IImageOperationsFactory that cna be used to switch this out with a fake for testing
            var operationsRunner = new ImageOperations(generated);
            operations(operationsRunner);
            return generated;
        }

        /// <summary>
        /// Mutates the image by applying the operations to it.
        /// </summary>
        /// <param name="source">The image to rotate, flip, or both.</param>
        /// <param name="operations">The operations to perform on the source.</param>
        /// <returns>Anew Image which has teh data from the <paramref name="source"/> but with the <paramref name="operations"/> applied.</returns>
        public static IImage Generate(this IImage source, params IImageProcessor[] operations)
        {
            Guard.NotNull(operations, nameof(operations));
            var generated = source.Clone();

            // TODO: add parameter to Configuration to configure how this is created, create an IImageOperationsFactory that cna be used to switch this out with a fake for testing
            var operationsRunner = new ImageOperations(generated);
            operationsRunner.ApplyProcessors(operations);
            return generated;
        }

        /// <summary>
        /// Mutates the image by applying the operations to it.
        /// </summary>
        /// <typeparam name="TPixel">The pixel format.</typeparam>
        /// <param name="source">The image to rotate, flip, or both.</param>
        /// <param name="operations">The operations to perform on the source.</param>
        /// <returns>Anew Image which has teh data from the <paramref name="source"/> but with the <paramref name="operations"/> applied.</returns>
        public static Image<TPixel> Generate<TPixel>(this Image<TPixel> source, params IImageProcessor[] operations)
            where TPixel : struct, IPixel<TPixel>
        {
            Guard.NotNull(operations, nameof(operations));
            var generated = new Image<TPixel>(source);

            // TODO: add parameter to Configuration to configure how this is created, create an IImageOperationsFactory that cna be used to switch this out with a fake for testing
            var operationsRunner = new ImageOperations(generated);
            operationsRunner.ApplyProcessors(operations);
            return generated;
        }

        /// <summary>
        /// Mutates the image by applying the operations to it.
        /// </summary>
        /// <param name="source">The image to rotate, flip, or both.</param>
        /// <param name="operation">The operations to perform on the source.</param>
        /// <returns>returns the current optinoatins class to allow chaining of oprations.</returns>
        public static IImageOperations Run(this IImageOperations source, Action<IImage> operation)
            => source.ApplyProcessor(new DelegateImageProcessor(operation));
    }
}
