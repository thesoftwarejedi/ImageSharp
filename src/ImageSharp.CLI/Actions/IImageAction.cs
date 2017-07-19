namespace ImageSharp.CLI.Actions
{
    public abstract class ImageAction
    {
        public abstract void RunAction(ImageOperationContext imageOperationContext);
    }
}