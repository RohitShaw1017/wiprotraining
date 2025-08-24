using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class LibraryTests
{
    private Library library;

    [TestInitialize]
    public void Setup()
    {
        library = new Library();
    }

    [TestMethod]
    public void TestAddBook()
    {
        var book = new Book("Book Title", "Author", "123");
        library.AddBook(book);
        Assert.AreEqual(1, library.Books.Count);
    }

    [TestMethod]
    public void TestRegisterBorrower()
    {
        var borrower = new Borrower("John Doe", "LC001");
        library.RegisterBorrower(borrower);
        Assert.AreEqual(1, library.Borrowers.Count);
    }

    [TestMethod]
    public void TestBorrowBook()
    {
        var book = new Book("Book Title", "Author", "123");
        var borrower = new Borrower("John Doe", "LC001");
        library.AddBook(book);
        library.RegisterBorrower(borrower);

        bool result = library.BorrowBook("123", "LC001");

        Assert.IsTrue(result);
        Assert.IsTrue(book.IsBorrowed);
        Assert.AreEqual(1, borrower.BorrowedBooks.Count);
    }

    [TestMethod]
    public void TestReturnBook()
    {
        var book = new Book("Book Title", "Author", "123");
        var borrower = new Borrower("John Doe", "LC001");
        library.AddBook(book);
        library.RegisterBorrower(borrower);
        library.BorrowBook("123", "LC001");

        bool result = library.ReturnBook("123", "LC001");

        Assert.IsTrue(result);
        Assert.IsFalse(book.IsBorrowed);
        Assert.AreEqual(0, borrower.BorrowedBooks.Count);
    }

    [TestMethod]
    public void TestViewBooksAndBorrowers()
    {
        library.AddBook(new Book("Book A", "Author A", "001"));
        library.RegisterBorrower(new Borrower("Alice", "L001"));

        var books = library.ViewBooks();
        var borrowers = library.ViewBorrowers();

        Assert.AreEqual(1, books.Count);
        Assert.AreEqual(1, borrowers.Count);
    }
}
