namespace resources;

public class Comparator<T> where T : notnull{
    private readonly Func<T, int> function;

    public Comparator(){
        if(typeof(T) == typeof(int)){
            function = int(T v) => {
                if(v is int val)
                    return val;
                else
                    throw new Exception("Invalid Key");
            };
        }else if(typeof(T) == typeof(string)){
            function = int(T v) => {
                if(v is string val){
                    int total = 0;
                    val.ToList().ForEach(data => total = total * 256 + data);
                    return total;
                }
                else
                    throw new Exception("Invalid Key");
            };
        }else{
            function = int(T v) => {
                return v.GetHashCode();
            };
        }
    }

    public Comparator(Func<T, int> function) => this.function = function;

    public int Compare(T v1, T v2) => Compare(Evaluate(v1), Evaluate(v2));

    public int Compare(int v1, T v2) => Compare(v1, Evaluate(v2));

    public static int Compare(int v1, int v2) => v1.CompareTo(v2);

    public int Evaluate(T value) => function(value);

    public int Invoke(T val) => Evaluate(val);

    public int this[T v]{
        get{
            return Evaluate(v);
        }
    }
}