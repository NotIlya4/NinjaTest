namespace NinjaTest.Mocking;

class FileReader : IFileReader
{
    public string Read(string path)
    {
        return path;
    }
}