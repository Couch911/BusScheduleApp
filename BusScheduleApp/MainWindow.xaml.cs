using System.Windows;

namespace BusScheduleApp
{
    public partial class MainWindow : Window
    {
        DataAccess DataConnection;

        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += Window_Loaded;
            this.Activated += Window_Activated;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshData();
            // На старті меню "Автобуси" приховане
            BusMenuItem.Visibility = Visibility.Collapsed;
        }

        private void Window_Activated(object sender, System.EventArgs e)
        {
            // Показуємо меню тільки для Admin (рівень 2)
            if (Authorization.logUser == 2)
            {
                BusMenuItem.Visibility = Visibility.Visible;
            }
            else
            {
                BusMenuItem.Visibility = Visibility.Collapsed;
            }
        }

        private void RefreshData()
        {
            DataConnection = new DataAccess();
            BusListDG.ItemsSource = DataConnection.bList;
        }

        private void LoadDataMenuItem_Click(object sender, RoutedEventArgs e)
        {
            RefreshData();
        }

        private void AuthMenuItem_Click(object sender, RoutedEventArgs e)
        {
            LogInForm logWnd = new LogInForm();
            logWnd.ShowDialog();
        }

        private void SaveDataMenuItem_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Дані збережено (імітація для звіту).");
        }
    }
}