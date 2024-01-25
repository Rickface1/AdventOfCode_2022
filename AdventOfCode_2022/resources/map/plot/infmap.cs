namespace resources;

public class InfiniteMap{
    public CharMap Map;

    public InfiniteMap(List<List<char>> map){
        Map = new(map);
    }

    public InfiniteMap(string[] map){
        Map = new(map);
    }

    public InfiniteMap(CharMap map){
        Map = map;
    }

    public MapIndex GetIndex(MapIndex index){
        long rowBounds = Map.GetRowBounds();
        long columnBounds = Map.GetColumnBounds();

        long row = (index.line % rowBounds + rowBounds) % rowBounds;
        long column = (index.column % columnBounds + columnBounds) % columnBounds;

        return new MapIndex(row, column);
    }

    public char GetValue(MapIndex index) => Map.GetValue(GetIndex(index));

    public MapIndex IndexOf(char c) => Map.IndexOf(c);

    public bool ValidIndex(MapIndex index) => Map.ValidIndex(index);
}