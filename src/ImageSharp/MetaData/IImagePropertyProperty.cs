namespace ImageSharp
{
    public interface IImageProperty
    {
        IImagePropertyTag Tag { get; }

        object Value { get; }
    }
}