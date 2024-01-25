string[] data = File.ReadAllLines("input.txt");
MonkeyManager manager = new();
ParseInput(data, manager);
manager.Rounds(20);
Console.WriteLine(manager.P1());
//55926
//56168


/// Works correctly for sure
/// Adds all of the monkeys to the manager
static void ParseInput(string[] data, MonkeyManager manager){
    int monkeyVal = 0;
    List<int> startingValues = [];
    Operation operation = Operation.Multiply;
    int degree = 0;
    int test = 0;
    int trueMonkey = 0;
    int falseMonkey = 0;
    int current = 0;

    foreach(string line in data){
        if(current == 0){
            monkeyVal = line[7] - 48;
        }else if(current == 1){
            startingValues = line[18..].Split(',').Select(int.Parse).ToList();
        }else if(current == 2){
            if(line[25..] == "old"){
                degree = -1;
                operation = Operation.Square;
            }else{
                operation = line[23] == '*' ? Operation.Multiply : Operation.Add;
                degree = int.Parse(line[25..]);
            }
        }else if(current == 3){
            test = int.Parse(line[21..]);
        }else if(current == 4){
            trueMonkey = int.Parse(line[29..]);
        }else if(current == 5){
            falseMonkey = int.Parse(line[30..]);
        }else{
            manager.AddMonkey(monkeyVal, startingValues, operation, degree, test, trueMonkey, falseMonkey);
            current = -1;
        }

        current++;
    }

    manager.AddMonkey(monkeyVal, startingValues, operation, degree, test, trueMonkey, falseMonkey);
}
public class MonkeyManager(){
    public Dictionary<int, Monkey> monkeys = [];
    public void AddMonkey(int monkeyVal, List<int> startingValues, Operation starting, int degree, int test, int trueMonkey, int falseMonkey) =>
        monkeys.Add(monkeyVal, new(startingValues, starting, degree, test, trueMonkey, falseMonkey));
    public void Round(){
        for(int i = 0; i < monkeys.Count; i++){
            foreach(Item item in monkeys[i].items){
                monkeys[monkeys[i].Inspect(item)].AddItem(item);
            }
            monkeys[i].items.Clear();
        }
    }
    public void Rounds(int rounds){
        for(int i = 0; i < rounds; i++){
            Round();
        }
    }
    public long P1() =>
        monkeys.Select(data => data.Value.inspections).OrderByDescending(data => data).ToList()[0..2].Aggregate((a, b) => a * b);
}
public enum Operation{
    Multiply,
    Add,
    Square
}
public class Monkey{
    public Operation operation;
    public int degree;
    public int inspections;
    public List<Item> items;
    public int test;
    public int trueMonkey;
    public int falseMonkey;
    public int Inspect(Item item){
        inspections++;
        if(operation == Operation.Multiply){
            item.worry = (int)Math.Floor((item.worry * degree) / 3m);
        }else if(operation == Operation.Add){
            item.worry = (int)Math.Floor((item.worry + degree) / 3m);
        }else if(operation == Operation.Square){
            item.worry = (int)Math.Floor((item.worry * item.worry) / 3m);
        }else{
            throw new Exception();
        }
        return item.worry % test == 0 ? trueMonkey : falseMonkey;
    }
    public void AddItem(Item item) =>
        items.Add(item);
    public Monkey(List<int> startingValues, Operation starting, int degree, int test, int trueMonkey, int falseMonkey){
        items = startingValues.Select(data => new Item(data)).ToList();
        operation = starting;
        this.degree = degree;
        this.test = test;
        this.trueMonkey = trueMonkey;
        this.falseMonkey = falseMonkey;
    }
}
public record ItemRecord(int worry);
public class Item(int w){
    public int worry = w;
}