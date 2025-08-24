using System.Collections.Generic;
using System.Linq;

public class Library
{
    public List<Book> Books { get; private set; }
    public List<Borrower> Borrowers { get; private set; }

    public Library()
    {
        Books = new List<Book>();
        Borrowers = new List<Borrower>();
    }

    public void AddBook(Book book)
    {
        Books.Add(book);
    }

    public void RegisterBorrower(Borrower borrower)
    {
        Borrowers.Add(borrower);
    }

    public bool BorrowBook(string isbn, string cardNumber)
    {
        var book = Books.FirstOrDefault(b => b.ISBN == isbn && !b.IsBorrowed);
        var borrower = Borrowers.FirstOrDefault(b => b.LibraryCardNumber == cardNumber);
        if (book != null && borrower != null)
        {
            borrower.BorrowBook(book);
            return true;
        }
        return false;
    }

    public bool ReturnBook(string isbn, string cardNumber)
    {
        var borrower = Borrowers.FirstOrDefault(b => b.LibraryCardNumber == cardNumber);
        var book = borrower?.BorrowedBooks.FirstOrDefault(b => b.ISBN == isbn);
        if (book != null)
        {
            borrower.ReturnBook(book);
            return true;
        }
        return false;
    }

    public List<Book> ViewBooks() => Books;

    public List<Borrower> ViewBorrowers() => Borrowers;
}
