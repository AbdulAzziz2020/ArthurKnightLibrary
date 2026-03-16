namespace ArthurKnight.Core
{
    public interface IFrameDispatcher
    {
        void Patch(IFrameRunner runner);
        void Dispatch(IFrameRunner runner);
    }
}