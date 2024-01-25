namespace resources;

public static class General{
    public static string PrintTimes(int times, char c) => new(Enumerable.Repeat(c, times).ToArray());
    public static string PrintTimes(int times, string s) => new(Enumerable.Repeat(s, times).SelectMany(a => a).ToArray());
    public static (long max, long min) Order(long item1, long item2) => item1 > item2 ? (item1, item2) : (item2, item1);
    public static long Factorial(int val){
        int total = val;
        for(int i = val - 1; i > 0; i--){
            total *= i;
        }

        return total;
    }
}