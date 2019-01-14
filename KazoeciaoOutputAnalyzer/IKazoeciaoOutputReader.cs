namespace KazoeciaoOutputAnalyzer
{
    public interface IKazoeciaoOutputReader
    {
        SourcesDifference Read(string csv_path);
    }
}