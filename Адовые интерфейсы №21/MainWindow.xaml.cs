using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using Microsoft.Win32;
using System.IO;

static class VisualArray
{
    //Метод для одномерного массива
    public static DataTable ToDataTable<T>(this T[] arr)
    {
        var res = new DataTable();
        for (int i = 0; i < arr.Length; i++)
        {
            res.Columns.Add("col" + (i + 1), typeof(T));
        }
        var row = res.NewRow();
        for (int i = 0; i < arr.Length; i++)
        {
            row[i] = arr[i];
        }
        res.Rows.Add(row);
        return res;
    }
}

namespace Адовые_интерфейсы__21
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void exit_Click(object sender, RoutedEventArgs e)
        {
            // Добавляем данные для MessageBox
            string t1 = "Уверены, что хотите выйти?";
            string t2 = "Предупреждение";
            // Параметры кнопок MessageBox'a
            // В данном случае Да\Нет
            MessageBoxButton bt = MessageBoxButton.YesNo;
            // Подставляем иконку вопроса к тексту
            MessageBoxImage icon = MessageBoxImage.Question;
            // Создаем переменную для присваивания всех данных и присваиваем данные
            MessageBoxResult result;
            result = MessageBox.Show(t1, t2, bt, icon, MessageBoxResult.Yes);
            // Оператор switch помогает нам сменить окно, в случае положительного ответа в MessageBox
            switch (result)
            {
                // Описан только случай с положительным ответом, так как при отрицательном ничего не происходит
                case MessageBoxResult.Yes:
                    // Закрытие рабочего окна
                    this.Close();
                    break;
            }
        }

        int[] mas;

        private void create1_Click(object sender, RoutedEventArgs e)
        {
            ras1.IsEnabled = true; //Избавляемся от ошибки, пользователь не сможет произвести расчёт, пока таблица не создана
            int dp, stb;
            //Защита ввода
            bool f1 = Int32.TryParse(n.Text, out dp); 
            bool f2 = Int32.TryParse(kolk.Text, out stb);
            if (f1 == true && f2 == true) 
            {
                if (dp > 0 && stb > 0) //Контроль вводимых значений
                {
                    mas = new int[stb];
                    Random rnd = new Random();
                    for (int i = 0; i < stb; i++) //Задаём размер массива
                    {
                        mas[i] = rnd.Next(dp);
                    }
                    dg1.ItemsSource = VisualArray.ToDataTable(mas).DefaultView; //Заполнение
                }
                else
                {
                    // Очистка окна ввода
                    n.Clear();
                    kolk.Clear();  // Пойман на ошибке                
                    string t1 = "Недопустимое значение!";
                    string t2 = "Ошибочка";
                    MessageBoxButton button = MessageBoxButton.OK;
                    // Добавление иконки ошибки к тексту
                    MessageBoxImage icon = MessageBoxImage.Error;
                    // Переменная для объединения данных
                    MessageBoxResult result;
                    result = MessageBox.Show(t1, t2, button, icon, MessageBoxResult.Yes);
                }
            }
        }

        private void ras1_Click(object sender, RoutedEventArgs e) 
        {
            int sum = 0; //Придаём значение 0, чтобы при расчёте суммы не было ошибки
            int x;
            for (int i = 0; i < mas.Length; i++) //Перебираем значение нашего массива

            {
                x = mas[i]; //Присваиваем значение массива, чтобы  поставить ограничение
                if (x % 5 == 0) //Проверка на кратность пяти
                {
                    sum = sum + mas[i]; //Выполняем условие *смотреть "о программе\about"*
                }
            }
            viv1.Text = Convert.ToString(sum); //Выводим результат в текстбокс
        }


        private void about_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Горе-дизайнер Бирюков Георгий из ИСП-23. Ввести n целых чисел. Найти сумму чисел кратных 5. Результат вывести на экран");
        }
  

        private void anek_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Как называется главное место казни на Руси? Кол-центр"); //Выводим анекдот 
        }

        private void clir_Click(object sender, RoutedEventArgs e) //Очистка
        {
            ras1.IsEnabled = false;
            n.Clear();
                kolk.Clear();
            viv1.Clear();
        }

        private void random_Click(object sender, RoutedEventArgs e) 
        {
            ras1.IsEnabled = true;
            Random rnd = new Random();
            mas = new int[rnd.Next(0, 10)]; //Задаём диапозон заданных значений 
            for (int i = 0; i < mas.Length; i++)
            {
                mas[i] = rnd.Next(0, 10);
            }
            dg1.ItemsSource = VisualArray.ToDataTable(mas).DefaultView;
        }
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            //Создаём и настраиваем элемент
            SaveFileDialog save = new SaveFileDialog();
            save.DefaultExt = "сишарповаябимба.txt";
            save.Filter = "Все файлы(*.*) | *.* | Текстовые файлы | *.txt";
            save.FilterIndex = 2;
            save.Title = "Сохрание таблицы";

            //Открываем диалоговое окно и при успехе работаем с файлом
            if (save.ShowDialog() == true)
            {
                StreamWriter file = new StreamWriter(save.FileName); //Создаём поток для работы с файлом и указываем ему имя файла
                file.WriteLine(mas.Length); //Записываем размер массива в файл
                for (int i = 0; i < mas.Length; i++) //Записываем элемент массива в файл
                {
                    file.WriteLine(mas[i]);
                    
                }
                file.Close();
            }

        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            //Создаём и настраиваем элемент
            OpenFileDialog open = new OpenFileDialog();
            open.DefaultExt = "сишарповаябимба.txt";
            open.Filter = "Все файлы(*.*) | *.* | Текстовые файлы | *.txt";
                open.FilterIndex = 2;
            open.Title = "Открываем";
            //Открываем диаологовое окно и при успехе работаем с файлом
            if(open.ShowDialog()==true)
            {
                StreamReader file = new StreamReader(open.FileName); //Создамём поток для работы с файлом и указываем ему имя файла
                int len = Convert.ToInt32(file.ReadLine()); //Читаем размер массива
                mas = new int[len]; //Создаём массив
                for (int i=0; i<mas.Length; i++) //Считываем массив с файла
                {
                    mas[i] = Convert.ToInt32(file.ReadLine());                    
                }
                file.Close();
                dg1.ItemsSource = VisualArray.ToDataTable(mas).DefaultView; //Выводим массив на форму

            }

        }
    }
} 

