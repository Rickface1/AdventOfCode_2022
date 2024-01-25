using resources;

int[][] grid = File.ReadAllLines("input.txt").Select(a => a.Select(b => b - 48).ToArray()).ToArray();
TestSpeed p1 = new(() => P1(grid), 1000, 100);
TestSpeed p2 = new(() => P2(grid), 1000, 100);

Console.WriteLine(p1);
Console.WriteLine();
Console.WriteLine(p2);

static int P2(int[][] grid){
    int max = 0;

    for(int x = 1; x < grid.Length - 1; x++){
        for(int y = 1; y < grid[x].Length - 1; y++){
            int current = GetVal(x, y, grid);
                max = Math.Max(current, max);
        }
    }

    return max;

    static int GetVal(int x, int y, int[][] grid){
        int up = PathFind(x, y, true, true, grid);
        int down = PathFind(x, y, true, false, grid);
        int left = PathFind(y, x, false, true, grid);
        int right = PathFind(y, x, false, false, grid);

        return up * down * left * right;
    }

    static int PathFind(int val, int other, bool x, bool decreasing, int[][] grid){
        int trees = 1;
        int current;
        if(x){
            if(decreasing){
                current = grid[val][other];
                if(val <= 0)
                    return 0;
                while(val > 1 && grid[val - 1][other] < current){
                    trees++;
                    val--;
                }
            }else{
                current = grid[val][other];
                if(val >= grid.Length - 1)
                    return 0;
                while(val < grid.Length - 2 && grid[val + 1][other] < current){
                    trees++;
                    val++;
                }
            }
        }else{
            if(decreasing){
                current = grid[other][val];
                if(val <= 0)
                    return 0;
                while(val > 1 && grid[other][val - 1] < current){
                    trees++;
                    val--;
                }
            }else{
                current = grid[other][val];
                if(val >= grid[other].Length - 1)
                    return 0;
                while(val < grid[other].Length - 2 && grid[other][val + 1] < current){
                    trees++;
                    val++;
                }
            }
        }

        return trees;
    }
}
static int P1(int[][] grid){    
    HashSet<(int, int)> result = [];
    int previous;


    for(int x = 0; x < grid.Length; x++){
        previous = int.MinValue;
        for(int y = 0; y < grid[x].Length; y++){
            if(grid[x][y] > previous){
                previous = grid[x][y];
                result.Add((x, y));
            }
        }
    }

    for(int x = 0; x < grid.Length; x++){
        previous = int.MinValue;
        for(int y = grid[x].Length - 1; y >= 0; y--){
            if(grid[x][y] > previous){
                previous = grid[x][y];
                result.Add((x, y));
            }
        }
    }

    for(int y = 0; y < grid[0].Length; y++){
        previous = int.MinValue;
        for(int x = 0; x < grid.Length; x++){
            if(grid[x][y] > previous){
                previous = grid[x][y];
                result.Add((x, y));
            }
        }
    }

    for(int y = 0; y < grid[0].Length; y++){
        previous = int.MinValue;
        for(int x = grid.Length - 1; x >= 0; x--){
            if(grid[x][y] > previous){
                previous = grid[x][y];
                result.Add((x, y));
            }
        }
    }

    return result.Count;
}