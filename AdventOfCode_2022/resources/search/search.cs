namespace resources;

public class Search<T> where T : notnull{
    private readonly List<T> values;
    private readonly Comparator<T> compare;

    public Search(List<T> v){
        values = v;
        compare = new();
    }

    public Search(){
        values = [];
        compare = new();
    }

    public Search(Comparator<T> comparator){
        values = [];
        compare = comparator;
    }

    public Search(List<T> v, Comparator<T> comparator){
        values = v;
        compare = comparator;
    }

    public void AddAndSort(T item){
        values.Add(item);
        Sort();
    }

    public void Add(T item) => values.Add(item);

    public void Sort() => values.Sort(compare.Compare);

    public void RemoveAt(int index) => values.RemoveAt(index);

    public void SetList(List<T> v){
        values.Clear();
        values.AddRange(v);
    }

    public int Binary(T value) => Binary(values, value, compare);

    public static int Binary(List<T> values, T value) => Binary(values, value, new());

    public static int Binary(List<T> values, T value, Comparator<T> comparator){
        if(values == null)
            throw new NullReferenceException();
        int high = values.Count - 1;
        int low = 0;
        int center = ((high - low) / 2) + low;

        int current;
        int compared = comparator[value];

        while(low < high){
            current = comparator[values[center]];
            if(current == compared)
                return center;
            else if(current > compared)
                high = center - 1;
            else
                low = center + 1;

            center = ((high - low) / 2) + low;
        }

        return -1;
    }

    public int Linear(T value) => Linear(values, value);

    public static int Linear(List<T> values, T value){
        for(int i = 0; i < values.Count; i++)
            if(values[i].Equals(value))
                return i;

        return -1;
    }
}