namespace ArthurKnight.Core
{
    public interface IFrame
    {
        public float DeltaTime { get; }
        public float Tick { get; }
        public float FixedDeltaTime { get; }
        public float UnscaledDeltaTime { get; }
        public int FrameCount { get; }
    }
}