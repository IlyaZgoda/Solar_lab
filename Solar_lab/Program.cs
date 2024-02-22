using Solar_lab;

PersonController PersonList = new PersonController();
string filePath = "save.txt";
PersonList.LoadDataFromFileAsync(filePath);

PersonList.ShowUpcomingBdays();

while (true)
{
    Console.WriteLine(
                  "\n1 - Отображение всего списка ДР \n"
                + "2 - ближайшие ДР \n"
                + "3 - добавление ДР \n"
                + "4 - удаление ДР \n"
                + "5 - редактирование ДР \n"
                + "6 - сохранить в файл \n");

    string input = Console.ReadLine();
    int choice;
    Console.WriteLine();

    if (int.TryParse(input, out choice))
    {
        switch (choice)
        {
            case 1:
                PersonList.ShowList();
                break;
            case 2:
                PersonList.ShowUpcomingBdays();
                break;
            case 3:
                PersonList.AddPerson();
                break;
            case 4:
                PersonList.DeletePerson();
                break;
            case 5:
                PersonList.UpdatePerson();
                break;
            case 6:
                PersonList.SaveDataToFile(filePath);
                break;
            default:
                Console.WriteLine("Неправильный ввод данных");
                break;
        }
    }   
}