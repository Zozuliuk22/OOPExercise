namespace AnkhMorpork.Builders
{
    public interface INpcBuilder<T> where T : class
    {
        void SetName(string name);

        void Reset();

        T GetNpc();
    }
}
