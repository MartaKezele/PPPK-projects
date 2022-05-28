using Azure.Storage.Blobs.Models;
using Microsoft.Win32;
using RemoteFileStorage.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace RemoteFileStorage
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private readonly ItemsViewModel itemsViewModel;

        public MainWindow()
        {
            InitializeComponent();
            itemsViewModel = new ItemsViewModel();
            Init();
        }

        private void Init()
        {
            cbDirectories.ItemsSource = itemsViewModel.Directories;
            lbItems.ItemsSource = itemsViewModel.Items;
        }

        private void CbDirectories_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                itemsViewModel.Directory = cbDirectories.Text;
            }
        }

        private void CbDirectories_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (itemsViewModel.Directories.Contains(cbDirectories.Text))
            {
                itemsViewModel.Directory = cbDirectories.Text;
                cbDirectories.SelectedItem = itemsViewModel.Directory;
            }
        }

        private void LbItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataContext = lbItems.SelectedItem as BlobItem;
        }

        private async void BtnUpload_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                await itemsViewModel.UploadAsync(openFileDialog.FileName, cbDirectories.Text);
            }

            cbDirectories.Text = itemsViewModel.Directory;
        }

        private async void BtnDownload_Click(object sender, RoutedEventArgs e)
        {
            if (!(lbItems.SelectedItem is BlobItem blobItem))
            {
                return;
            }
            var saveFileDialog = new SaveFileDialog {
                FileName = blobItem.Name.Contains(ItemsViewModel.ForwardSlash) ? 
                blobItem.Name.Substring(blobItem.Name.LastIndexOf(ItemsViewModel.ForwardSlash) + 1) 
                : blobItem.Name
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                await itemsViewModel.DownloadAsync(blobItem, saveFileDialog.FileName);
            }
            cbDirectories.Text = itemsViewModel.Directory;
        }

        private async void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (!(lbItems.SelectedItem is BlobItem blobItem))
            {
                return;
            }
            await itemsViewModel.DeleteAsync(blobItem);
            cbDirectories.Text = itemsViewModel.Directory;
        }
    }
}
