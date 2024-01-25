namespace resources;

public class Equation{
    public readonly string literal;
    public readonly List<EquationSegment> segments;

    public Equation(string literal){
        this.literal = literal;
        segments = GetEquationSegments(literal);
    }

    public Equation(List<EquationSegment> segments){
        literal = string.Concat(segments.Select(data => data.ToString()));
        this.segments = segments;
    }

    public Equation Derivative() => new(segments.Select(data => data.Derivative()).Where(data => data != null).Select(data => data!).ToList());

    public static List<EquationSegment> GetEquationSegments(string literal){
        List<EquationSegment> segments = [];

        for(int x = 0; x < literal.Length; x++){
            if(literal[x] == '+' || literal[x] == '-' || Char.IsNumber(literal[x])){
                int index = literal[x..].ToList().FindIndex(data => !Char.IsNumber(data));
                index = index == -1 ? literal.Length - 1: index;
                int current = int.Parse(literal[x .. (x + index)]);
                x += index + 1;

                if(Char.IsNumber(literal[x])){
                    index = literal[x..].ToList().FindIndex(data => !Char.IsNumber(data));
                    segments.Add(new(current, int.Parse(literal[x .. (x + index)])));
                    x += index;
                }else if(x < literal.Length){
                    segments.Add(new(current, 1));
                }else{
                    segments.Add(new(current, 0));
                }
            }
        }

        return segments;
    }

    public double Evaluate(double d) => segments.Select(data => data.Value(d)).Sum();

    public Equation Subtract(Equation equation1) => new(segments.Concat(equation1.segments.Select(data => data.Reverse())).ToList());

    public double XIntercept() => -(Evaluate(0)/Derivative().Evaluate(0));

    public bool Parallel(Equation equation){
        return Derivative().Equals(equation.Derivative());
    }

    public override bool Equals(object? obj){
        if(obj is Equation equation){
            return equation.segments.Count == segments.Count && segments.All(equation.segments.Contains);
        }else{
            return false;
        }
    }

    public override int GetHashCode(){
        return segments.Select(data => data.GetHashCode()).Aggregate(0, (acc, val) => unchecked(acc * 31 + val));
    }

    public override string ToString() => string.Join("", segments.Select(data => data.ToString()));
}