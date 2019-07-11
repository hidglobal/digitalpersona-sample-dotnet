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
using System.Windows.Shapes;

namespace UsingDigitalPersonaNetSDK
{
    public partial class InputBox : Window
    {
        private InputBox()
        {
            InitializeComponent();
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        public static string Show(string description)
        {
            var dialog = new InputBox();
            dialog.Owner = Application.Current.MainWindow;
            dialog.DescriptionTextBlock.Text = description;
            return dialog.ShowDialog() == true ? dialog.ValueTextBox.Text : null;
        }
    }
}
