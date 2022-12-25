using ImageConverterToWebp;
using System.Text.RegularExpressions;

List<string> _folders;
FileSystemWatcher _fileWather;
List<FileSystemWatcher> _foldersWatcher = new();

Console.Write("Введите полный путь к директории (без кавычек): ");
string path = Console.ReadLine();
Console.Write("Введите качество картинки 0 - 100: ");
int imgQualityPercent = Convert.ToInt32(Console.ReadLine());
Console.WriteLine();

try
{
    _folders = Directory.GetDirectories(path).ToList();
    _folders.Add(path);
    foreach (var item in _folders)
    {
        _fileWather = new FileSystemWatcher(item);
        _fileWather.Created += FileSystemWatcher_Created;
        _fileWather.EnableRaisingEvents = true;
        _foldersWatcher.Add(_fileWather);
    }
}
catch (Exception e)
{
    Console.WriteLine("Такого пути не существует: " + path);
    Console.WriteLine(e.StackTrace);
    Console.WriteLine(e.Message);
    Console.WriteLine();
}
Console.ReadLine();

void FileSystemWatcher_Created(object sender, FileSystemEventArgs e)
{
    string fileExtension = new FileInfo(e.FullPath).Extension;

    if (!Regex.IsMatch(fileExtension, @"\.png|\.jpg|\.jpeg", RegexOptions.IgnoreCase))
        return;

    Console.WriteLine($"Новый файл найден: {e.FullPath}");

    WebPConverter.ConvertToWebP(e.FullPath, e.FullPath.Replace(fileExtension, ".webp"), imgQualityPercent);
}