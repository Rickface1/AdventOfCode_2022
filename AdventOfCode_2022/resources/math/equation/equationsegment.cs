namespace resources;

public class EquationSegment{
    public readonly double coefficient;
    public readonly int power;
    public EquationSegment(double coefficient, int power){
        this.coefficient = coefficient;
        this.power = power;
    }

    public double Value(double d) => coefficient * Math.Pow(d, power);

    public static EquationSegment? Simplify(EquationSegment segment1, EquationSegment segment2) => segment1.coefficient == segment2.coefficient ? new(segment1.coefficient + segment2.coefficient, segment1.power) : null;

    public EquationSegment? Derivative() => power >= 1 ? new(coefficient * power, power - 1) : null;

    public EquationSegment? AntiDerivative() => new(coefficient * (1 / power), power + 1);

    public EquationSegment Reverse() => new(coefficient * -1, power);

    public EquationSegment? Derivative(int number){
        EquationSegment? CurrentEquation = this;
        for(int x = 0; x < number; x++){
            CurrentEquation = CurrentEquation?.Derivative();
        }
        return CurrentEquation;
    }

    public override bool Equals(object? obj){
        if(obj is EquationSegment segment){
            return segment.coefficient == coefficient && segment.power == power;
        }

        return false;
    }
    public override int GetHashCode(){
        return HashCode.Combine(coefficient, power);
    }

    public override string ToString() => coefficient != 0 ? ((coefficient > 0 ? "+" : "") + (power > 1 ? $"{coefficient}x^{power}" : (power == 1 ?  $"{coefficient}x" : coefficient.ToString()))) : "";
}