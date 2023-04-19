

class Apple     {
    public string Color { get; set; }
    public int Weight { get; set; }
}

List<Apple> apples = new List<Apple>        {
    new Apple { Color = "Red", Weight = 180},
    new Apple { Color = "Green", Weight = 195},
    new Apple { Color = "Red", Weight = 190},
    new Apple { Color = "Green", Weight = 190},
};

//                                     
IEnumerable<int> weightsOfRedApples = apples
      /*si filtrano risultati con where*/   .Where(apple => apple.Color == "Red")
      /*si proiettano risultati con select*/.Select(apple => apple.Weight);
      /*il tipo di risultato della select e' 
      quello della variabile a cui viene assegnato
      in questo caso int, perchè la proprietà Weight 
      è un intero*/

double average = apples
      /*si filtrano risultati con where*/   .Where(apple => apple.Color == "Red")
      /*si proiettano risultati con select*/.Select(apple => apple.Weight)
                                            .Average();

Console.WriteLine($"result of linq = {average}\n");

// Esercizio 1
int minimumWeight = apples
            .Select(apple => apple.Weight)
            .Min();

Console.WriteLine($"result minimum weight = {minimumWeight}\n");

// Esercizio 2
IEnumerable <string> color = apples
        .Where(apple => apple.Weight == 190)
        .Select(apple => apple.Color);

int contatore = 0;
foreach (var a in color)        {
Console.WriteLine($"result color no {contatore} = {a}\n");
contatore++;
};

// Esercizio 3

int redAppleCount = apples
            .Where(apple => apple.Color == "Red")
            .Count();
Console.WriteLine($"result of : how many red apples are in?\n {redAppleCount} \n");

// Esercizio 4
int totalWeight = apples
            .Where(apple => apple.Color == "Green")
            .Select(apple => apple.Weight)
            .Sum();

Console.WriteLine($"total weight of green apples = {totalWeight}.")