string[] data = File.ReadAllLines("input.txt");
int total = 0;
char[] split = ['-', ','];

foreach(string line in data){
    int[] ranges = line.Split(split, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Select(int.Parse).ToArray();
    //int range1comp = ranges[0].CompareTo(ranges[2]);
    //int range2comp = ranges[1].CompareTo(ranges[3]);
    if(ranges[0] <= ranges[3] && ranges[1] >= ranges[2]){
        total++;
    }
}

Console.WriteLine(total);