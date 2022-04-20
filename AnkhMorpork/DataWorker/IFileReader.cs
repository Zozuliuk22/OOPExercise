namespace AnkhMorpork
{
    public interface IFileReader
    {
        /// <summary>
        /// To read the file into the path.
        /// </summary>
        /// <param name="path">The path to the file that must be read.</param>
        /// <returns>Contents of the file as a string.</returns>
        string Read(string path);
    }
}
