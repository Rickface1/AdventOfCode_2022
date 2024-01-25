string[] data = File.ReadAllLines("input.txt");
int index = FindIndex(data);
List<int> indexes = Columns(data, index);

SupplyStacks stacks = new(indexes.Count);
for(int x = 0; x < index; x++){
    for(int y = 0; y < data[x].Length; y++){
        if(char.IsLetter(data[x][y])){
            stacks.AddSupply(data[x][y], indexes.IndexOf(y) + 1);
        }
    }
}

stacks.Finish();

string[] split = ["move ", " from ", " to "];

for(int i = index + 2; i < data.Length; i++){
    //data[i].Split(split, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Select(int.Parse).ToList().ForEach(data => Console.Write($"{data} "));
    //Console.WriteLine();
    stacks.Move(data[i].Split(split, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Select(int.Parse).ToArray());
}


stacks.GetLastBoxes().ToList().ForEach(Console.Write);


static List<int> Columns(string[] data, int index){
    string line = data[index];
    List<int> indexes = [];

    for(int i = 0; i < line.Length; i++){
        if(char.IsDigit(line[i])){
            indexes.Add(i);
        }
    }

    return indexes;
}
static int FindIndex(string[] data){
    char[] chars = [];
    for(int i = 0; i < data.Length; i++){
        if(!data[i].Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).SelectMany(data => data).Any(data => !char.IsDigit(data))){
            return i;
        }
    }
    return -1;
}
public class SupplyStacks{
    public Dictionary<int, List<char>> stacks;
    public SupplyStacks(int length){
        stacks = [];
        for(int i = 0; i < length; i++){
            stacks.Add(i + 1, []);
        }
    }
    public char[] GetLastBoxes() => stacks.Select(data => data).OrderBy(data => data.Key).Select(data => data.Value.Last()).ToArray();
    public void Move(int[] val) => Move(val[0], val[1], val[2]);
    public void Move(int quantity, int from, int to){
        List<char> removed = stacks[from][(stacks[from].Count - quantity) ..];
        //removed.Reverse();
        stacks[from].RemoveRange(stacks[from].Count - quantity, quantity);
        stacks[to].AddRange(removed);
    }
    public void AddSupply(char c, int stack) => stacks[stack].Add(c);
    public void Finish(){
        foreach(List<char> stack in stacks.Values){
            stack.Reverse();
        }
    }
    public override string ToString(){
        string r = "";
        stacks.Select(data => data).OrderBy(data => data.Key).ToList().ForEach(data => {
            r += $"{data.Key}: " + string.Join(" ", data.Value) + "\n";
        });
        return r;
    }
}