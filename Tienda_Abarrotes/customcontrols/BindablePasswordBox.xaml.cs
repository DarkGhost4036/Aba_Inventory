using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
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

namespace Proyecto1Esco.CustomControl
{
    /// <summary>
    /// Lógica de interacción para BindablePasswordBox.xaml
    /// </summary>
    public partial class BindablePasswordBox : UserControl
    {
        public BindablePasswordBox()
        {
            InitializeComponent();
            txtPassword.PasswordChanged += OnPasswordChanged;
        }
        public static readonly DependencyProperty
            PasswordPropety = DependencyProperty.Register(
                "Password", typeof(SecureString),
                typeof(BindablePasswordBox));

        public SecureString Password
        {
            get { return (SecureString)GetValue(PasswordPropety); }
            set { SetValue(PasswordPropety, value); }
        }
        private void OnPasswordChanged(
            object sender, EventArgs e)
        {
            Password = txtPassword.SecurePassword;
        }
    }
}
