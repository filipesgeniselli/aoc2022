namespace AdventOfCode2022.Days;

public class Day07 : IDay
{
    public string Part1(List<string> inputs)
    {
        FileSystem fs = GenerateFileSystem(inputs);

        var result = FindTargetToDeletion(fs);
        return result.Select(x => x.TotalDirSize).Sum().ToString();
    }

    public string Part2(List<string> inputs)
    {
        FileSystem fs = GenerateFileSystem(inputs);
        var targetFreeSpace = 30000000 - (70000000 - fs.TotalDirSize);

        var result = FindTargetToDeletion(fs, targetFreeSpace);
        return result.Select(x => x.TotalDirSize).Min().ToString();
    }

    private List<FileSystem> FindTargetToDeletion(FileSystem fs)
    {
        if (fs.Children == null)
            return new();

        var result = fs.Children.Where(x => x.IsDirectory && x.TotalDirSize <= 100000).ToList();

        foreach (var dir in fs.Children.Where(x => x.IsDirectory))
        {
            result.AddRange(FindTargetToDeletion(dir));
        }

        return result;
    }

    private List<FileSystem> FindTargetToDeletion(FileSystem fs, int targetSize)
    {
        if(fs.Children == null) 
            return new();

        var result = fs.Children.Where(x => x.IsDirectory && x.TotalDirSize >= targetSize).ToList();

        foreach (var dir in fs.Children.Where(x => x.IsDirectory))
        {
            result.AddRange(FindTargetToDeletion(dir, targetSize));
        }

        return result;
    }

    private static FileSystem GenerateFileSystem(List<string> inputs)
    {
        FileSystem fs = new FileSystem("/", true, null, null, new());
        foreach (var input in inputs.Skip(1))
        {
            if (input.StartsWith("$"))
            {
                //commands (ls command is ignored)
                var command = input.Split(' ');
                if (command[1] == "cd")
                {
                    if (command[2] == "..")
                    {
                        fs = fs.Parent;
                    }
                    else
                    {
                        fs = fs.Children.Where(x => x.Name == command[2]).First();
                    }
                }
            }
            else
            {
                var commandResult = input.Split(' ');
                if (commandResult[0] == "dir")
                {
                    fs.Children.Add(new FileSystem(commandResult[1], true, null, fs, new()));
                }
                else
                {
                    fs.Children.Add(new FileSystem(commandResult[1], false, int.Parse(commandResult[0]), fs, null));
                }
            }
        }

        while (fs.Parent != null)
        {
            fs = fs.Parent;
        }

        return fs;
    }

}

internal class FileSystem 
{
    public string Name { get; set; }
    public bool IsDirectory { get; set; }
    public int? Size { get; set; }
    public FileSystem? Parent { get; set; }
    public List<FileSystem>? Children { get; set; }

    public FileSystem(string name, bool isDirectory, int? size, FileSystem? parent, List<FileSystem>? children)
    {
        Name = name;
        IsDirectory = isDirectory;
        Size = size;
        Parent = parent;
        Children = children;
    }

    public int TotalDirSize =>
        IsDirectory ? Children.Select(x => x.TotalDirSize).Sum() : Size ?? 0;
};