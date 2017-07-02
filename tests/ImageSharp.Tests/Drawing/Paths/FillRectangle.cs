
namespace ImageSharp.Tests.Drawing.Paths
{
    using System;

    using ImageSharp.Drawing.Brushes;

    using Xunit;
    using ImageSharp.Drawing;
    using ImageSharp.Drawing.Processors;
    using ImageSharp.PixelFormats;

    public class FillRectangle : IDisposable
    {
        GraphicsOptions noneDefault = new GraphicsOptions();
        Color color = Color.HotPink;
        SolidBrush brush = Brushes.Solid(Color.HotPink);
        SixLabors.Primitives.Rectangle rectangle = new SixLabors.Primitives.Rectangle(10, 10, 77, 76);

        private ProcessorWatchingImage img;

        public FillRectangle()
        {
            this.img = new Paths.ProcessorWatchingImage(10, 10);
        }

        public void Dispose()
        {
            img.Dispose();
        }

        [Fact]
        public void CorrectlySetsBrushAndRectangle()
        {
            img.Mutate(x => x.Fill(brush, rectangle));

            Assert.NotEmpty(img.ProcessorApplications);
            FillRegionProcessor processor = Assert.IsType<FillRegionProcessor>(img.ProcessorApplications[0].processor);

            Assert.Equal(GraphicsOptions.Default, processor.Options);

            ShapeRegion region = Assert.IsType<ShapeRegion>(processor.Region);
            SixLabors.Shapes.RectangularePolygon rect = Assert.IsType<SixLabors.Shapes.RectangularePolygon>(region.Shape);
            Assert.Equal(rect.Location.X, rectangle.X);
            Assert.Equal(rect.Location.Y, rectangle.Y);
            Assert.Equal(rect.Size.Width, rectangle.Width);
            Assert.Equal(rect.Size.Height, rectangle.Height);

            Assert.Equal(brush, processor.Brush);
        }

        [Fact]
        public void CorrectlySetsBrushRectangleAndOptions()
        {
            img.Mutate(x => x.Fill(brush, rectangle, noneDefault));

            Assert.NotEmpty(img.ProcessorApplications);
            FillRegionProcessor processor = Assert.IsType<FillRegionProcessor>(img.ProcessorApplications[0].processor);

            Assert.Equal(noneDefault, processor.Options);

            ShapeRegion region = Assert.IsType<ShapeRegion>(processor.Region);
            SixLabors.Shapes.RectangularePolygon rect = Assert.IsType<SixLabors.Shapes.RectangularePolygon>(region.Shape);
            Assert.Equal(rect.Location.X, rectangle.X);
            Assert.Equal(rect.Location.Y, rectangle.Y);
            Assert.Equal(rect.Size.Width, rectangle.Width);
            Assert.Equal(rect.Size.Height, rectangle.Height);

            Assert.Equal(brush, processor.Brush);
        }

        [Fact]
        public void CorrectlySetsColorAndRectangle()
        {
            img.Mutate(x => x.Fill(color, rectangle));

            Assert.NotEmpty(img.ProcessorApplications);
            FillRegionProcessor processor = Assert.IsType<FillRegionProcessor>(img.ProcessorApplications[0].processor);

            Assert.Equal(GraphicsOptions.Default, processor.Options);

            ShapeRegion region = Assert.IsType<ShapeRegion>(processor.Region);
            SixLabors.Shapes.RectangularePolygon rect = Assert.IsType<SixLabors.Shapes.RectangularePolygon>(region.Shape);
            Assert.Equal(rect.Location.X, rectangle.X);
            Assert.Equal(rect.Location.Y, rectangle.Y);
            Assert.Equal(rect.Size.Width, rectangle.Width);
            Assert.Equal(rect.Size.Height, rectangle.Height);

            SolidBrush brush = Assert.IsType<SolidBrush>(processor.Brush);
            Assert.Equal(color, brush.Color);
        }

        [Fact]
        public void CorrectlySetsColorRectangleAndOptions()
        {
            img.Mutate(x => x.Fill(color, rectangle, noneDefault));

            Assert.NotEmpty(img.ProcessorApplications);
            FillRegionProcessor processor = Assert.IsType<FillRegionProcessor>(img.ProcessorApplications[0].processor);

            Assert.Equal(noneDefault, processor.Options);

            ShapeRegion region = Assert.IsType<ShapeRegion>(processor.Region);
            SixLabors.Shapes.RectangularePolygon rect = Assert.IsType<SixLabors.Shapes.RectangularePolygon>(region.Shape);
            Assert.Equal(rect.Location.X, rectangle.X);
            Assert.Equal(rect.Location.Y, rectangle.Y);
            Assert.Equal(rect.Size.Width, rectangle.Width);
            Assert.Equal(rect.Size.Height, rectangle.Height);

            SolidBrush brush = Assert.IsType<SolidBrush>(processor.Brush);
            Assert.Equal(color, brush.Color);
        }
    }
}
