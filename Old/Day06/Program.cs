string data = File.ReadAllLines("input.txt")[0];

char[] current = [.. data[.. 14]];

for (int i = 4; i < data.Length; i++){
    if(current.Distinct().Count() == 14){
        Console.WriteLine(i);
        break;
    }else{
        LeftShiftAndAdd(current, data[i]);
    }
}

static void LeftShiftAndAdd(char[] chars, char c){
    for(int i = 0; i < chars.Length - 1; i++){
        chars[i] = chars[i + 1];
    }
    chars[^1] = c;
}