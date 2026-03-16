namespace ArthurKnight.Core
{
    public interface IFrameRunner
    {
        float UpdateInterval { get; }
        bool IsActive { get; }

        void Framer(in IFrame frame) { }
        void FixedFramer(in IFrame frame) { }
        void LateFramer(in IFrame frame) { }
    }
}