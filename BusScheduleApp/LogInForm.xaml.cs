using System.Windows;

namespace BusScheduleApp
{
    public partial class LogInForm : Window
    {
        public LogInForm()
        {
            InitializeComponent();
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            Authorization auth = new Authorization();
            int result = auth.LogCheck(logTextBox.Text, passwordTextBox.Password);

            if (result == 2)
            {
                MessageBox.Show("Ви увійшли як Admin", "Успіх", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            else if (result == 1)
            {
                MessageBox.Show("Ви увійшли як User", "Успіх", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Введіть правильні дані авторизації.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }
    }
}