using System.Globalization;

namespace Solar_lab
{
    internal class PersonController
    {
       private Dictionary<string, DateTime> persons = [];

        public void AddPerson()
        {
            Console.WriteLine("Введите имя человека:\n");
            string name = inputName();
            Console.WriteLine();
            Console.WriteLine("Введите дату рождения в формате дд.ММ.гггг (день.месяц.год):\n");
            DateTime birthday = InputDoB();
            Console.WriteLine();
            persons.Add(name, birthday);
            Console.WriteLine("Запись создана \n");
        }

        public void DeletePerson()
        {
            Console.WriteLine("Введите имя человека:\n");
            string name = inputName();

            if (persons.ContainsKey(name))
            {
                persons.Remove(name);
                Console.WriteLine("Запись удалена \n");
            }

            else Console.WriteLine("Запись не найдена \n");
        }

        public void UpdatePerson()
        {
            Console.WriteLine("Введите имя человека:\n");
            string name = inputName();

            if (persons.ContainsKey(name))
            {
                Console.WriteLine("Введите новую дату рождения:\n");
                DateTime birthday = InputDoB();
                persons[name] = birthday;
                Console.WriteLine("Запись обновлена \n");
            }

            else Console.WriteLine("Запись не найдена \n");
        }

        public void ShowList()
        {
            foreach(var person in persons)
            {
                Console.WriteLine($"{person.Key} {person.Value.ToString("dd.MM.yyyy")}");
            }
        }

        public void ShowUpcomingBdays()
        {
            DateTime today = DateTime.Today;

            foreach (var pair in persons)
            {
                DateTime nextBirthday = new DateTime(today.Year, pair.Value.Month, pair.Value.Day);

                int daysUntilBirthday = (nextBirthday - today).Days;
                if (daysUntilBirthday < 60)
                {
                    Console.WriteLine($"{pair.Key} {pair.Value.ToString("dd.MM.yyyy")}, дней до дня рождения: {daysUntilBirthday}");
                }
            }
        }
        DateTime InputDoB()
        {
            DateTime dob; // date of birth
            string input;

            do
            {
                input = Console.ReadLine();
            }
            while (!DateTime.TryParseExact(input, "dd.MM.yyyy", null, DateTimeStyles.None, out dob));

            return dob;
        }

        string inputName()
        {
            string input;

            do
            {
                input = Console.ReadLine();
            }
            while (input == "");

            return input;
        }

        public void SaveDataToFile(string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (var person in persons)
                {
                    writer.WriteLine($"{person.Key} {person.Value.ToString("dd.MM.yyyy")}");
                }
                Console.WriteLine("Данные сохранены \n");
                writer.Close();
            }
        }

        public void LoadDataFromFileAsync(string filePath)
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
