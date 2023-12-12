using System;
using System.Collections.Generic;
using System.Windows;
using Exversion.Analytics;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.IO;
using SupportLib;
using System.Xml.Serialization;
using WpfMath;

namespace Bystroschot
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string[] test1var, test2var;
        string formula_in_math_format_1var, formula_in_Tex_format_1var, formula_in_math_format_2var, formula_in_Tex_format_2var;
        int idx = 0;
        public static User user = new User();
        List<User> users = new List<User>();//хранение истории
        public MainWindow()
        {
            InitializeComponent();
            StartWorking();
            Restore();
        }
        public void StartWorking()
        {
            MainGridWindow.Children.Clear();

            MainLabel.Content = "Быстросчёт";
            MainGridWindow.Children.Add(MainLabel);
            Grid.SetRow(MainLabel, 0);
            MainGridWindow.Children.Add(CloseBtn);
            Grid.SetRow(CloseBtn, 0);

            Grid grid = new Grid() { };
            grid.ShowGridLines = true;
            grid.RowDefinitions.Add(new RowDefinition());
            grid.RowDefinitions.Add(new RowDefinition());
            grid.RowDefinitions.Add(new RowDefinition());
            grid.ColumnDefinitions.Add(new ColumnDefinition());
            grid.ColumnDefinitions.Add(new ColumnDefinition());
            grid.ColumnDefinitions.Add(new ColumnDefinition());
            MainGridWindow.Children.Add(grid);          
            Grid.SetRow(grid, 1);

            Button TestButton = new Button()
            {
                Height = 80,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Center,
                Content = "Начать тест",
                FontSize = 40,
                Background = new SolidColorBrush(Color.FromRgb(192, 192, 255)),
                Style = (Style)Application.Current.Resources["RoundButton"]
        };
            TestButton.Click += TestButton_Click;
            Button HistoryButton = new Button()
            {
                Height = 80,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Center,
                Content = "История",
                FontSize = 40,
                Background = new SolidColorBrush(Color.FromRgb(192, 192, 255)),
                Style = (Style)Application.Current.Resources["RoundButton"]
            };
            HistoryButton.Click += HistoryButton_Click;
            
            grid.Children.Add(TestButton);
            Grid.SetRow(TestButton, 0);
            Grid.SetColumn(TestButton, 1);

            grid.Children.Add(HistoryButton);
            Grid.SetRow(HistoryButton, 1);
            Grid.SetColumn(HistoryButton, 1);
        }
        private void HistoryButton_Click(object sender, RoutedEventArgs e)
        {
            MainGridWindow.Children.Clear();

            MainLabel.Content = "История";
            MainGridWindow.Children.Add(MainLabel);
            Grid.SetRow(MainLabel, 0);
            MainGridWindow.Children.Add(CloseBtn);
            Grid.SetRow(CloseBtn, 0);
            MainGridWindow.Children.Add(MainHome);
            Grid.SetRow(MainHome, 0);

            Grid grid = new Grid() { };
            grid.ShowGridLines = true;
            grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(4, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(3, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            MainGridWindow.Children.Add(grid);
            Grid.SetRow(grid, 1);


            ListBox ListOfSessions = new ListBox() {HorizontalAlignment=HorizontalAlignment.Stretch,
                VerticalAlignment=VerticalAlignment.Stretch, FontSize=20,
                SelectionMode = SelectionMode.Single};
            foreach(User u in users)
            {
                ListOfSessions.Items.Add(u);
            }
            
            grid.Children.Add(ListOfSessions);
            Grid.SetRow(ListOfSessions, 0);
            Grid.SetColumn(ListOfSessions, 0);

            Button Delete = new Button()
            {
                Height = 80,
                Width = 350,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Content = "Удалить",
                FontSize = 40,
                Background = new SolidColorBrush(Color.FromRgb(192, 192, 255)),
                Style = (Style)Application.Current.Resources["RoundButton"]
            };
            Delete.Click += DeleteItem;
            grid.Children.Add(Delete);
            Grid.SetRow(Delete, 0);
            Grid.SetColumn(Delete, 1);

            void DeleteItem(object sender4, RoutedEventArgs e4)
            {
                if (ListOfSessions.SelectedIndex == -1)
                    MessageBox.Show("Выберите строку для удаления");
                else
                {
                    users.RemoveAt(ListOfSessions.SelectedIndex);
                    ListOfSessions.Items.RemoveAt(ListOfSessions.SelectedIndex);
                }
            }
        }
        private void TestButton_Click(object sender, RoutedEventArgs e)
        {
            MainGridWindow.Children.Clear();

            MainGridWindow.Children.Add(MainLabel);
            Grid.SetRow(MainLabel, 0);
            MainGridWindow.Children.Add(CloseBtn);
            Grid.SetRow(CloseBtn, 0);
            MainGridWindow.Children.Add(MainHome);
            Grid.SetRow(MainHome, 0);

            Grid grid = new Grid();
            grid.ShowGridLines = true;
            grid.RowDefinitions.Add(new RowDefinition());
            grid.RowDefinitions.Add(new RowDefinition());
            grid.RowDefinitions.Add(new RowDefinition());
            grid.ColumnDefinitions.Add(new ColumnDefinition());
            grid.ColumnDefinitions.Add(new ColumnDefinition());
            grid.ColumnDefinitions.Add(new ColumnDefinition());
            MainGridWindow.Children.Add(grid);
            Grid.SetRow(grid, 1);

            Label ClassLabel = new Label()
            {
                Content = "Класс",
                HorizontalContentAlignment = HorizontalAlignment.Center,
                VerticalContentAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Height = 80,
                Width = 300,
                FontSize = 40,
                Background = new SolidColorBrush(Color.FromRgb(192, 192, 255))
            };
            TextBox EnterClass = new TextBox()
            {
                Height = 80,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Center,
                FontSize = 40
            };
            EnterClass.KeyDown += TextBoxKeyDown;
            grid.Children.Add(EnterClass);
            grid.Children.Add(ClassLabel);
            Grid.SetRow(EnterClass, 0);
            Grid.SetColumn(EnterClass, 1);
            Grid.SetRow(ClassLabel, 0);
            Grid.SetColumn(ClassLabel, 0);

            void TextBoxKeyDown(object sender0, KeyEventArgs e0)
            {
                if (e0.Key == Key.Enter)
                {
                    if (EnterClass.Text != "")
                    {
                        user._name = EnterClass.Text;
                        Label TopicChoice = new Label()
                        {
                            Content = "Выберите тему",
                            HorizontalContentAlignment = HorizontalAlignment.Center,
                            VerticalContentAlignment = VerticalAlignment.Center,
                            HorizontalAlignment = HorizontalAlignment.Center,
                            VerticalAlignment = VerticalAlignment.Center,
                            Height = 80,
                            Width = 300,
                            FontSize = 40,
                            Background = new SolidColorBrush(Color.FromRgb(192, 192, 255))
                        };
                        ComboBox TopicBox = new ComboBox()
                        {
                            Height = 80,
                            HorizontalAlignment = HorizontalAlignment.Stretch,
                            FontSize = 40
                        };
                        //====================================Для примера
                        TopicBox.Items.Add("Логарифмы");
                        
                        //=====================================
                        TopicBox.SelectionChanged += TopicChanged;
                        grid.Children.Add(TopicChoice);
                        grid.Children.Add(TopicBox);
                        Grid.SetRow(TopicChoice, 1);
                        Grid.SetColumn(TopicChoice, 0);
                        Grid.SetRow(TopicBox, 1);
                        Grid.SetColumn(TopicBox, 1);

                        void TopicChanged(object sender1, SelectionChangedEventArgs e1)
                        {
                            user._theme = TopicBox.SelectedItem.ToString();
                            Label TestChoice = new Label()
                            {
                                Content = "Выберите тест",
                                HorizontalContentAlignment = HorizontalAlignment.Center,
                                VerticalContentAlignment = VerticalAlignment.Center,
                                HorizontalAlignment = HorizontalAlignment.Center,
                                VerticalAlignment = VerticalAlignment.Center,
                                Height = 80,
                                Width = 300,
                                FontSize = 40,
                                Background = new SolidColorBrush(Color.FromRgb(192, 192, 255))
                            };
                            ComboBox TestTopicBox = new ComboBox()
                            {
                                Height = 80,
                                HorizontalAlignment = HorizontalAlignment.Stretch,
                                FontSize = 40
                            };
                            TestTopicBox.SelectionChanged += TestChanged;
                            //==================================== примеры
                            TestTopicBox.Items.Add("А");
                            TestTopicBox.Items.Add("Б"); 
                            TestTopicBox.Items.Add("В");
                            //====================================примеры
                            grid.Children.Add(TestChoice);
                            grid.Children.Add(TestTopicBox);
                            Grid.SetRow(TestChoice, 2);
                            Grid.SetColumn(TestChoice, 0);
                            Grid.SetRow(TestTopicBox, 2);
                            Grid.SetColumn(TestTopicBox, 1);
                            void TestChanged(object sender2, SelectionChangedEventArgs e2)
                            {
                                user._test = TestTopicBox.SelectedItem.ToString();
                                Button StartTestButton = new Button()
                                {
                                    Height = 80,
                                    HorizontalAlignment = HorizontalAlignment.Center,
                                    VerticalAlignment = VerticalAlignment.Center,
                                    Content = "Приступить к тесту",
                                    FontSize = 40,
                                    Background = new SolidColorBrush(Color.FromRgb(192, 192, 255)),
                                    Style = (Style)Application.Current.Resources["RoundButton"]
                                };
                                StartTestButton.Click += StartTestButton_Click;
                                grid.Children.Add(StartTestButton);
                                Grid.SetRow(StartTestButton, 2);
                                Grid.SetColumn(StartTestButton, 2);
                            }                            
                        };
                    }
                    else
                    {
                        MessageBox.Show("Внесите данные");
                    }
                }
            }          
        }                      
        private void StartTestButton_Click(object sender, RoutedEventArgs e)
        {           
            //=========Здесь закидываем экземпляр класса юзер в общий список================
            User user1 = new User();
            user1._name = user._name;
            user1._theme = user._theme;
            user1._test= user._test;
            users.Add(user1);
            if(user1._test == "Б")
            {
                test1var = File.ReadAllLines("..\\Debug\\files\\Б(1).txt");
                test2var = File.ReadAllLines("..\\Debug\\files\\Б(2).txt");
            }
            else if (user1._test == "А")
            {
                test1var = File.ReadAllLines("..\\Debug\\files\\А(1).txt");
                test2var = File.ReadAllLines("..\\Debug\\files\\А(2).txt");
                
            }
            else if (user1._test == "В")
            {
                test1var = File.ReadAllLines("..\\Debug\\files\\В(1).txt");
                test2var = File.ReadAllLines("..\\Debug\\files\\В(2).txt");
            }
            int len = test1var.Length;
            idx = 0;
            MainGridWindow.Children.Clear();

            MainLabel.Content = "Тема: " + user._theme + "Тест: " + user._test;
            MainGridWindow.Children.Add(MainLabel);
            Grid.SetRow(MainLabel, 0);
            MainGridWindow.Children.Add(CloseBtn);
            Grid.SetRow(CloseBtn, 0);
            MainGridWindow.Children.Add(MainHome);
            Grid.SetRow(MainHome, 0);

            Grid grid = new Grid();
            grid.ShowGridLines = true;
            grid.RowDefinitions.Add(new RowDefinition() {Height= new GridLength(4, GridUnitType.Star)});
            grid.RowDefinitions.Add(new RowDefinition() {Height = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition());
            grid.ColumnDefinitions.Add(new ColumnDefinition());
            MainGridWindow.Children.Add(grid);
            Grid.SetRow(grid, 1);

            Button ShowFormula = new Button()
            {
                Height = 80,
                Width = 400,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Content = "Показать формулу",
                FontSize = 40,
                Background = new SolidColorBrush(Color.FromRgb(192, 192, 255)),
                Style = (Style)Application.Current.Resources["RoundButton"]
            };
            Button Return = new Button()
            {
                Height = 80,
                Width = 400,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Content = "Назад",
                FontSize = 40,
                Background = new SolidColorBrush(Color.FromRgb(192, 192, 255)),
                Style = (Style)Application.Current.Resources["RoundButton"]
                
            };
           
            Return.Click += GoBack;
            grid.Children.Add(Return);//0 ребенок
            Grid.SetRow(Return, 1);        
                       
            
            ShowFormula.Click += ShowContent;
            grid.Children.Add(ShowFormula);//1 ребенок
            Grid.SetRow(ShowFormula, 1);
            Grid.SetColumn(ShowFormula, 1);
            PrintFormula(grid, 0);

            //метод для кнопки назад
            void GoBack(object sender1, RoutedEventArgs e1)
            {
                if (idx == len-1)
                {
                    ShowFormula.Content = "Показать формулу";
                    ShowFormula.Click += ShowContent;
                    ShowFormula.Click -= GoHome;

                }
                if (idx > 0)
                {
                    idx--;
                    PrintFormula(grid, idx);
                }
            }
            void ShowContent(object sender1, RoutedEventArgs e1)
            {               
                if (idx < len) //предусматривается, что все файлы тестов для разных вариантов будут одной длины
                {
                    idx++;
                    PrintFormula(grid, idx);
                    if (idx == len-1)
                    {
                        ShowFormula.Content = "Завершить тест";
                        ShowFormula.Click -= ShowContent;
                        ShowFormula.Click += GoHome;
                    }
                }
               
            }
        }
        private void PrintFormula(Grid grid, int index)
        {
            formula_in_math_format_1var = test1var[index];
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
            if (grid.Children.Count > 2)
            {
                grid.Children.RemoveAt(3);
                grid.Children.RemoveAt(2);
            }
            Image imageFirstVar = new Image() { HorizontalAlignment = HorizontalAlignment.Stretch, VerticalAlignment = VerticalAlignment.Stretch, Margin= new Thickness(50, 50, 50, 50) };
            imageFirstVar.Source = bitmapImage;
            Grid.SetColumn(imageFirstVar, 0);
            Grid.SetRow(imageFirstVar, 0);
            grid.Children.Add(imageFirstVar);//2 ребёнок

            //2variant
            formula_in_math_format_2var = test2var[index];
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
            
            Image imageSecondVar = new Image() { HorizontalAlignment = HorizontalAlignment.Stretch, VerticalAlignment = VerticalAlignment.Stretch, Margin = new Thickness(50,50,50,50) };
            imageSecondVar.Source = bitmapImage2;
            Grid.SetColumn(imageSecondVar, 1);
            Grid.SetRow(imageSecondVar, 0);
            grid.Children.Add(imageSecondVar);//3 ребёнок
        }
        private void GoHome(object sender, RoutedEventArgs e)
        {
            StartWorking();
        }        
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Вы хотите выйти из приложения?", "Подтверждение выхода", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                Save();
                // Закрытие приложения
                System.Windows.Application.Current.Shutdown();
            }
        }
        public void Save()
        {
            XmlSerializer seralize = new XmlSerializer(typeof(List<User>), new Type[] { typeof(User) });
            using (FileStream file = new FileStream("history.xml", FileMode.OpenOrCreate))
            {
                seralize.Serialize(file, users);
            }
        }
        public void Restore()
        {            
            string[] strok = File.ReadAllLines("history.xml");
            if (strok.Length != 0)
            {
                using (var file = new FileStream("history.xml", FileMode.OpenOrCreate))
                {
                    var xml = new XmlSerializer(typeof(List<User>), new Type[] { typeof(User) });
                    users = (List<User>)xml.Deserialize(file);
                }
            }                       
        }
    }
    

}
