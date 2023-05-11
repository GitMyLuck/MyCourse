using System;
using System.Collections.Generic;
using MyCourse.Models.ValueObjects;

namespace MYCourse.Models.Entities.Server;

public partial class Course
{
    // costruttore
    public Course(string title, string author)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new ArgumentException("Titolo è un'argomento obbligatorio");
        }
        if (string.IsNullOrWhiteSpace(author))
        {
            throw new ArgumentException("Autore è un'argomento obbligatorio");
        }
        Title = title;
        Author = author;
        Lessons = new HashSet<Lesson>();
    }
	
    public long Id { get; private set; }

    public string Title { get; private set; }

    public string Description { get; private set; }

    public string ImagePath { get; private set; }

    public string Author { get; private set; }

    public string Email { get; private set; }

    public double Rating { get; private set; }

    public Money FullPrice { get; private set; }

    public Money CurrentPrice { get; private set; }

    public void ChangeTitle(string newTitle)
    {
        if ( string.IsNullOrWhiteSpace(newTitle))
        {
                throw new ArgumentException("Impostato titolo non valido");
        }
        Title = newTitle;
    }

    public void ChangePrices(Money newFullPrice, Money newDiscountPrice)
    {
        // controlla se il valore del prezzo intero o di quello scontato non siano
        // nulli
        if (newFullPrice == null || newDiscountPrice == null)
        {
            throw new ArgumentException("Il valore non può essere nullo");
        }
        // controlla che le valute fra prezzo corrente e prezzo intero siano le stesse
        if (newFullPrice.Currency != newDiscountPrice.Currency)
        {
            throw new ArgumentException("Le valute non corrispondono");
        }
        // il prezzo scontato deve essere minore del prezzo intero
        if (newFullPrice.Amount < newDiscountPrice.Amount)
        {
            throw new ArgumentException("Il prezzo scontato deve essere minore del prezzo intero");
        }

        // una volta sanitizzati i valori essi vengono assegnati alle relative proprietà
        FullPrice = newFullPrice;
        CurrentPrice = newDiscountPrice;

    }
    public virtual ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();
}
