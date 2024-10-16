namespace ChuuniExtension.CountdownTool
{
    public interface ICountdown
    {
        float DeltaTime{ get; }
        bool TimesUp{ get; }

        void Update();
        void Reset();
    }
}
