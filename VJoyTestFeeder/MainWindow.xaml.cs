using System.ComponentModel;
using VJoyTestFeeder.ViewModel;

namespace VJoyTestFeeder
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            var VM = DataContext as MainWindowViewModel;
            if (VM != null)
            {
                VM.OnClosing();
            }
        }
    }
}
