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
using System.IO;
using Microsoft.Win32;
using LibMatr;
using Lib_10;

namespace Practos3
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
        int[,] matr;

        private void save_Click(object sender, RoutedEventArgs e)
        {
            //настройка элемента сохранить
            SaveFileDialog save = new SaveFileDialog();
            save.DefaultExt = ".txt";
            save.Filter = "Все файлы (*.*) | *.* |Текстовые файлы | *.txt";
            save.FilterIndex = 2;
            save.Title = "Сохранение ряда";

            //открытие окна
            if (save.ShowDialog() == true)

            {
                //создание потока
                StreamWriter file = new StreamWriter(save.FileName);
                //записываем размер матрицы в файл
                file.WriteLine(matr.Length);
                //запись в файл
                for (int i = 0; i < Convert.ToInt32(Stolbsy.Text); i++)
                {
                    for (int j = 0; j < Convert.ToInt32(Stroke.Text); j++)
                    {
                        file.WriteLine(matr[i, j]);
                    }
                }
                file.Close();
            }
        }

        private void info_Click(object sender, RoutedEventArgs e)//информация
        {
            MessageBox.Show("Выполнил Потапкин Павел. Задание:Дана матрица размера M × N. Найти номер ее столбца с наименьшим произведением элементов и вывести данный номер, а также значение наименьшегопроизведения.");
        }

        private void exit_Click(object sender, RoutedEventArgs e)//закрытие
        {
            this.Close();
        }

        private void open_Click(object sender, RoutedEventArgs e)
        {
            //настраиваем элемент открыть
            OpenFileDialog open = new OpenFileDialog();

            open.DefaultExt = ".txt";
            open.Filter = "Bce файлы (*.*) | *.* | текстовые файлы | *.txt";
            open.FilterIndex = 2;
            open.Title = "Открытие таблицы";

            //открытие окна
            if (open.ShowDialog() == true)
            {

                //создание потока
                StreamReader file = new StreamReader(open.FileName);

                //определение размера
                int len = Convert.ToInt32(file.ReadLine());


                for (int i = 0; i < Convert.ToInt32(Stolbsy.Text); i++)
                {
                    for (int j = 0; j < Convert.ToInt32(Stroke.Text); j++)
                    {
                        matr[i, j] = Convert.ToInt32(file.ReadLine()); ;
                    }
                }
                file.Close();

                //вывод
                Tablitsa.ItemsSource = Class1.ToDataTable(matr).DefaultView;
            }
        }
        private void Tablitsa_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            //ввод описание
            //двумерный
            int indexColumn = e.Column.DisplayIndex;
            int indexRow = e.Row.GetIndex();
            matr[indexColumn, indexRow] = Convert.ToInt32(((TextBox)e.EditingElement).Text);
        }
        private void Diap_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !(char.IsDigit(e.Text, 0));    //блокировка ввода некор.знач
        }
        private void Stroke_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !(char.IsDigit(e.Text, 0));     //блокировка ввода некор.знач
        }
        private void Stolbsy_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !(char.IsDigit(e.Text, 0));     //блокировка ввода некор.знач
        }

        private void Go_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                for (int i = 0; i < Diap.Text.Length; i++) //защита от пробела
                {
                    if (Diap.Text[i] == ' ')
                    {
                        MessageBox.Show("Некоректное значение!");
                        Diap.Text = null;
                        return;
                    }
                }

                for (int i = 0; i < Stroke.Text.Length; i++) //защита от пробела
                {
                    if (Stroke.Text[i] == ' ')
                    {
                        MessageBox.Show("Некоректное значение!");
                        Stroke.Text = null;
                        return;
                    }
                }

                for (int i = 0; i < Stolbsy.Text.Length; i++) //защита от пробела
                {
                    if (Stolbsy.Text[i] == ' ')
                    {
                        MessageBox.Show("Некоректное значение!");
                        Stolbsy.Text = null;
                        return;
                    }
                }


                //предел таблицы

                int CountStroke = Convert.ToInt32(Stroke.Text);
                int CountStolbsy = Convert.ToInt32(Stroke.Text);
                matr = new int[CountStroke, CountStolbsy];
                Tablitsa.ItemsSource = Class1.ToDataTable(matr).DefaultView;

                //предел по строкам
                int randMax = Convert.ToInt32(Diap.Text);

                //количество ячеек
                CountStroke = Convert.ToInt32(Stroke.Text);
                CountStolbsy = Convert.ToInt32(Stolbsy.Text);


                matr = new int[CountStroke, CountStolbsy];//создание матрицы

                ClassMatr.InflateMatr(CountStroke, CountStolbsy, randMax, ref matr);//использование функции заполнения

                Tablitsa.ItemsSource = Class1.ToDataTable(matr).DefaultView;//вывод на таблицу
            }
            catch
            {
                MessageBox.Show("Ошибка");
            }
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            ClassMatr.ClearMatr(Convert.ToInt32(Stroke.Text), Convert.ToInt32(Stolbsy.Text), ref matr);//функция очистки
            Tablitsa.ItemsSource = Class1.ToDataTable(matr).DefaultView;//вывод на таблицу
        }

        private void Rasch_Click(object sender, RoutedEventArgs e)
        {
            int rez;//результат
            double summ;
            Class2.MaxInMatrStroke(Convert.ToInt32(Stroke.Text), Convert.ToInt32(Stolbsy.Text), matr, out rez, out summ);//функция умножения всех элементов массива
            Summa.Text = Convert.ToString(summ);//вывод
            NumberOfStroke.Text = Convert.ToString(rez);
        }
    }
}

    

