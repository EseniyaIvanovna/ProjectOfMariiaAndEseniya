using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
using SupportLib;

namespace Bystroschot
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            StartWorking();
        }
        public void StartWorking() //это окно запускатся при старте приложения и при переходе к стартовому меню
        {
            MainGridWindow.Children.Clear(); //главное окно состоит из 2 половин. Тут обе очищаются
            
            MainGridWindow.Children.Add(MainLabel); //добавляем надпись Быстросчёт (0 ребёнок)
            Grid.SetRow(MainLabel, 0);

            MainGridWindow.Children.Add(CloseBtn); //добавляем кнопку выхода (1 ребёнок)
            Grid.SetRow(CloseBtn, 0);

            Grid grid = new Grid() { /*ShowGridLines= true*/}; //нижняя половина из 3 рядов и 3 столбцов (2 ребёнок)
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
            Grid.SetRow(grid, 1);

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
        }; //три кнопки основного меню
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

        private void CloseButton_Click(object sender, RoutedEventArgs e) 
        {
            //MessageBox.Show("Ok");
            MessageBoxResult result = MessageBox.Show("Вы хотите выйти из приложения?", "Подтверждение выхода", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                // Закрытие приложения
                System.Windows.Application.Current.Shutdown();
            }
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
            MainGridWindow.Children.Clear(); //главное окно состоит из 2 половин. Тут обе очищаются
            
            MainGridWindow.Children.Add(MainLabel);
            Grid.SetRow(MainLabel, 0);

            MainGridWindow.Children.Add(CloseBtn);
            Grid.SetRow(CloseBtn, 0);
            //============================================================================== // выше создавали рамку (надпись и кнопку)
            User user = new User(); //создаю экземпляр класса в котором будет храниться информация о текущем прохождении теста

            Grid grid = new Grid() { /*ShowGridLines = true*/ }; //нижняя половина из 3 рядов и 3 столбцов (2 ребёнок)
            //grid.ShowGridLines = true;
            grid.RowDefinitions.Add(new RowDefinition());
            grid.RowDefinitions.Add(new RowDefinition());
            grid.RowDefinitions.Add(new RowDefinition());
            grid.ColumnDefinitions.Add(new ColumnDefinition());
            grid.ColumnDefinitions.Add(new ColumnDefinition());
            grid.ColumnDefinitions.Add(new ColumnDefinition());
            MainGridWindow.Children.Add(grid);
            Grid.SetRow(grid, 1);

            Label EnterName = new Label()
            {
                Content = "Класс",
                HorizontalContentAlignment = HorizontalAlignment.Center,
                VerticalContentAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Center,
                Height = 80,
                Width = 300,
                FontSize = 35,
                BorderBrush = new SolidColorBrush(Color.FromRgb(192, 192, 255)),
                BorderThickness = new Thickness(5)
        };// лэйбл Класс
            grid.Children.Add(EnterName);
            Grid.SetRow(EnterName, 0);
            Grid.SetColumn(EnterName, 0);

            TextBox NameData = new TextBox() 
            {
                Height = 80,
                //Width = 200,
                HorizontalAlignment= HorizontalAlignment.Stretch,
                FontSize = 35,
            }; //куда вводим класс
            NameData.KeyDown += TextBoxKeyDown1;
            grid.Children.Add(NameData);
            Grid.SetRow(NameData, 0);
            Grid.SetColumn(NameData, 1);
            void TextBoxKeyDown1(object sender0, KeyEventArgs e0)//при вводе текста и нажатии ентер появятся окна дальше
            {
                if (e0.Key == Key.Enter)
                {
                    if (NameData.Text == "")
                    {
                        MessageBox.Show("Введите данные");
                    }
                    else
                    {
                        user._name = NameData.Text; //заполняем поле класса

                        Label TopicChoice = new Label()
                        {
                            Content = "Выберите тему",
                            HorizontalContentAlignment = HorizontalAlignment.Center,
                            VerticalContentAlignment = VerticalAlignment.Center,
                            HorizontalAlignment = HorizontalAlignment.Stretch,
                            VerticalAlignment = VerticalAlignment.Center,
                            Height = 80,
                            Width = 300,
                            FontSize = 35,
                            BorderBrush = new SolidColorBrush(Color.FromRgb(192, 192, 255)),
                            BorderThickness = new Thickness(5)
                        }; //лэйбл выбора темы
                        grid.Children.Add(TopicChoice);
                        Grid.SetRow(TopicChoice, 1);
                        Grid.SetColumn(TopicChoice, 0);

                        ComboBox TopicBox = new ComboBox()
                        {
                            Height = 80,
                            //Width = 200,
                            HorizontalAlignment = HorizontalAlignment.Stretch,
                            Background= Brushes.White,
                            FontSize = 35
                        }; //комбобокс с выбором темы
                        TopicBox.SelectionChanged += ChoseTopic;
                        grid.Children.Add(TopicBox);
                        Grid.SetRow(TopicBox, 1);
                        Grid.SetColumn(TopicBox, 1);

                        //==============это для примера
                        TopicBox.Items.Add("Тема1");
                        TopicBox.Items.Add("Тема2");
                        TopicBox.Items.Add("Тема3");
                        //==============

                        void ChoseTopic(object sender1, RoutedEventArgs e1) //после выбора темы появится выбор теста
                        {
                            user._theme = TopicBox.Text; // заполняем выбор темы

                            Label TestChoice = new Label()
                            {
                                Content = "Выберите тест",
                                HorizontalContentAlignment = HorizontalAlignment.Center,
                                VerticalContentAlignment = VerticalAlignment.Center,
                                HorizontalAlignment = HorizontalAlignment.Stretch,
                                VerticalAlignment = VerticalAlignment.Center,
                                Height = 80,
                                Width = 300,
                                FontSize = 35,
                                BorderBrush = new SolidColorBrush(Color.FromRgb(192, 192, 255)),
                                BorderThickness = new Thickness(5)
                            }; //лейбл выбор теста
                            grid.Children.Add(TestChoice);
                            Grid.SetRow(TestChoice, 2);
                            Grid.SetColumn(TestChoice, 0);
                            ComboBox TestTopicBox = new ComboBox()
                            {
                                Height = 80,
                                //Width = 200,
                                HorizontalAlignment = HorizontalAlignment.Stretch,
                                Background = Brushes.Aqua, //Можно ли изменить цвет комбобокса????????!!!!!!!!!!!1
                                FontSize = 35
                            }; //комбобокс с выбором теста
                            TestTopicBox.SelectionChanged += ChoseTest;
                            grid.Children.Add(TestTopicBox);
                            Grid.SetRow(TestTopicBox, 2);
                            Grid.SetColumn(TestTopicBox, 1);


                            //==============это для примера
                            TestTopicBox.Items.Add("Тест1");
                            TestTopicBox.Items.Add("Тест2");
                            TestTopicBox.Items.Add("Тест3");
                            //==============
                            void ChoseTest(object sender2, RoutedEventArgs e2)
                            {
                                user._test = TestTopicBox.Text; // заполняем выбор темы

                                Button StartTestButton = new Button()
                                {
                                    MinWidth = 300,
                                    Height = 80,
                                    HorizontalAlignment = HorizontalAlignment.Center,
                                    VerticalAlignment = VerticalAlignment.Center,
                                    Content = "Приступить к тесту",
                                    FontSize = 24,
                                    Background = new SolidColorBrush(Color.FromRgb(192, 192, 255)),
                                    Style = (Style)Application.Current.Resources["RoundButton"]
                                };//кнопка приступить к тесту, при её нажатии переходим к выполнению теста
                                StartTestButton.Click += StartTestButton_Click;
                                grid.Children.Add(StartTestButton);
                                Grid.SetRow(StartTestButton, 2);
                                Grid.SetColumn(StartTestButton, 2);
                            }
                        }
                    }                    
                }                
            }     
        }

        private void StartTestButton_Click(object sender, RoutedEventArgs e)
        {
            //Canvas canva = (Canvas)MainGridWindow.Children[1];
            //TextBox TextOfData = (TextBox)canva.Children[1];
            //if (TextOfData.Text == "")
            //    MessageBox.Show("Заполните все поля выше");
            //else
            //{
            //    Label label = (Label)MainGridWindow.Children[0];
            //    label.Content = "Тема Тест  ";
            //    MainGridWindow.Children.RemoveAt(1);
            //    Canvas InterectiveCanvas = new Canvas() { Width = 800, Height = 420 };
            //    MainGridWindow.Children.Add(InterectiveCanvas);
            //    Grid.SetRow(InterectiveCanvas, 1);
            //    Button Home = new Button() { Width = 250, Height = 50, Content = "Вернуться к началу", FontSize = 24,
            //        Background = new SolidColorBrush(Color.FromRgb(192, 192, 255)),
            //        Style = (Style)Application.Current.Resources["RoundButton"]
            //    };
            //    InterectiveCanvas.Children.Add(Home);
            //    Canvas.SetLeft(Home, 510);
            //    Canvas.SetTop(Home, 320);
            //    Home.Click += GoHome;
            //}
        }

        private void GoHome(object sender, RoutedEventArgs e)
        {
            StartWorking();
        }

    }
}
