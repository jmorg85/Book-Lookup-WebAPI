using Entity_Framework_Practice;
using Entity_Framework_Practice.Controllers;
using Entity_Framework_Practice.Interfaces;
using Entity_Framework_Practice.Models;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace BookRepositoryTests
{
    public class BookRepositoryTests
    {
        private BookRepository _bookRepository;
        ApiContext _context;

        [SetUp]
        public void Setup()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApiContext>()
                .UseInMemoryDatabase("TestDB");

            _context = new ApiContext(optionsBuilder.Options);
            _context.Database.EnsureDeleted();

            _bookRepository = new BookRepository(_context);

            var testData = GetTestData();

            _context.Books.AddRange(testData);
            _context.SaveChanges();
        }

        [TearDown]
        public void Dispose()
        {
            _context.SaveChanges();
            _context.Dispose();
        }

        [Test]
        public async Task GetAllBoooks_ShouldReturnAllBooks()
        {
            //Arrange

            //Act
            var result = await _bookRepository.GetAllBooks();
            //Assert
            Assert.That(result.Count() == 5);
        }

        [Test]
        public async Task AddBook_GivenANewBook_ShouldAddThatBookToTheDB()
        {
            //Arrange
            Book testBook = new Book()
            {
                Title = "Test",
                Author = "Me",
                Description = "This is a test book used for testing purposes."
            };

            //Act
            await _bookRepository.AddBook(testBook);
            var result = await _bookRepository.GetAllBooks();

            //Assert
            Assert.That(result.Count == 6);
        }

        [Test]
        public async Task CheckIfBookExists_GivenARealId_ShouldReturnTrue()
        {
            var result = await _bookRepository.CheckIfBookExists(1);

            Assert.That(result == true);
        }

        [Test]
        public async Task CheckIfBookExists_GivenAFakelId_ShouldReturnFalse()
        {
            var result = await _bookRepository.CheckIfBookExists(11);

            Assert.That(result == false);
        }

        [Test]
        public async Task DeleteBook_GivenARealId_ShouldDeleteTheBookFromTheDB()
        {
            await _bookRepository.DeleteBook(2);
            var result = await _bookRepository.GetAllBooks();

            Assert.That(result.Count == 4);
        }

        private List<Book> GetTestData()
        {
            List<Book> testData = new List<Book>
            {
                new Book
                {
                    Title = "Best. Movie. Year. Ever. How 1999 blew up the big screen",
                    Author = "Brian Raftery",
                    Description = "From a veteran culture writer and modern movie expert, a celebration and analysis of the movies of 1999—arguably the most groundbreaking year in American cinematic history. In 1999, Hollywood as we know it exploded: Fight Club. The Matrix. Office Space.",
                },
                new Book
                {
                    Title = "The Power of Habit: Why We Do What We Do in Life and Business",
                    Author = "Charles Duhigg",
                    Description = "The Power of Habit: Why We Do What We Do in Life and Business is a book by Charles Duhigg, a New York Times reporter, published in February 2012 by Random House. It explores the science behind habit creation and reformation. The book reached the best seller list for The New York Times, Amazon.com, and USA Today."
                },
                new Book
                {
                    Title = "The Disaster Artist: My Life Inside The Room, The Greatest Bad Movie Ever Made",
                    Author = "Greg Sestero & Tom Bissell",
                    Description = "The Disaster Artist: My Life Inside The Room, the Greatest Bad Movie Ever Made is a 2013 non-fiction book written by Greg Sestero and Tom Bissell."
                },
                new Book
                {
                    Title = "The Divine Comedy",
                    Author = "Dante Alighieri",
                    Description = "The Divine Comedy is an Italian narrative poem by Dante Alighieri, begun c. 1308 and completed around 1321, shortly before the author's death. It is widely considered the pre-eminent work in Italian literature and one of the greatest works of Western literature."
                },
                new Book
                {
                    Title = "From Crook To Cook: Platinum Recipes from Tha Boss Dogg's Kitchen",
                    Author = "Snoop Dogg",
                    Description = "Welcome to tha Boss Dogg's Kitchen\r\nThe first cookbook and recipe book from Tha Dogg: You've seen Snoop work his culinary magic on VH1's Emmy-nominated Martha and Snoop's Potluck Dinner Party, and now, Tha Dogg's up in your kitchen...with his first cookbook.\r\n\r\nRecipe book that delivers 50 recipes straight from Snoop's own collection: Snoop's cookbook features OG staples like Baked Mac & Cheese and Fried Bologna Sandwiches with Chips, and new takes on classic weeknight faves like Soft Flour Tacos and Easy Orange Chicken. And it don't stop...Snoop's giving a taste of the high life with remixes on upper echelon fare such as Lobster Thermidor and Filet Mignon. But we gotta keep it G with those favorite munchies too, ya know? From chewy Starbursts to those glorious Frito BBQ Twists, you should have an arsenal of snacks that'll satisfy. And of course, no party is complete without that Gin and Juice and other platinum ways to entertain."
                }
            };

            return testData;
        }
    }
}