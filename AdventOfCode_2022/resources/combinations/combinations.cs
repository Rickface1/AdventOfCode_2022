namespace resources;

/// <summary>
/// All things combinations
/// </summary>
public static class Combinations{
    public static IEnumerable<T[]> Generate<T>(List<T> values) => Generate(values, 2);
    public static IEnumerable<T[]> Generate<T>(List<T> values, int length){
        T[] current = new T[length];
        int currentLength = 0;
        IEnumerable<T[]> Helper(int size, int start){
            if(size == 0){
                yield return [.. current];
                yield break;
            }

            for(int i = start; i < values.Count; i++){
                current[currentLength] = values[i];
                currentLength++;
                foreach(var result in Helper(size - 1, i + 1))
                    yield return result;
                currentLength--;
            }
        }

        return Helper(length, 0);
    }
    /// <summary>
    /// Generates all combinations of length 2
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="values"></param>
    /// <returns></returns>
    public static T[][] GenerateAll<T>(List<T> values) => GenerateAll(values, 2);
    /// <summary>
    /// Generates all combinations of variable length
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="values"></param>
    /// <param name="length"></param>
    /// <returns></returns>
    public static T[][] GenerateAll<T>(List<T> values, int length){
        T[][] final = new T[Count(values, length)][];
        T[] current = new T[length];
        int currentPos = 0;
        int currentLength = 0;

        void Helper(int size, int start){
            if(size == 0){
                final[currentPos] = [.. current];
                currentPos++;
                return;
            }

            for(int i = start; i < values.Count; i++){
                current[currentLength] = values[i];
                currentLength++;
                Helper(size - 1, i + 1);
                currentLength--;
            }
        }

        Helper(length, 0);
        return final;
    }
    /// <summary>
    /// Finds the number of combinations for size length with 
    /// list values
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="values"></param>
    /// <param name="length"></param>
    /// <returns></returns>
    public static long Count<T>(List<T> values, int length) => values.Count > length ? General.Factorial(values.Count)/(General.Factorial(length) * General.Factorial(values.Count - length)) : 1;
    /// <summary>
    /// Finds number of combinations for size length and 
    /// number of objects objects
    /// </summary>
    /// <param name="objects"></param>
    /// <param name="length"></param>
    /// <returns></returns>
    public static long Count(int objects, int length) => objects > length ? General.Factorial(objects)/(General.Factorial(length) * General.Factorial(objects - length)) : 1;
    /// <summary>
    /// Finds the number of combinations with length 2
    /// within the given list
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="values"></param>
    /// <returns></returns>
    public static long Count<T>(List<T> values) => Count(values, 2);
    /// <summary>
    /// Finds the number of combinations with length 2
    /// and number of objects objects
    /// </summary>
    /// <param name="objects"></param>
    /// <returns></returns>
    public static long Count(int objects) => Count(objects, 2);
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