using System.Reflection;

namespace resources;

public class CharMap{
    public List<List<char>> map;

    public CharMap(List<List<char>> map) => this.map = map;

    public CharMap(string[] map) => this.map = [.. map.Select(data => data.ToCharArray().ToList())];

    public CharMap() => map = [[]];

    public MapIndex IndexOf(char c){
        int row = map.FindIndex(data => data.Contains(c));
        return new(row, map[row].IndexOf(c));
    }

    public int GetRowBounds() => map.Count;

    public int GetColumnBounds() => map[0].Count;

    public char GetValue(MapIndex index) => map[(int)index.line][(int)index.column];

    public char? ProtectedNextValue(MapIndex index, Direction direction){
        int line = (int)index.line;
        int column = (int)index.column;


        try{
            if(direction == Direction.North){
                return map[line - 1][column];
            }else if(direction == Direction.South){
                return map[line + 1][column];
            }else if(direction == Direction.East){
                return map[line][column + 1];
            }else if(direction == Direction.West){
                return map[line][column - 1];
            }
        }catch(ArgumentOutOfRangeException){
            return null;
        }

        throw new Exception($"DIRECTION OUT OF BOUNDS: {direction}");
    }

    public char GetNextValue(MapIndex index, Direction direction){
        int line = (int)index.line;
        int column = (int)index.column;


        if(direction == Direction.North){
            return map[line - 1][column];
        }else if(direction == Direction.South){
            return map[line + 1][column];
        }else if(direction == Direction.East){
            return map[line][column + 1];
        }else if(direction == Direction.West){
            return map[line][column - 1];
        }

        throw new Exception($"DIRECTION OUT OF BOUNDS: {direction}");
    }

    public bool ValidIndex(MapIndex index) => index.ValidIndex(GetRowBounds(), GetColumnBounds());

    public List<char> GetRow(int index) => map[index];

    public List<char> GetColumn(int index) => map.Select(data => data[index]).ToList();

    public CharMap Clone() => new(map.Select(data => new string(data.ToArray())).ToArray());

    public void Print() => map.ForEach(data => Console.WriteLine(data.ToArray()));

    public void SetValue(MapIndex index, char c) => map[(int)index.line][(int)index.column] = c;

    public void Replace(CharMap map) => this.map = map.map;
}