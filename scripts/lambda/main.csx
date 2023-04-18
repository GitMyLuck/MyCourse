Func<DateTime, bool> canDrive = dob => {
    return dob.AddYears(18) <= DateTime.Today;
};

DateTime dob = new DateTime(2005, 12, 25);

bool result = canDrive(dob);
Console.WriteLine($"risultato è  {result}");

Action<DateTime> printDate = date => Console.WriteLine(date);

DateTime date = DateTime.Today;
printDate(date);

// Primo esercizio
Func<string, string, string> concats = (nome, cognome) =>
  {
        return string.Concat(nome, " ", cognome);
};

string nome = "Giovanni";
string cognome = "Avallone";
string res = concats(nome, cognome);
Console.WriteLine("\nEsercizio 1 'NOME E COGNOME' =>");
Console.WriteLine($"risultato 'concat' = \"{res}\"");


// Secondo Esercizio

// primo metodo ...
Func<int, int, int, int> getMaximum = (no1, no2, no3) =>  {
  
    int maxNo1 = Math.Max(no1, no2);
    int maxNo2 = Math.Max(maxNo1, no3);
    return maxNo2;
};

// secondo metodo ...
Func<int, int, int, int> getMaximum2 = (no1, no2, no3) =>  {
  
    int maxNumber= 0;
    int[] numbers = { no1, no2, no3 };
    
    Array.Sort(numbers);

    maxNumber = numbers.Last();

    return maxNumber;
};

int no1 = 16, no2 = 61, no3 = 34;

int maxResult = getMaximum2(no1, no2, no3);
Console.WriteLine($"\nEsercizio 2 'MAX NUMBER' =>");
Console.WriteLine($"Il numero più alto è : {maxResult}");


//  terzo esercizio

Action<DateTime, DateTime> printLowerDate = (data1, data2) => {

    int res = DateTime.Compare(data1, data2);

};

DateTime d = new DateTime(2023, 04, 16);
DateTime e = DateTime.Today;

printLowerDate(d,e);