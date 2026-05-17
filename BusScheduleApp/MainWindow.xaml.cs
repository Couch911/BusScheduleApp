using System;
using System.Windows;

namespace BusScheduleApp
{
    public partial class MainWindow : Window
    {
        DataAccess DataConnection;
        private bool isAddingMode = false;

        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += Window_Loaded;
            this.Activated += Window_Activated;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshData();
            BusMenuItem.Visibility = Visibility.Collapsed;
        }

        private void Window_Activated(object sender, System.EventArgs e)
        {
            if (Authorization.logUser == 2)
            {
                BusMenuItem.Visibility = Visibility.Visible;
            }
            else
            {
                BusMenuItem.Visibility = Visibility.Collapsed;
                flightGroupBox.Visibility = Visibility.Hidden; 
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

        private void EditDataMenuItem_Click(object sender, RoutedEventArgs e)
        {
            isAddingMode = false;
            flightGroupBox.Visibility = Visibility.Visible;
            MessageBox.Show("Оберіть у списку рейс для редагування подвійним кліком", "Увага!", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void AddDataMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (DataConnection.bList.Count >= 85)
            {
                MessageBox.Show("Кількість записів перевищує ліміт. Видаліть або відредагуйте існуючі.", "Увага", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            isAddingMode = true;
            flightGroupBox.Visibility = Visibility.Visible;

            int newId = 1;
            foreach (var trip in DataConnection.bList)
            {
                if (trip.reys_id >= newId) newId = trip.reys_id + 1;
            }

            idTextBox.Text = newId.ToString();
            fromTextBox.Text = "";
            toTextBox.Text = "";
            seatsTextBox.Text = "";
            depTextBox.Text = "00:00";
            arrTextBox.Text = "00:00";
        }
        private void BusListDG_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (BusListDG.SelectedItem is BusTrip selectedTrip && !isAddingMode)
            {
                idTextBox.Text = selectedTrip.reys_id.ToString();
                fromTextBox.Text = selectedTrip.punkt_vidpravku;
                toTextBox.Text = selectedTrip.punkt_priznachennya;
                seatsTextBox.Text = selectedTrip.kiltist_mists_v_salonu.ToString();
                depTextBox.Text = selectedTrip.vidpravku.ToString("HH:mm");
                arrTextBox.Text = selectedTrip.pributtia.ToString("HH:mm");
            }
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int id = int.Parse(idTextBox.Text);
                string from = fromTextBox.Text;
                string to = toTextBox.Text;
                int seats = int.Parse(seatsTextBox.Text);

                TimeSpan depTime = TimeSpan.Parse(depTextBox.Text);
                TimeSpan arrTime = TimeSpan.Parse(arrTextBox.Text);

                DateTime depDate = DateTime.Today.Add(depTime);
                DateTime arrDate = DateTime.Today.Add(arrTime);

                BusTrip tripData = new BusTrip(id, from, to, seats, depDate, arrDate);

                DataConnection.SaveTripToDb(tripData, isAddingMode);

                MessageBox.Show("Дані успішно збережено в базу!", "Успіх", MessageBoxButton.OK, MessageBoxImage.Information);

                RefreshData();
                flightGroupBox.Visibility = Visibility.Hidden;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Перевірте правильність вводу (наприклад, час має бути HH:mm).\nДеталі: " + ex.Message, "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}