using Hotel.Domain.Extensions;
using Hotel.Sql;
using Hotel.Sql.Tools;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp
{
    class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int AddressId { get; set; }
        public Address Address { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
    }

    class Address
    {
        public int Id { get; set; }
        public string City { get; set; }
    }

    class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
    }

    class Program
    {
        static HotelContext context = new HotelContext();

        static void Main()
        {
            var people = new List<Person>
            {
                new Person { Id = 1, Name = "Marek", AddressId = 11, BookId = 1 },
                new Person { Id = 2, Name = "Kasia", AddressId = 12, BookId = 2 },
                new Person { Id = 3, Name = "Maria", AddressId = 13, BookId = 3 },
            };

            var addresses = new List<Address>
            {
                new Address { Id = 11, City = "Katowice" },
                new Address { Id = 12, City = "Wrocław" },
                new Address { Id = 13, City = "Warszawa" },
            };

            var books = new List<Book>
            {
                new Book { Id = 1, Title = "Book 1" },
                new Book { Id = 2, Title = "Book 2" },
                new Book { Id = 3, Title = "Book 3" },
            };

            var joined = people.Join(addresses, x => x.AddressId, x => x.Id, (person, address) => new Person
            {
                Id = person.Id,
                Name = person.Name,
                BookId = person.BookId,
                Address = address
            }).Join(books, x => x.BookId, x => x.Id, (person, book) => new Person
            {                               
                Id = person.Id,
                Name = person.Name,
                Address = person.Address,
                Book = book
            }).ToList();
        }

        static int Run(Func<int> func)
        {
            try
            {
                return func();
            }
            catch (Exception ex)
            {

            }

            return default;
        }
    }
}
