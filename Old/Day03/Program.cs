string[] data = File.ReadAllLines("input.txt");
int total = 0;

string[] currentLine = new string[3];
int currentPos = 0;

foreach(string line in data){
    if(currentPos >= 3){
        currentPos = 0;
        total += Convert(currentLine[0].Intersect(currentLine[1]).Intersect(currentLine[2]).First());
    }

    currentLine[currentPos] = line;
    currentPos++;
}

total += Convert(currentLine[0].Intersect(currentLine[1]).Intersect(currentLine[2]).First());

Console.WriteLine(total);

static int Convert(char c){
    if(char.IsAsciiLetterLower(c)){
        return (int)c - 96;
    }else{
        return (int)c - 38;
    }
}