string[] data = File.ReadAllLines("input.txt");
long max = 0;
long current = 0;

List<long> calories = [];

foreach(string line in data){
    if(line.Length > 0){
        current += long.Parse(line);
    }else{
        calories.Add(current);
        current = 0;
    }
}

calories.Add(current);

max = calories.OrderByDescending(a => a).ToList()[.. 3].Sum();

Console.WriteLine(max);