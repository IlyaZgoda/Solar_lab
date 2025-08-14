using System.Globalization;

namespace Solar_lab
{
    internal class PersonController
    {
       private readonly Dictionary<string, DateTime> persons = [];

        public void AddPerson()
        {
            Console.WriteLine("Введите имя человека:\n");

            string? name = InputName();

            Console.WriteLine();
            Console.WriteLine("Введите дату рождения в формате дд.ММ.гггг (день.месяц.год):\n");

            DateTime birthday = InputDateOfBirth();

            Console.WriteLine();

            persons.Add(name!, birthday);

            Console.WriteLine("Запись создана \n");
        }

        private bool ArePersonsExist() =>
            persons.Count > 0 || persons is not null;

        public void DeletePerson()
        {
            if (!ArePersonsExist())
            {
                Console.WriteLine("Список пуст");
                return;
            }

            Console.WriteLine("Введите имя человека:\n");

            string? name = InputName();

            if (persons.ContainsKey(name!))
            {
                persons.Remove(name!);
                Console.WriteLine("Запись удалена \n");
            }

            else
            {
                Console.WriteLine("Запись не найдена \n");
            }
        }

        public void UpdatePerson()
        {
            if (!ArePersonsExist())
            {
                Console.WriteLine("Список пуст");
                return;
            }

            Console.WriteLine("Введите имя человека:\n");

            string? name = InputName();

            if (persons.ContainsKey(name!))
            {
                Console.WriteLine("Введите новую дату рождения:\n");

                DateTime birthday = InputDateOfBirth();

                persons[name!] = birthday;

                Console.WriteLine("Запись обновлена \n");
            }

            else
            {
                Console.WriteLine("Запись не найдена \n");
            }
        }

        public void ShowList()
        {
            foreach(var person in persons)
            {
                Console.WriteLine($"{person.Key} {person.Value:dd.MM.yyyy}");
            }
        }

        public void ShowUpcomingBirthdays()
        {
            DateTime today = DateTime.Today;

            foreach (var pair in persons)
            {
                DateTime birthdayThisYear = new(today.Year, pair.Value.Month, pair.Value.Day);
                DateTime birthdayNextYear = new(today.Year + 1, pair.Value.Month, pair.Value.Day);

                DateTime nextBirthday = birthdayThisYear >= today
                    ? birthdayThisYear
                    : birthdayNextYear;

                int daysUntilBirthday = (nextBirthday - today).Days;

                if (daysUntilBirthday < 60)
                {
                    Console.WriteLine($"{pair.Key} {pair.Value:dd.MM.yyyy}, дней до дня рождения: {daysUntilBirthday}");
                }
            }
        }

        private DateTime InputDateOfBirth()
        {
            DateTime birth;

            string? input;

            do
            {
                input = Console.ReadLine();
            }
            while (!DateTime.TryParseExact(input, "dd.MM.yyyy", null, DateTimeStyles.None, out birth));

            return birth;
        }

        private string? InputName()
        {
            string? input;

            do
            {
                input = Console.ReadLine();
            }
            while (input?.Length == 0);

            return input;
        }

        public void SaveDataToFile(string filePath)
        {
            using StreamWriter writer = new(filePath);

            foreach (var person in persons)
            {
                writer.WriteLine($"{person.Key} {person.Value.ToString("dd.MM.yyyy")}");
            }

            Console.WriteLine("Данные сохранены \n");

            writer.Close();
        }

        public void LoadDataFromFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);
                foreach (string line in lines)
                {
                    string[] parts = line.Split(' ');
                    if (DateTime.TryParse(parts[1], out DateTime date))
                    {
                        persons[parts[0]] = date;                       
                    }
                }
            }
            else
            {
                Console.WriteLine("Файл не найден \n");
            }

        }
    }
}
