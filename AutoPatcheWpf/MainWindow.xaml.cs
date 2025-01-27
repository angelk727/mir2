using AutoPatcheWpf.VM;
using System.IO;

namespace AutoPatcheWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new MainWindowViewModel();
        }


        private void DataGrid_DragEnter(object sender, System.Windows.DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effects = System.Windows.DragDropEffects.Copy;
            }
            else
            {
                e.Effects = System.Windows.DragDropEffects.None;
            }
        }

        private void DataGrid_Drop(object sender, System.Windows.DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(System.Windows.DataFormats.FileDrop);
                var viewModel = (MainWindowViewModel)DataContext;
                foreach (string file in files)
                {
                    if (Directory.Exists(file))
                    {
                        viewModel.AddFilesFromDirectory(file);
                    }
                    else
                    {
                        viewModel.AddFileToDataGridView(file);
                    }
                }
            }
        }
    }
}