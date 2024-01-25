namespace resources;

/// <summary>
/// All things permutations
/// </summary>
public static class Permutations{
    /// <summary>
    /// Generates them one at a time, using the IEnumerator and 
    /// IEnumerable interfaces. Genreates them with length 2
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="values"></param>
    /// <returns></returns>
    public static IEnumerable<T[]> Generate<T>(List<T> values) => Generate(values, 2);
    /// <summary>
    /// Generates them one at a time, using the IEnumerator and 
    /// IEnumerable interfaces.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="values"></param>
    /// <param name="length"></param>
    /// <returns></returns>
    public static IEnumerable<T[]> Generate<T>(List<T> values, int length){
        static IEnumerable<T[]> Helper(T[] current, int size){
            if(size == 1){
                yield return [..current];
                yield break;
            }

            foreach (var result in Helper(current, size - 1)){
                yield return result;
            }

            for(int i = 0; i < size - 1; i++){
                if(size % 2 == 1)
                    (current[size - 1], current[0]) = (current[0], current[size - 1]);
                else
                    (current[size - 1], current[i]) = (current[i], current[size - 1]);
                foreach (var result in Helper(current, size - 1)){
                    yield return result;
                }
            }
        }
        return Helper([.. values], length);
    }

    /// <summary>
    /// Generates all permutations with size 2
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="values"></param>
    /// <returns></returns>
    public static T[][] GenerateAll<T>(List<T> values) => GenerateAll(values, 2);
    /// <summary>
    /// Generates all permutations with size length
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="values"></param>
    /// <param name="length"></param>
    /// <returns></returns>
    public static T[][] GenerateAll<T>(List<T> values, int length){
        T[][] returns = new T[Count(values, length)][];
        int currentPos = 0;
        void Helper(T[] current, int size){
            if(size == 1){
                returns[currentPos] = [.. current];
                currentPos++;
                return;
            }

            Helper(current, size - 1);

            for(int i = 0; i < size - 1; i++){
                if(size % 2 == 1)
                    (current[size - 1], current[0]) = (current[0], current[size - 1]);
                else
                    (current[size - 1], current[i]) = (current[i], current[size - 1]);
                Helper(current, size - 1);
            }
        }
        Helper([.. values], length);

        return returns;
    }
    /// <summary>
    /// Finds the number of permutations
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="values"></param>
    /// <param name="length"></param>
    /// <returns></returns>
    public static long Count<T>(List<T> values, int length) => values.Count != length ? General.Factorial(values.Count)/General.Factorial(values.Count - length) : General.Factorial(values.Count);
    /// <summary>
    /// Finds the number of permutations
    /// </summary>
    /// <param name="objects"></param>
    /// <param name="length"></param>
    /// <returns></returns>
    public static long Count(int objects, int length) => objects != length ? General.Factorial(objects)/General.Factorial(objects - length) : General.Factorial(objects);
    /// <summary>
    /// Finds the number of permutations
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="values"></param>
    /// <returns></returns>
    public static long Count<T>(List<T> values) => Count(values, 2);
    /// <summary>
    /// Finds the number of permutations
    /// </summary>
    /// <param name="objects"></param>
    /// <returns></returns>
    public static long Count(int objects) => Count(objects, 2);
    /// <summary>
    /// Counts all permutations for length 1 to array length
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="values"></param>
    /// <returns></returns>
    public static long CountAll<T>(List<T> values){
        long total = 0;
        for(int i = 1; i <= values.Count; i++){
            total += Count(values, i);
        }
        return total;
    }
    /// <summary>
    /// Counts all permutations for length 1 to array length
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="objects"></param>
    /// <returns></returns>
    public static long CountAll(int objects){
        long total = 0;
        for(int i = 1; i <= objects; i++){
            total += Count(objects, i);
        }
        return total;
    }
    /// <summary>
    /// Prints all combinations one at a time for debugging purposes
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="values"></param>
    public static void PrintValues<T>(List<List<T>> values) => values.ForEach(a => {
            a.ForEach(b => Console.Write(b));
            Console.WriteLine();
        });
}