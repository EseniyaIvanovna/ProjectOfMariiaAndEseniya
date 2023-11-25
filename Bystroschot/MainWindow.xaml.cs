using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfMath;
using Analytics;
using Exversion;
using Exversion.Analytics;
using Aspose.TeX;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace Bystroschot
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BaseConverter converter;
        public MainWindow()
        {
            InitializeComponent();
            StartWorking();
            
        }
        public void StartWorking()
        {
            MainGridWindow.Children.Clear();
            Label lable = new Label()
            {
                Content = "Быстросчёт",
                HorizontalContentAlignment = HorizontalAlignment.Center,
                VerticalContentAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Center,
                Height = 80,
                FontSize = 48
                //Background = new SolidColorBrush(Color.FromRgb(159, 159, 250))
            };


            MainGridWindow.Children.Add(lable);
            Grid.SetRow(lable, 0);

            Grid grid = new Grid() { };
            //grid.ShowGridLines = true;
            grid.RowDefinitions.Add(new RowDefinition());
            grid.RowDefinitions.Add(new RowDefinition());
            grid.RowDefinitions.Add(new RowDefinition());
            grid.ColumnDefinitions.Add(new ColumnDefinition());
            grid.ColumnDefinitions.Add(new ColumnDefinition());
            grid.ColumnDefinitions.Add(new ColumnDefinition());
            grid.RowDefinitions[0].Height = new GridLength(15, GridUnitType.Star);
            grid.RowDefinitions[1].Height = new GridLength(10, GridUnitType.Star);
            grid.RowDefinitions[2].Height = new GridLength(15, GridUnitType.Star);
            MainGridWindow.Children.Add(grid);
            //Canvas InterectiveCanvas = new Canvas() {Width=800, Height=420};
            //MainGridWindow.Children.Add(InterectiveCanvas);
            Grid.SetRow(grid, 1);

            ////как сделать кнопку скруглённой???
            //Style buttonStyle = new Style(typeof(Button));
            //Setter cornerRadiusSetter = new Setter(Border.CornerRadiusProperty, new CornerRadius(50));
            //buttonStyle.Setters.Add(cornerRadiusSetter);



            Button TestButton = new Button()
            {
                MinWidth = 200,
                Height = 80,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Center,
                Content = "Начать тест",
                FontSize = 24,
                Background = new SolidColorBrush(Color.FromRgb(192, 192, 255)),
                Style = (Style)Application.Current.Resources["RoundButton"]
        };
            TestButton.Click += TestButton_Click;
            Button HistoryButton = new Button()
            {
                MinWidth = 200,
                Height = 80,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Center,
                Content = "История",
                FontSize = 24,
                Background = new SolidColorBrush(Color.FromRgb(192, 192, 255)),
                Style = (Style)Application.Current.Resources["RoundButton"]
            };
            HistoryButton.Click += HistoryButton_Click;
            Button AddingButton = new Button()
            {
                MinWidth = 200,
                Height = 80,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Center,
                Content = "Настройки",
                FontSize = 24,
                Background = new SolidColorBrush(Color.FromRgb(192, 192, 255)),
                Style = (Style)Application.Current.Resources["RoundButton"]
            };

            grid.Children.Add(TestButton);
            Grid.SetRow(TestButton, 0);
            Grid.SetColumn(TestButton, 1);
            //Canvas.SetLeft(TestButton, 300);
            //Canvas.SetTop(TestButton, 50);

            grid.Children.Add(HistoryButton);
            Grid.SetRow(HistoryButton, 1);
            Grid.SetColumn(HistoryButton, 1);

            grid.Children.Add(AddingButton);
            Grid.SetRow(AddingButton, 2);
            Grid.SetColumn(AddingButton, 1);
        }

        private void HistoryButton_Click(object sender, RoutedEventArgs e)
        {
            Label label = (Label)MainGridWindow.Children[0];
            label.Content = "История";
            MainGridWindow.Children.RemoveAt(1);
            //Canvas InterectiveCanvas = new Canvas() { Width = 800, Height = 420 };
            //MainGridWindow.Children.Add(InterectiveCanvas);
            //Grid.SetRow(InterectiveCanvas, 1);
            Grid grid = new Grid();
            grid.RowDefinitions.Add(new RowDefinition() {MinHeight=260});
            grid.RowDefinitions.Add(new RowDefinition());
            grid.RowDefinitions.Add(new RowDefinition() {MinHeight=60});
            grid.ColumnDefinitions.Add(new ColumnDefinition() {MinWidth=410});
            grid.ColumnDefinitions.Add(new ColumnDefinition() {MinWidth=260});
            MainGridWindow.Children.Add(grid);
            Grid.SetRow(grid, 1);
            ListBox ListOfSessions = new ListBox() { Height = 250, HorizontalAlignment=HorizontalAlignment.Stretch};
            
            //Canvas.SetLeft(ListOfSessions, 50);
            //Canvas.SetTop(ListOfSessions, 50);
            

            Button Home = new Button()
            {
                Width = 250,
                Height = 50,
                Content = "Вернуться к началу",
                FontSize = 24,
                Background = new SolidColorBrush(Color.FromRgb(192, 192, 255)),
                Style = (Style)Application.Current.Resources["RoundButton"]
            };
            grid.Children.Add(ListOfSessions);
            Grid.SetRow(ListOfSessions, 0);
            Grid.SetColumn(ListOfSessions, 0);

            grid.Children.Add(Home);
            Grid.SetRow(Home, 2);
            Grid.SetColumn(Home, 1);
            //Canvas.SetLeft(Home, 510);
            //Canvas.SetTop(Home, 320);
            Home.Click += GoHome;
        }

        private void TestButton_Click(object sender, RoutedEventArgs e)
        {
            MainGridWindow.Children.RemoveAt(1);
            Canvas InterectiveCanvas = new Canvas() { Width = 800, Height = 420};
            MainGridWindow.Children.Add(InterectiveCanvas);
            Grid.SetRow(InterectiveCanvas, 1);

            Label EnrerData = new Label()
            {
                Content = "Класс",
                HorizontalContentAlignment = HorizontalAlignment.Center,
                VerticalContentAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Height = 50,
                Width = 150,
                FontSize = 24,
                Background = new SolidColorBrush(Color.FromRgb(159, 159, 250))
            };
            TextBox data = new TextBox()
            {
                Height = 50,
                Width = 200,
                FontSize = 24
            };
            data.KeyDown += TextBoxKeyDown;
            Label TopicChoice = new Label()
            {
                Content = "Выберите тему",
                HorizontalContentAlignment = HorizontalAlignment.Center,
                VerticalContentAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Height = 50,
                Width = 200,
                FontSize = 24,
                Background = new SolidColorBrush(Color.FromRgb(159, 159, 250))
            };
            ComboBox TopicBox = new ComboBox()
            {
                Height = 50,
                Width = 200,
                FontSize = 24
            };
            TopicBox.Items.Add("Тема1");
            TopicBox.SelectedItem = TopicBox.Items[0];
            TopicBox.Items.Add("Тема2");
            TopicBox.Items.Add("Тема3");
            TopicBox.SelectionChanged += TopicChanged;

            InterectiveCanvas.Children.Add(EnrerData);
            Canvas.SetLeft(EnrerData, 30);
            Canvas.SetTop(EnrerData, 50);
            InterectiveCanvas.Children.Add(data);
            Canvas.SetLeft(data, 260);
            Canvas.SetTop(data, 50);
            InterectiveCanvas.Children.Add(TopicChoice);
            Canvas.SetLeft(TopicChoice, 30);
            Canvas.SetTop(TopicChoice, 150);
            InterectiveCanvas.Children.Add(TopicBox);
            Canvas.SetLeft(TopicBox, 260);
            Canvas.SetTop(TopicBox, 150);


        }

        private void StartTestButton_Click(object sender, RoutedEventArgs e)
        {
            Canvas canva = (Canvas)MainGridWindow.Children[1];
            TextBox TextOfData = (TextBox)canva.Children[1];
            if (TextOfData.Text == "")
                MessageBox.Show("Заполните все поля выше");
            else
            {
                Label label = (Label)MainGridWindow.Children[0];
                label.Content = "Тема Тест  ";
                MainGridWindow.Children.RemoveAt(1);
                Canvas InterectiveCanvas = new Canvas() { Width = 800, Height = 420 };
                MainGridWindow.Children.Add(InterectiveCanvas);
                Grid.SetRow(InterectiveCanvas, 1);
                Button Home = new Button() { Width = 250, Height = 50, Content = "Вернуться к началу", FontSize = 24,
                    Background = new SolidColorBrush(Color.FromRgb(192, 192, 255)),
                    Style = (Style)Application.Current.Resources["RoundButton"]
                };
                InterectiveCanvas.Children.Add(Home);
                Canvas.SetLeft(Home, 510);
                Canvas.SetTop(Home, 320);
                Home.Click += GoHome;
                //ScrollViewer firstVariant = new ScrollViewer() { Height = 559, SnapsToDevicePixels = true, VerticalScrollBarVisibility = ScrollBarVisibility.Hidden, HorizontalScrollBarVisibility = ScrollBarVisibility.Auto };
                //Canvas.SetLeft(firstVariant, 10);
                //Canvas.SetTop(firstVariant, 182);

                //ScrollViewer secondVariant = new ScrollViewer();
                //Canvas.SetLeft(secondVariant, 432);
                //Canvas.SetTop(secondVariant, 182);
                //InterectiveCanvas.Children.Add(firstVariant);
                //InterectiveCanvas.Children.Add(secondVariant);

                //Обновленный вариант вывода картинок на экран
                Button ShowFormula = new Button()
                {
                    Width = 250,
                    Height = 50,
                    Content = "Показать формулу",
                    FontSize = 24,
                    Background = new SolidColorBrush(Color.FromRgb(192, 192, 255)),
                    Style = (Style)Application.Current.Resources["RoundButton"]
                };
                InterectiveCanvas.Children.Add(ShowFormula);
                Canvas.SetLeft(ShowFormula, 810);
                Canvas.SetTop(ShowFormula, 320);
                ShowFormula.Click += ShowContent;
            }
        }
        int idx = 0;
        private void ShowContent(object sender, RoutedEventArgs e) //метод для вывода на экран
        {
            Grid grid = new Grid();
            grid.RowDefinitions.Add(new RowDefinition());
            grid.RowDefinitions.Add(new RowDefinition());
            grid.ColumnDefinitions.Add(new ColumnDefinition());
            grid.ColumnDefinitions.Add(new ColumnDefinition());
            MainGridWindow.Children.Add(grid);
            Grid.SetRow(grid, 1);

            
            string[] test1var = File.ReadAllLines("1Вариант.txt"); // тут должно быть поле объекта типа User _test
            string[] test2var = File.ReadAllLines("2Вариант.txt");
            string formula_in_math_format_1var, formula_in_Tex_format_1var, formula_in_math_format_2var, formula_in_Tex_format_2var;

            if (idx < test1var.Length) //предусматривается, что все файлы тестов для разных вариантов будут одной длины
            {
               //1variant
                formula_in_math_format_1var = test1var[idx];
                var converter1 = new AnalyticsTeXConverter();
                formula_in_Tex_format_1var = converter1.Convert(formula_in_math_format_1var);//преобразование в Latex
                string path = Equation.CreateEquationFirstVariant(formula_in_Tex_format_1var);
                FileInfo f = new FileInfo(path);
                string AbsoluteUri = f.FullName;
                BitmapImage bitmapImage = new BitmapImage();
                using (FileStream stream = new FileStream(AbsoluteUri, FileMode.Open, FileAccess.Read))
                {
                    bitmapImage.BeginInit();
                    bitmapImage.CacheOption = BitmapCacheOption.OnLoad; 
                    bitmapImage.StreamSource = stream;
                    bitmapImage.EndInit();
                }

                grid.Children.Clear(); //да почему это не работает????!!!! откуда наслоение, если я очистила грид перед выводом
                Image imageFirstVar = new Image() { Width = 200, Height = 200 };
                imageFirstVar.Source = bitmapImage;
                Grid.SetColumn(imageFirstVar, 0);
                Grid.SetRow(imageFirstVar, 0);
                grid.Children.Add(imageFirstVar);

                //2variant
                formula_in_math_format_2var = test2var[idx];
                var converter2 = new AnalyticsTeXConverter();
                formula_in_Tex_format_2var = converter2.Convert(formula_in_math_format_2var);//преобразование в Latex
                string path2 = Equation.CreateEquationSecondVariant(formula_in_Tex_format_2var);
                FileInfo f2 = new FileInfo(path2);
                string AbsoluteUri2 = f2.FullName;
                BitmapImage bitmapImage2 = new BitmapImage();
                using (FileStream stream = new FileStream(AbsoluteUri2, FileMode.Open, FileAccess.Read))
                {
                    bitmapImage2.BeginInit();
                    bitmapImage2.CacheOption = BitmapCacheOption.OnLoad;
                    bitmapImage2.StreamSource = stream;
                    bitmapImage2.EndInit();
                }
                //grid.Children.Clear(); //не понимаю, почему, но если строчку разкомитить, то выводится будет только второй вариант
                Image imageSecondVar = new Image() { Width = 200, Height = 200 };
                imageSecondVar.Source = bitmapImage2;
                Grid.SetColumn(imageSecondVar, 1);
                Grid.SetRow(imageSecondVar, 0);
                grid.Children.Add(imageSecondVar);

                idx++;
                
            }

        }
        private void GoHome(object sender, RoutedEventArgs e)
        {
            StartWorking();
        }

        private void TopicChanged(object ender, SelectionChangedEventArgs e)
        {
            Label TestChoice = new Label()
            {
                Content = "Выберите тест",
                HorizontalContentAlignment = HorizontalAlignment.Center,
                VerticalContentAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Height = 50,
                Width = 200,
                FontSize = 24,
                Background = new SolidColorBrush(Color.FromRgb(159, 159, 250))
            };
            ComboBox TestTopicBox = new ComboBox()
            {
                Height = 50,
                Width = 200,
                FontSize = 24
            };

            TestTopicBox.Items.Add("Тест1");
            TestTopicBox.SelectedItem = TestTopicBox.Items[0];
            TestTopicBox.Items.Add("Тест2");
            TestTopicBox.Items.Add("Тест3");
            Canvas x = (Canvas)MainGridWindow.Children[1];
            x.Children.Add(TestChoice);
            Canvas.SetLeft(TestChoice, 30);
            Canvas.SetTop(TestChoice, 250);
            x.Children.Add(TestTopicBox);
            Canvas.SetLeft(TestTopicBox, 260);
            Canvas.SetTop(TestTopicBox, 250);
            Button StartTestButton = new Button() { Width = 250, Height = 50, Content = "Приступить к тесту", FontSize = 24,
                Background = new SolidColorBrush(Color.FromRgb(192, 192, 255)),
                Style = (Style)Application.Current.Resources["RoundButton"]
            };
            x.Children.Add(StartTestButton);
            Canvas.SetLeft(StartTestButton, 510);
            Canvas.SetTop(StartTestButton, 320);
            StartTestButton.Click += StartTestButton_Click;
        }


        private static void TextBoxKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                MessageBox.Show($"Данные внесены в Историю");
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
    static class Equation
    {
        public static string CreateEquationFirstVariant(string Latex)
        {
            const string fileName = @"Equation1Var.PNG";
            var parser = new TexFormulaParser();
            var formula = parser.Parse(Latex);
            var pngByte = formula.RenderToPng(30.0, 0.0, 0.0, "Arial");
            File.WriteAllBytes(fileName, pngByte);
            return fileName;
        }
        public static string CreateEquationSecondVariant(string Latex)
        {
            const string fileName = @"Equation2Var.PNG";
            var parser = new TexFormulaParser();
            var formula = parser.Parse(Latex);
            var pngByte = formula.RenderToPng(20.0, 0.0, 0.0, "Arial");
            File.WriteAllBytes(fileName, pngByte);
            return fileName;
        }
    }

}
