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
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DigitalPersona.Altus.VerifyIdentity.Sdk;

namespace UsingDigitalPersonaNetSDK
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        private void Log(string str)
        {
            LogTextBox.AppendText((str ?? String.Empty) + Environment.NewLine);
        }

        private void Log(string format, params object[] args)
        {
            Log(String.Format(format, args));
        }


        private void Authenticate_CurrentUser_Click(object sender, RoutedEventArgs e)
        {
            LogTextBox.Clear();

            try
            {
                var dialog = new AuthenticationDialog();
                dialog.Title = ".NET SDK Sample";
                dialog.Description = ((Button)sender).Content.ToString();
                dialog.ParentWindow = new WindowInteropHelper(this).Handle;

                Log("User name: {0}", dialog.UserName);
                Log("Show dialog");

                if (dialog.ShowDialog())
                {
                    Log("OK");
                    Log("User name: {0}", dialog.UserName);
                    Log("Authenticated credentials: {0}", String.Join(", ", dialog.AuthenticatedCredentials));
                }
                else
                {
                    Log("Cancelled");
                }
            }
            catch (Exception ex)
            {
                Log("Error: {0}", ex.Message);
            }
        }

        private void Authenticate_SpecifiedUser_Click(object sender, RoutedEventArgs e)
        {
            LogTextBox.Clear();

            try
            {
                var userName = InputBox.Show("User name:");

                Log("User name: {0}", userName);

                var dialog = new AuthenticationDialog();
                dialog.Title = ".NET SDK Sample";
                dialog.Description = ((Button)sender).Content.ToString();
                dialog.ParentWindow = new WindowInteropHelper(this).Handle;
                dialog.UserName = userName;

                Log("Show dialog");

                if (dialog.ShowDialog())
                {
                    Log("OK");
                    Log("User name: {0}", dialog.UserName);
                    Log("Authenticated credentials: {0}", String.Join(", ", dialog.AuthenticatedCredentials));
                }
                else
                {
                    Log("Cancelled");
                }
            }
            catch (Exception ex)
            {
                Log("Error: {0}", ex.Message);
            }
        }

        private void Authenticate_CusmonUserNameType_Click(object sender, RoutedEventArgs e)
        {
            LogTextBox.Clear();

            try
            {
                var userName = InputBox.Show("User name:");

                var userNameTypeValues = new Dictionary<string, int>();
                userNameTypeValues["SAM (domain\\user_name)"] = 3;
                userNameTypeValues["DpAccount (user_name)"] = 9;
                var userNameType = ChooseBox.Show<int>("User name type:", userNameTypeValues);

                Log("User name: {0}", userName);
                Log("User name: {0}", userNameType);

                var dialog = new AuthenticationDialog();
                dialog.Title = ".NET SDK Sample";
                dialog.Description = ((Button)sender).Content.ToString();
                dialog.ParentWindow = new WindowInteropHelper(this).Handle;
                dialog.UserName = userName;
                dialog.UserNameType = userNameType;

                Log("Show dialog");

                if (dialog.ShowDialog())
                {
                    Log("OK");
                    Log("User name: {0}", dialog.UserName);
                    Log("Authenticated credentials: {0}", String.Join(", ", dialog.AuthenticatedCredentials));
                }
                else
                {
                    Log("Cancel");
                }
            }
            catch (Exception ex)
            {
                Log("Error: {0}", ex.Message);
            }
        }

        private void Authenticate_CusmonPolicy_Click(object sender, RoutedEventArgs e)
        {
            LogTextBox.Clear();

            try
            {
                var policy = new ulong[] { 1 | 2, 4 }; // password+fingerprints or smartcards

                Log("Custom policy: {0}", String.Join(", ", policy));

                var dialog = new AuthenticationDialog();
                dialog.Title = ".NET SDK Sample";
                dialog.Description = ((Button)sender).Content.ToString();
                dialog.ParentWindow = new WindowInteropHelper(this).Handle;
                dialog.AuthenticationPolicy = policy;

                Log("Show dialog");

                if (dialog.ShowDialog())
                {
                    Log("OK");
                    Log("User name: {0}", dialog.UserName);
                    Log("Authenticated credentials: {0}", String.Join(", ", dialog.AuthenticatedCredentials));
                }
                else
                {
                    Log("Cancelled");
                }
            }
            catch (Exception ex)
            {
                Log("Error: {0}", ex.Message);
            }
        }

        private void Authenticate_CusmonPolicyModified_Click(object sender, RoutedEventArgs e)
        {
            LogTextBox.Clear();

            try
            {
                var dialog = new AuthenticationDialog();
                dialog.Title = ".NET SDK Sample";
                dialog.Description = ((Button)sender).Content.ToString();
                dialog.ParentWindow = new WindowInteropHelper(this).Handle;
                dialog.UserName = Environment.UserDomainName + "\\" + Environment.UserName;

                var policy = dialog.ReadAuthPolicy();
                Log("Original policy: {0}", String.Join(", ", policy));

                policy = policy.Where(p => (p & 1) != p).ToArray(); // remove password
                Log("Modified policy: {0}", String.Join(", ", policy));
                
                dialog.AuthenticationPolicy = policy;

                Log("Show dialog");

                if (dialog.ShowDialog())
                {
                    Log("OK");
                    Log("User name: {0}", dialog.UserName);
                    Log("Authenticated credentials: {0}", String.Join(", ", dialog.AuthenticatedCredentials));
                }
                else
                {
                    Log("Cancel");
                }
            }
            catch (Exception ex)
            {
                Log("Error: {0}", ex.Message);
            }
        }


        private void Identify_DefaultUserName_Click(object sender, RoutedEventArgs e)
        {
            LogTextBox.Clear();

            try
            {
                var dialog = new IdentificationDialog();
                dialog.Title = ".NET SDK Sample";
                dialog.Description = ((Button)sender).Content.ToString();
                dialog.ParentWindow = new WindowInteropHelper(this).Handle;

                Log("Show dialog");

                if (dialog.ShowDialog())
                {
                    Log("OK");
                    Log("User name: {0}", dialog.UserName);
                    Log("Authenticated credentials: {0}", String.Join(", ", dialog.AuthenticatedCredentials));
                }
                else
                {
                    Log("Cancelled");
                }
            }
            catch (Exception ex)
            {
                Log("Error: {0}", ex.Message);
            }
        }

        private void Identify_SpecefiedUserName_Click(object sender, RoutedEventArgs e)
        {
            LogTextBox.Clear();

            try
            {
                var dialog = new IdentificationDialog();
                dialog.Title = ".NET SDK Sample";
                dialog.Description = ((Button)sender).Content.ToString();
                dialog.ParentWindow = new WindowInteropHelper(this).Handle;
                dialog.UserName = "test1000";

                Log("UserName: {0}", dialog.UserName);
                Log("Show dialog");

                if (dialog.ShowDialog())
                {
                    Log("OK");
                    Log("User name: {0}", dialog.UserName);
                    Log("Authenticated credentials: {0}", String.Join(", ", dialog.AuthenticatedCredentials));
                }
                else
                {
                    Log("Cancelled");
                }
            }
            catch (Exception ex)
            {
                Log("Error: {0}", ex.Message);
            }
        }

        private void Identify_CustomPolicy_Click(object sender, RoutedEventArgs e)
        {
            LogTextBox.Clear();

            try
            {
                var policy = new ulong[] { 1 | 2, 4 }; // password+fingerprints or smartcards

                Log("Custom policy: {0}", String.Join(", ", policy));

                var dialog = new IdentificationDialog();
                dialog.Title = ".NET SDK Sample";
                dialog.Description = ((Button)sender).Content.ToString();
                dialog.ParentWindow = new WindowInteropHelper(this).Handle;
                dialog.AuthenticationPolicy = policy;

                Log("Show dialog");

                if (dialog.ShowDialog())
                {
                    Log("OK");
                    Log("User name: {0}", dialog.UserName);
                    Log("Authenticated credentials: {0}", String.Join(", ", dialog.AuthenticatedCredentials));
                }
                else
                {
                    Log("Cancelled");
                }
            }
            catch (Exception ex)
            {
                Log("Error: {0}", ex.Message);
            }
        }


        private void Authenticate_WriteSecret_Click(object sender, RoutedEventArgs e)
        {
            LogTextBox.Clear();

            try
            {
                var secretName = InputBox.Show("Secret name:");
                var secretValue = InputBox.Show("Secret value:");

                Log("Secret name: {0}", secretName);
                Log("Secret value: {0}", secretValue);

                var dialog = new AuthenticationDialog();
                dialog.Title = ".NET SDK Sample";
                dialog.Description = ((Button)sender).Content.ToString();
                dialog.ParentWindow = new WindowInteropHelper(this).Handle;

                Log("User name: {0}", dialog.UserName);
                Log("Show dialog");

                if (dialog.ShowDialogWriteSecret(secretName, secretValue))
                {
                    Log("Secret writed");
                    Log("Authenticated credentials: {0}", String.Join(", ", dialog.AuthenticatedCredentials));
                }
                else
                {
                    Log("Cancelled");
                }
            }
            catch (Exception ex)
            {
                Log("Error: {0}", ex.Message);
            }
        }

        private void Authenticate_ReadSecret_Click(object sender, RoutedEventArgs e)
        {
            LogTextBox.Clear();

            try
            {
                var secretName = InputBox.Show("Secret name:");

                Log("Secret name: {0}", secretName);

                var dialog = new AuthenticationDialog();
                dialog.Title = ".NET SDK Sample";
                dialog.Description = ((Button)sender).Content.ToString();
                dialog.ParentWindow = new WindowInteropHelper(this).Handle;

                Log("User name: {0}", dialog.UserName);
                Log("Show dialog");

                string secretValue;
                if (dialog.ShowDialogReadSecret(secretName, out secretValue))
                {
                    Log("Secret readed");
                    Log("Authenticated credentials: {0}", String.Join(", ", dialog.AuthenticatedCredentials));
                    Log("Secret value: {0}", secretValue);
                }
                else
                {
                    Log("Cancelled");
                }
            }
            catch (Exception ex)
            {
                Log("Error: {0}", ex.Message);
            }
        }

        private void Authenticate_DeleteSecret_Click(object sender, RoutedEventArgs e)
        {
            LogTextBox.Clear();

            try
            {
                var secretName = InputBox.Show("Secret name:");

                Log("Secret name: {0}", secretName);

                var dialog = new AuthenticationDialog();
                dialog.Title = ".NET SDK Sample";
                dialog.Description = ((Button)sender).Content.ToString();
                dialog.ParentWindow = new WindowInteropHelper(this).Handle;

                Log("User name: {0}", dialog.UserName);
                Log("Show dialog");

                if (dialog.ShowDialogWriteSecret(secretName, null))
                {
                    Log("Secret deleted");
                    Log("Authenticated credentials: {0}", String.Join(", ", dialog.AuthenticatedCredentials));
                }
                else
                {
                    Log("Cancelled");
                }
            }
            catch (Exception ex)
            {
                Log("Error: {0}", ex.Message);
            }
        }

        private void Authenticate_CheckSecretExistsCurrentUser_Click(object sender, RoutedEventArgs e)
        {
            LogTextBox.Clear();

            try
            {
                var secretName = InputBox.Show("Secret name:");

                Log("Secret name: {0}", secretName);

                var dialog = new AuthenticationDialog();
                dialog.Title = ".NET SDK Sample";
                dialog.Description = ((Button)sender).Content.ToString();
                dialog.ParentWindow = new WindowInteropHelper(this).Handle;

                Log("User name: {0}", dialog.UserName);

                var sercetExists = dialog.SecretExists(secretName);

                Log("Secret exists: {0}", sercetExists);
            }
            catch (Exception ex)
            {
                Log("Error: {0}", ex.Message);
            }
        }

        private void Authenticate_CheckSecretExistsSpecifiedUser_Click(object sender, RoutedEventArgs e)
        {
            LogTextBox.Clear();

            try
            {
                var userName = InputBox.Show("User name:");
                var secretName = InputBox.Show("Secret name:");

                Log("User name: {0}", userName);
                Log("Secret name: {0}", secretName);

                var dialog = new AuthenticationDialog();
                dialog.Title = ".NET SDK Sample";
                dialog.Description = ((Button)sender).Content.ToString();
                dialog.ParentWindow = new WindowInteropHelper(this).Handle;
                dialog.UserName = userName;

                var sercetExists = dialog.SecretExists(secretName);

                Log("Secret exists: {0}", sercetExists);
            }
            catch (Exception ex)
            {
                Log("Error: {0}", ex.Message);
            }
        }


        private void Identify_WriteSecret_Click(object sender, RoutedEventArgs e)
        {
            LogTextBox.Clear();

            try
            {
                var secretName = InputBox.Show("Secret name:");
                var secretValue = InputBox.Show("Secret value:");

                Log("Secret name: {0}", secretName);
                Log("Secret value: {0}", secretValue);

                var dialog = new IdentificationDialog();
                dialog.Title = ".NET SDK Sample";
                dialog.Description = ((Button)sender).Content.ToString();
                dialog.ParentWindow = new WindowInteropHelper(this).Handle;

                Log("Show dialog");

                if (dialog.ShowDialogWriteSecret(secretName, secretValue))
                {
                    Log("User name: {0}", dialog.UserName);
                    Log("Authenticated credentials: {0}", String.Join(", ", dialog.AuthenticatedCredentials));
                    Log("Secret writed");
                }
                else
                {
                    Log("Cancelled");
                }
            }
            catch (Exception ex)
            {
                Log("Error: {0}", ex.Message);
            }
        }

        private void Identify_ReadSecret_Click(object sender, RoutedEventArgs e)
        {
            LogTextBox.Clear();

            try
            {
                var secretName = InputBox.Show("Secret name:");

                Log("Secret name: {0}", secretName);

                var dialog = new IdentificationDialog();
                dialog.Title = ".NET SDK Sample";
                dialog.Description = ((Button)sender).Content.ToString();
                dialog.ParentWindow = new WindowInteropHelper(this).Handle;

                Log("Show dialog");

                string secretValue;
                if (dialog.ShowDialogReadSecret(secretName, out secretValue))
                {
                    Log("User name: {0}", dialog.UserName);
                    Log("Authenticated credentials: {0}", String.Join(", ", dialog.AuthenticatedCredentials));
                    Log("Secret readed");
                    Log("Secret value: {0}", secretValue);
                }
                else
                {
                    Log("Cancelled");
                }
            }
            catch (Exception ex)
            {
                Log("Error: {0}", ex.Message);
            }
        }

        private void Identify_DeleteSecret_Click(object sender, RoutedEventArgs e)
        {
            LogTextBox.Clear();

            try
            {
                var secretName = InputBox.Show("Secret name:");

                Log("Secret name: {0}", secretName);

                var dialog = new IdentificationDialog();
                dialog.Title = ".NET SDK Sample";
                dialog.Description = ((Button)sender).Content.ToString();
                dialog.ParentWindow = new WindowInteropHelper(this).Handle;

                Log("Show dialog");

                if (dialog.ShowDialogWriteSecret(secretName, null))
                {
                    Log("User name: {0}", dialog.UserName);
                    Log("Authenticated credentials: {0}", String.Join(", ", dialog.AuthenticatedCredentials));
                    Log("Secret deleted");
                }
                else
                {
                    Log("Cancelled");
                }
            }
            catch (Exception ex)
            {
                Log("Error: {0}", ex.Message);
            }
        }

        private void Identify_CheckSecretExists_Click(object sender, RoutedEventArgs e)
        {
            LogTextBox.Clear();

            try
            {
                var userName = InputBox.Show("User name:");
                var secretName = InputBox.Show("Secret name:");

                Log("User name: {0}", userName);
                Log("Secret name: {0}", secretName);

                var dialog = new IdentificationDialog();
                dialog.Title = ".NET SDK Sample";
                dialog.Description = ((Button)sender).Content.ToString();
                dialog.ParentWindow = new WindowInteropHelper(this).Handle;
                dialog.UserName = userName;

                var sercetExists = dialog.SecretExists(secretName);

                Log("Secret exists: {0}", sercetExists);
            }
            catch (Exception ex)
            {
                Log("Error: {0}", ex.Message);
            }
        }

        private void Identify_CheckSecretExistsWithDialog_Click(object sender, RoutedEventArgs e)
        {
            LogTextBox.Clear();

            try
            {
                var secretName = InputBox.Show("Secret name:");

                Log("Secret name: {0}", secretName);

                var dialog = new IdentificationDialog();
                dialog.Title = ".NET SDK Sample";
                dialog.Description = ((Button)sender).Content.ToString();
                dialog.ParentWindow = new WindowInteropHelper(this).Handle;

                if (dialog.ShowDialog())
                {
                    Log("OK");
                    Log("User name: {0}", dialog.UserName);
                    Log("Authenticated credentials: {0}", String.Join(", ", dialog.AuthenticatedCredentials));

                    var secretExists = dialog.SecretExists(secretName);

                    Log("Secret exists: {0}", secretExists);
                }
                else
                {
                    Log("Cancelled");
                }
            }
            catch (Exception ex)
            {
                Log("Error: {0}", ex.Message);
            }
        }


    }
}
