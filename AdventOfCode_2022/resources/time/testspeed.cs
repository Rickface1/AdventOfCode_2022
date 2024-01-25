using System.Diagnostics;

namespace resources;

public class TestSpeed<T> where T : Delegate{
    private T func;
    private int warmups;
    private int runs;
    public TestSpeed(T f){
        func = f;
        warmups = 10;
        runs = 500;
    }
    public TestSpeed(T f, int r, int w){
        func = f;
        runs = r;
        warmups = w;
    }
    public TestSpeed(T f, int r){
        func = f;
        runs = r;
        warmups = 10;
    }
    public int GetWarmups() => warmups;
    public void SetWarmups(int w) => warmups = w;
    public int GetRuns() => runs;
    public void SetRuns(int r) => runs = r;
    public T GetFunc() => func;
    public void SetFunc(T f) => func = f;
    public double Run(){
        for(int i = 0; i < warmups; i++)
            func.DynamicInvoke();

        Stopwatch sw = new();
        sw.Start();

        for(int i = 0; i < runs; i++)
            func.DynamicInvoke();
        
        sw.Stop();

        return sw.Elapsed.TotalMilliseconds / runs;
    }
    public double Run(int runs, int warmups){
        for(int i = 0; i < warmups; i++)
            func.DynamicInvoke();

        Stopwatch sw = new Stopwatch();
        sw.Start();

        for(int i = 0; i < runs; i++)
            func.DynamicInvoke();
        
        sw.Stop();

        return sw.Elapsed.TotalMilliseconds / runs;
    }
    public void Override(T f, int r, int w){
        func = f;
        runs = r;
        warmups = w;
    }
    public void Override(T f, int r){
        func = f;
        runs = r;
    }
    public void Override(T f) => func = f;
    public TestSpeed<T> Clone() => new(func, runs, warmups);
    public override string ToString() => $"Runs: {runs}\nWarmups: {warmups}\nTime: {Run()}";
}