string[] grid = File.ReadAllLines("input.txt");

(int, int)[] rope = Enumerable.Repeat((0, 0), 10).ToArray();
HashSet<(int, int)> landed = [(0, 0)];

// ???
// Value presented is one too low

foreach(string line in grid){
    int current = int.Parse(line[2..]);
    for(int i = 0; i < current; i++){
        Move(ref rope[0], line[0]);
        for(int b = 0; b < 9; b++){
            Follow(rope[b], ref rope[b + 1], landed, b == 8);
        }
    }
}

Console.WriteLine(landed.Count);

static void Follow((int, int) head, ref (int, int) tail, HashSet<(int, int)> landed, bool add){
    while(Math.Abs(head.Item1 - tail.Item1) > 1 || Math.Abs(head.Item2 - tail.Item2) > 1){
        if(head.Item1 == tail.Item1){
            tail.Item2 = tail.Item2 > head.Item2 ? tail.Item2 - 1 : tail.Item2 + 1;
        }else if(head.Item2 == tail.Item2){
            tail.Item1 = tail.Item1 > head.Item1 ? tail.Item1 - 1 : tail.Item1 + 1;
        }else{
            tail.Item1 = tail.Item1 > head.Item1 ? tail.Item1 - 1 : tail.Item1 + 1;
            tail.Item2 = tail.Item2 > head.Item2 ? tail.Item2 - 1 : tail.Item2 + 1;
        }
        if(add)
            landed.Add(tail);
    }
}

static int Move(ref (int, int) head, char direction) =>
    direction switch{
        'U' => head.Item1 -= 1,
        'L' => head.Item2 -= 1,
        'R' => head.Item2 += 1,
        'D' => head.Item1 += 1,
        _ => throw new Exception()
    };

