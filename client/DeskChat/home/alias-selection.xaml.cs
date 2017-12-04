using DeskChat.app;
using DeskChat.models;
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
using static DeskChat.MainWindow;

namespace DeskChat.home
{
    /// <summary>
    /// Interaction logic for alias_selection.xaml
    /// </summary>
    public partial class AliasSelector : Page
    {
        public event UserChanged changedUser;
        public AliasSelector()
        {
            InitializeComponent();
        }

        private void okSelectAlias(object sender, RoutedEventArgs e)
        {
            User.getInstance().Alias = nameSelector.Text;
            btnOk.Visibility = Visibility.Hidden;
            changedUser(this, e);
        }
    }
}
