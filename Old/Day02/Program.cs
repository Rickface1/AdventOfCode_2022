string[] data = File.ReadAllLines("input.txt");
RockPaperScissors rps = new();
foreach(string line in data){
    rps.Compare(line[0], line[2]);
}

Console.WriteLine(rps.total);

public class RockPaperScissors{
    public long total = 0;
    public enum RPS{
        Rock,
        Paper,
        Scissors
    }
    static Dictionary<char, RPS> Dictionary = new(){
        {'A', RPS.Rock},
        {'B', RPS.Paper},
        {'C', RPS.Scissors},
    };

    public void Compare(char t, char y){
        RPS them = Dictionary[t];
        RPS you = FindValue(them, y);

        if(them == you){
            total += 3;
        }else if((them == RPS.Rock && you == RPS.Paper) || (them == RPS.Paper && you == RPS.Scissors) || (them == RPS.Scissors && you == RPS.Rock)){
            total += 6;
        }

        total += Translate(you);
    }

    RPS FindValue(RPS them, char val){
        if(val == 'X'){
            //lose
            if(them == RPS.Rock){
                return RPS.Scissors;
            }
            if(them == RPS.Paper){
                return RPS.Rock;
            }
            if(them == RPS.Scissors){
                return RPS.Paper;
            }
        }else if(val == 'Y'){
            // tie
            return them;
        }else if(val == 'Z'){
            // win
            if(them == RPS.Rock){
                return RPS.Paper;
            }
            if(them == RPS.Paper){
                return RPS.Scissors;
            }
            if(them == RPS.Scissors){
                return RPS.Rock;
            }
        }

        throw new Exception();
    }

    int Translate(RPS state){
        if(state == RPS.Rock)
            return 1;
        if(state == RPS.Paper)
            return 2;
        if(state == RPS.Scissors)
            return 3;
        throw new Exception();
    }
}