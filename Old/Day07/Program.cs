string[] data = File.ReadAllLines("input.txt");

// 1082741 too low
// 1169616 too low

FileSystem system = new();
foreach(string line in data){
    system.Run(line);
}

Console.WriteLine(system.Delete());

class FileSystem{
    public Directory root;
    public List<Directory> directories;
    public Directory current;
    public FileSystem(){
        root = new("/");
        current = root;
        directories = [root];
    }
    public void Run(string line){
        string[] split = line.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        if(split[0] == "$"){
            if(split[1] == "cd"){
                if(split[2] == ".."){
                    current = current.root ?? current;
                }else if(split[2] == "/"){
                    current = root;
                }else{
                    Directory? directory = current.Find(split[2]);
                    if(directory != null){
                        current = directory;
                    }else{
                        Directory New = new(split[2], current);
                        current.AddDirectory(New);

                        current = New;
                        directories.Add(current);
                    }
                }
            }
        }else if(split[0] == "dir"){
            if(current.Contains(split[1])){
                Directory New = new(split[1], current);
                current.AddDirectory(New);
                
                directories.Add(current);
            }
        }else{
            current.AddFile(split[1], current, int.Parse(split[0]));
        }
    }
    public long FindTotal(){
        List<long> totals = [];

        root.FindTotal(totals);

        return totals.Where(data => data <= 100000).Sum();
    }

    public long Delete(){
        List<long> totals = [];
        long target = 40000000;
        long total = root.FindTotal(totals);

        return totals.Where(data => total - data <= target).Min();
    }
}

abstract class Path{
    public Directory? root;
    public string name;
    public Path(string n, Directory r){
        name = n;
        root = r;
    }
    public Path(string n){
        name = n;
    }
    public override int GetHashCode() => name.GetHashCode();
}
class Directory : Path{
    public HashSet<Path> contents;
    public Directory(string n, Directory r) : base(n, r){
        contents = [];
    }
    public Directory(string n) : base(n){
        contents = [];
    }
    public bool Contains(string n) => contents.Any(data => data.name == n);
    public Directory? Find(string n) => (Directory?)contents.FirstOrDefault(data => data.name == n);
    public void AddFile(string n, Directory r, int s) => contents.Add(new Files(n,r,s));
    public void AddDirectory(Directory d) => contents.Add(d);
    public long FindTotal(List<long> final){
        long total = 0;

        foreach(Path path in contents){
            if(path is Directory directory){
                total += directory.FindTotal(final);
            }else if(path is Files file){
                total += file.size;
            }
        }

        final.Add(total);
        return total;
    }
    public long FindTotal(){
        long total = 0;

        foreach(Path path in contents){
            if(path is Directory directory){
                total += directory.FindTotal();
            }else if(path is Files file){
                total += file.size;
            }
        }

        return total;
    }
    public override string ToString() => $"dir {name} :: {root}";
}

class Files(string n, Directory r, int s) : Path(n, r){
    public int size = s;
    public override string ToString() => $"{size} {name} :: {root}";
}