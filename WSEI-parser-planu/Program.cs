using System;
using OpenQA.Selenium.Firefox;


namespace WSEI_parser_planu {
    class Program {
        public static void Main() {
            var driver = new FirefoxDriver();
            driver.Navigate().GoToUrl("https://dziekanat.wsei.edu.pl/Plany/PlanProwadzacego");
            Console.WriteLine("Zaloguj się i przejdź na stronę, która wyświetli plan. Wybierz \"Cały semestr\" i kliknij szukaj.\n Naciśnij Enter jak skończysz");
            Console.ReadLine();
            var scheduleHtml = driver.PageSource; 
            var schedule = new Schedule(scheduleHtml);
            schedule.SaveToCsv();
        }
    }
}