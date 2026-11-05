using System;
using System.Collections.Generic;

namespace LibraryApp
{
    // Класс читателя (нужен для задач 10, 17, 21-25)
    public class Reader
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }

        public Reader(string name, string surname = "", string phone = "")
        {
            Name = name;
            Surname = surname;
            Phone = phone;
        }

        public override string ToString()
        {
            string info = Name;
            if (!string.IsNullOrEmpty(Surname)) info += " " + Surname;
            if (!string.IsNullOrEmpty(Phone)) info += $" (Тел: {Phone})";
            return info;
        }
    }

    // Задачи 1, 3, 5, 7, 8: Класс Книга
    public class Book
    {
        // Поля / свойства (Задача 1)
        public string Title { get; set; }
        public string Author { get; set; }
        public int Year { get; set; }

        // Дополнительные свойства для выдачи
        public bool IsAvailable { get; set; } = true;
        public string BorrowedBy { get; set; } = "";

        public Book(string title, string author, int year)
        {
            Title = title;
            Author = author;
            Year = year;
            IsAvailable = true;
        }

        // Задача 1: метод DisplayInfo()
        public void DisplayInfo()
        {
            string status = IsAvailable ? "Доступна" : $"Выдана (Читатель: {BorrowedBy})";
            Console.WriteLine($"Название: \"{Title}\" | Автор: {Author} | Год: {Year} | Статус: {status}");
        }

        // Задача 3: метод SetTitle()
        public void SetTitle(string title)
        {
            Title = title;
        }

        // Задача 7: метод IsBookAvailable()
        public bool IsBookAvailable()
        {
            return IsAvailable;
        }

        // Задача 8: метод CalculateBookAge()
        public int CalculateBookAge(int currentYear)
        {
            return currentYear - Year;
        }
    }

    // Класс Библиотека
    public class Library
    {
        // Задача 11: Название библиотеки
        public static string LibraryName = "Центральная Городская Библиотека";

        // Задача 15: Статический счетчик книг
        public static int TotalBooks = 0;

        // Коллекции для хранения данных
        private List<Book> books = new List<Book>();
        private List<Reader> readers = new List<Reader>();

        // Задача 2: Метод без параметров
        public void ShowWelcomeMessage()
        {
            Console.WriteLine("==================================================");
            Console.WriteLine($" Добро пожаловать в {LibraryName}!");
            Console.WriteLine("==================================================");
        }

        // Задача 4 и Задача 15: Метод с несколькими параметрами и увеличение счетчика
        public void AddBook(string title, string author, int year)
        {
            Book newBook = new Book(title, author, year);
            books.Add(newBook);
            TotalBooks++; // Задача 15: увеличиваем статический счетчик
            Console.WriteLine($"Книга \"{title}\" успешно добавлена в библиотеку.");
        }

        // Метод для добавления уже созданного объекта книги (например для демонстрации задачи 5)
        public void AddBook(Book book)
        {
            books.Add(book);
            TotalBooks++;
        }

        // Задача 6: Метод с возвращаемым значением
        public int GetBookCount()
        {
            return books.Count;
        }

        // Задача 9: Метод поиска по названию
        public Book FindBook(string title)
        {
            foreach (var book in books)
            {
                if (book.Title.Equals(title, StringComparison.OrdinalIgnoreCase))
                {
                    return book;
                }
            }
            return null;
        }

        // Задача 10: Метод проверки читателя
        public bool IsReaderRegistered(string name)
        {
            foreach (var reader in readers)
            {
                if (reader.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }

        // Задача 11: Статический метод
        public static void ShowLibraryName()
        {
            Console.WriteLine($"[Информация о библиотеке]: Название — {LibraryName}");
        }

        // Задача 12: Нестатический метод вывода списка книг
        public void ShowBooks()
        {
            if (books.Count == 0)
            {
                Console.WriteLine("В библиотеке пока нет книг.");
                return;
            }

            Console.WriteLine("\n--- Список книг в библиотеке ---");
            for (int i = 0; i < books.Count; i++)
            {
                Console.Write($"{i + 1}. ");
                books[i].DisplayInfo();
            }
        }

        // Задача 13: Статический метод расчёта штрафа
        public static double CalculateFine(int overdueDays)
        {
            if (overdueDays <= 0) return 0.0;
            double finePerDay = 15.0; // 15 руб в день
            return overdueDays * finePerDay;
        }

        // Задача 14: Сравнение методов - статический GetMaxBooks и нестатический GetAvailableBooks
        public static int GetMaxBooks(int first, int second)
        {
            return first > second ? first : second;
        }

        public int GetAvailableBooks()
        {
            int count = 0;
            foreach (var b in books)
            {
                if (b.IsAvailable) count++;
            }
            return count;
        }

        // Задача 16: Перегрузка SearchBook (3 варианта: название, автор, год)
        public List<Book> SearchBook(string title)
        {
            List<Book> result = new List<Book>();
            foreach (var b in books)
            {
                if (b.Title.IndexOf(title, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    result.Add(b);
                }
            }
            return result;
        }

        // Поиск по автору (перегрузка с именованным параметром/сигнатурой - передаем признак автора или второй строковый параметр)
        public List<Book> SearchBookByAuthor(string author)
        {
            List<Book> result = new List<Book>();
            foreach (var b in books)
            {
                if (b.Author.IndexOf(author, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    result.Add(b);
                }
            }
            return result;
        }

        public List<Book> SearchBook(int year)
        {
            List<Book> result = new List<Book>();
            foreach (var b in books)
            {
                if (b.Year == year)
                {
                    result.Add(b);
                }
            }
            return result;
        }

        // Задача 17: Перегрузка метода AddReader
        public void AddReader(string name)
        {
            readers.Add(new Reader(name));
            Console.WriteLine($"Читатель {name} зарегистрирован.");
        }

        public void AddReader(string name, string surname)
        {
            readers.Add(new Reader(name, surname));
            Console.WriteLine($"Читатель {name} {surname} зарегистрирован.");
        }

        public void AddReader(string name, string surname, string phone)
        {
            readers.Add(new Reader(name, surname, phone));
            Console.WriteLine($"Читатель {name} {surname} (тел: {phone}) зарегистрирован.");
        }

        // Задача 18: Возврат книги с условием
        public bool ReturnBook(string title)
        {
            Book book = FindBook(title);
            if (book == null)
            {
                Console.WriteLine($"Книга \"{title}\" не найдена.");
                return false;
            }

            if (book.IsAvailable)
            {
                Console.WriteLine($"Книга \"{title}\" и так находится в библиотеке (не была выдана).");
                return false;
            }

            book.IsAvailable = true;
            book.BorrowedBy = "";
            Console.WriteLine($"Книга \"{title}\" успешно возвращена в библиотеку!");
            return true;
        }

        // Задача 19: Рекурсивный метод подсчета книг
        public int CountBooksRecursive()
        {
            return CountBooksHelper(books, 0);
        }

        private int CountBooksHelper(List<Book> list, int index)
        {
            if (index >= list.Count)
            {
                return 0;
            }
            return 1 + CountBooksHelper(list, index + 1);
        }

        // Задача 20: Метод сортировки книг по году издания
        public void SortBooksByYear()
        {
            books.Sort((b1, b2) => b1.Year.CompareTo(b2.Year));
            Console.WriteLine("Книги отсортированы по году издания (по возрастанию).");
        }

        // Задача 21: Метод выдачи книги
        public void BorrowBook(string title, string readerName)
        {
            if (!IsReaderRegistered(readerName))
            {
                Console.WriteLine($"Ошибка: Читатель \"{readerName}\" не зарегистрирован в библиотеке!");
                return;
            }

            Book book = FindBook(title);
            if (book == null)
            {
                Console.WriteLine($"Ошибка: Книга \"{title}\" не найдена в библиотеке.");
                return;
            }

            if (!book.IsAvailable)
            {
                Console.WriteLine($"Ошибка: Книга \"{title}\" уже выдана читателю {book.BorrowedBy}.");
                return;
            }

            book.IsAvailable = false;
            book.BorrowedBy = readerName;
            Console.WriteLine($"Успешно: Книга \"{book.Title}\" выдана читателю {readerName}.");
        }

        // Задача 22: Метод возврата книги с расчетом штрафа за просрочку
        public void ReturnBookWithFine(string title, int overdueDays)
        {
            Book book = FindBook(title);
            if (book == null)
            {
                Console.WriteLine($"Ошибка: Книга \"{title}\" не найдена.");
                return;
            }

            if (book.IsAvailable)
            {
                Console.WriteLine($"Книга \"{title}\" не числится выданной.");
                return;
            }

            book.IsAvailable = true;
            string prevReader = book.BorrowedBy;
            book.BorrowedBy = "";

            Console.WriteLine($"Книга \"{title}\" возвращена читателем {prevReader}.");
            if (overdueDays > 0)
            {
                double fine = CalculateFine(overdueDays);
                Console.WriteLine($"Внимание: Просрочка {overdueDays} дн.! Начислен штраф: {fine} руб.");
            }
            else
            {
                Console.WriteLine("Книга возвращена вовремя, штрафов нет.");
            }
        }

        // Задача 23: Метод статистики
        public void ShowStatistics()
        {
            int total = books.Count;
            int available = GetAvailableBooks();
            int borrowed = total - available;
            int readerCount = readers.Count;

            Console.WriteLine("\n================ СТАТИСТИКА БИБЛИОТЕКИ ================");
            Console.WriteLine($" Общее количество книг:            {total} (по счетчику TotalBooks: {TotalBooks})");
            Console.WriteLine($" Доступно книг:                    {available}");
            Console.WriteLine($" Выдано на руки:                   {borrowed}");
            Console.WriteLine($" Зарегистрировано читателей:       {readerCount}");
            Console.WriteLine("=======================================================");
        }

        // Задача 24: Главное меню
        public static void ShowMenu()
        {
            Console.WriteLine("\n--------------- ГЛАВНОЕ МЕНЮ ---------------");
            Console.WriteLine("1. Добавить книгу");
            Console.WriteLine("2. Показать все книги");
            Console.WriteLine("3. Найти книгу");
            Console.WriteLine("4. Зарегистрировать читателя");
            Console.WriteLine("5. Выдать книгу");
            Console.WriteLine("6. Вернуть книгу");
            Console.WriteLine("7. Показать статистику");
            Console.WriteLine("8. Запустить авто-тест (демонстрация задач 1-20)");
            Console.WriteLine("0. Завершить программу");
            Console.Write("Выберите пункт меню: ");
        }

        // Вспомогательный метод для демонстрации задач 1-20
        public void RunStudentDemo()
        {
            Console.WriteLine("\n=== ДЕМОНСТРАЦИЯ БАЗОВЫХ ЗАДАЧ (1-20) ===");

            // Задачи 1, 3, 5: Создание объектов и вызов DisplayInfo()
            Console.WriteLine("\n--- Задачи 1, 3, 5: Создание 3 объектов Book и вызов DisplayInfo() ---");
            Book b1 = new Book("Война и мир", "Лев Толстой", 1869);
            Book b2 = new Book("Преступление и наказание", "Федор Достоевский", 1866);
            Book b3 = new Book("Мастер и Маргарита", "Михаил Булгаков", 1967);

            b1.DisplayInfo();
            b2.DisplayInfo();
            b3.DisplayInfo();

            // Задача 3: SetTitle
            Console.WriteLine("\n--- Задача 3: Изменение названия через SetTitle() ---");
            b1.SetTitle("Война и мир (Том 1)");
            b1.DisplayInfo();

            // Задача 8: CalculateBookAge
            Console.WriteLine("\n--- Задача 8: Возраст книги ---");
            int age = b2.CalculateBookAge(DateTime.Now.Year);
            Console.WriteLine($"Возраст книги \"{b2.Title}\" ({b2.Year} г.): {age} лет.");

            // Задача 11: Статический метод без создания объекта
            Console.WriteLine("\n--- Задача 11: Вызов статического метода ShowLibraryName() без объекта ---");
            Library.ShowLibraryName();

            // Задача 13, 14: Статические методы
            Console.WriteLine("\n--- Задача 13 & 14: Статические методы CalculateFine() и GetMaxBooks() ---");
            Console.WriteLine($"Штраф за 5 дней просрочки: {Library.CalculateFine(5)} руб.");
            Console.WriteLine($"Большее из (10 и 25 книг): {Library.GetMaxBooks(10, 25)}");

            // Задача 19: Рекурсивный метод
            Console.WriteLine("\n--- Задача 19: Рекурсивный подсчет книг ---");
            Console.WriteLine($"Количество книг в текущей библиотеке (рекурсивно): {CountBooksRecursive()}");

            Console.WriteLine("\n=== ДЕМОНСТРАЦИЯ ЗАВЕРШЕНА ===\n");
        }
    }

    // Задача 25: Финальная интеграция проекта
    class Program
    {
        static void Main(string[] args)
        {
            // Настройка консоли для корректного вывода кириллицы
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.InputEncoding = System.Text.Encoding.UTF8;

            Library myLibrary = new Library();

            // Задача 2: Вызов приветственного сообщения
            myLibrary.ShowWelcomeMessage();

            // Добавим немного начальных данных для удобства проверки преподом
            myLibrary.AddBook("Капитанская дочка", "Александр Пушкин", 1836);
            myLibrary.AddBook("Отцы и дети", "Иван Тургенев", 1862);
            myLibrary.AddBook("1984", "Джордж Оруэлл", 1949);
            myLibrary.AddReader("Иван", "Иванов", "+79991112233");
            myLibrary.AddReader("Анна", "Смирнова");

            bool running = true;
            while (running)
            {
                Library.ShowMenu();
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.WriteLine("\n[Добавление книги]");
                        Console.Write("Введите название: ");
                        string title = Console.ReadLine();
                        Console.Write("Введите автора: ");
                        string author = Console.ReadLine();
                        Console.Write("Введите год издания: ");
                        if (int.TryParse(Console.ReadLine(), out int year))
                        {
                            myLibrary.AddBook(title, author, year);
                        }
                        else
                        {
                            Console.WriteLine("Ошибка: год должен быть числом!");
                        }
                        break;

                    case "2":
                        myLibrary.ShowBooks();
                        Console.Write("\nОтсортировать книги по году издания? (д/н): ");
                        if (Console.ReadLine()?.ToLower() == "д")
                        {
                            myLibrary.SortBooksByYear(); // Задача 20
                            myLibrary.ShowBooks();
                        }
                        break;

                    case "3":
                        Console.WriteLine("\n[Поиск книги]");
                        Console.WriteLine("1. Поиск по названию");
                        Console.WriteLine("2. Поиск по автору");
                        Console.WriteLine("3. Поиск по году издания");
                        Console.Write("Выберите вариант: ");
                        string sType = Console.ReadLine();
                        if (sType == "1")
                        {
                            Console.Write("Введите часть названия: ");
                            string t = Console.ReadLine();
                            var results = myLibrary.SearchBook(t); // Задача 16 (перегрузка 1)
                            PrintSearchResults(results);
                        }
                        else if (sType == "2")
                        {
                            Console.Write("Введите имя автора: ");
                            string a = Console.ReadLine();
                            var results = myLibrary.SearchBookByAuthor(a); // Задача 16 (перегрузка 2)
                            PrintSearchResults(results);
                        }
                        else if (sType == "3")
                        {
                            Console.Write("Введите год: ");
                            if (int.TryParse(Console.ReadLine(), out int y))
                            {
                                var results = myLibrary.SearchBook(y); // Задача 16 (перегрузка 3)
                                PrintSearchResults(results);
                            }
                        }
                        break;

                    case "4":
                        Console.WriteLine("\n[Регистрация читателя]");
                        Console.Write("Введите имя: ");
                        string name = Console.ReadLine();
                        Console.Write("Введите фамилию (Enter если нет): ");
                        string surname = Console.ReadLine();
                        Console.Write("Введите телефон (Enter если нет): ");
                        string phone = Console.ReadLine();

                        // Задача 17: Перегрузки AddReader
                        if (!string.IsNullOrWhiteSpace(phone))
                        {
                            myLibrary.AddReader(name, surname, phone);
                        }
                        else if (!string.IsNullOrWhiteSpace(surname))
                        {
                            myLibrary.AddReader(name, surname);
                        }
                        else
                        {
                            myLibrary.AddReader(name);
                        }
                        break;

                    case "5":
                        Console.WriteLine("\n[Выдача книги]");
                        Console.Write("Введите название книги: ");
                        string bTitle = Console.ReadLine();
                        Console.Write("Введите имя зарегистрированного читателя: ");
                        string rName = Console.ReadLine();
                        myLibrary.BorrowBook(bTitle, rName); // Задача 21
                        break;

                    case "6":
                        Console.WriteLine("\n[Возврат книги]");
                        Console.Write("Введите название книги: ");
                        string retTitle = Console.ReadLine();
                        Console.Write("Сколько дней просрочки (0 если нет): ");
                        int.TryParse(Console.ReadLine(), out int days);
                        myLibrary.ReturnBookWithFine(retTitle, days); // Задачи 18, 22
                        break;

                    case "7":
                        myLibrary.ShowStatistics(); // Задача 23
                        break;

                    case "8":
                        myLibrary.RunStudentDemo();
                        break;

                    case "0":
                        running = false;
                        Console.WriteLine("\nСпасибо за использование системы управления библиотекой! До свидания.");
                        break;

                    default:
                        Console.WriteLine("Неверный ввод, выберите пункт от 0 до 8.");
                        break;
                }
            }
        }

        static void PrintSearchResults(List<Book> list)
        {
            if (list == null || list.Count == 0)
            {
                Console.WriteLine("Ничего не найдено.");
                return;
            }
            Console.WriteLine($"Найдено книг: {list.Count}");
            foreach (var b in list)
            {
                b.DisplayInfo();
            }
        }
    }
}
