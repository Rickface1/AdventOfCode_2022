string[] data = File.ReadAllLines("input.txt");
List<string> result = [];
string current = "";
int x = 1;
int cycle = 0;
int total = 0;

foreach(string line in data){
    if(Math.Abs((cycle % 40) - x) <= 1)
        current += "#";
    else
        current += " ";
    if(cycle != 0){
        if((cycle + 1) % 40 == 0){
            result.Add(current);
            current = "";
        }
    }
    cycle++;
    if((cycle - 20) % 40 == 0)
        total += cycle * x;
    if(line != "noop"){
        if(Math.Abs((cycle % 40) - x) <= 1)
            current += "#";
        else
            current += " ";
        if((cycle + 1) % 40 == 0){
            result.Add(current);
            current = "";
        }
        cycle++;
        if((cycle - 20) % 40 == 0)
            total += cycle * x;
        x += int.Parse(line[5..]);
    }
}

result.Add(current);

Console.WriteLine(total);
Console.WriteLine();
result.ForEach(Console.WriteLine);
Console.WriteLine(result[0].Length);

//RFKZCPEF