namespace AnkhMorpork.Builders
{
    public interface INpcBuilder<T> where T : class
    {
        /// <summary>
        /// To set the NPC's name.
        /// </summary>
        /// <param name="name">The NPC's name.</param>
        void SetName(string name);

        /// <summary>
        /// To clear NPC's settings and create a new NPC object.
        /// </summary>
        void Reset();

        /// <summary>
        /// Get the NPC object.
        /// </summary>
        /// <returns>The NPC object.</returns>
        T GetNpc();
    }
}
