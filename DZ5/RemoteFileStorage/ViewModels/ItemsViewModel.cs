using Azure.Storage.Blobs.Models;
using RemoteFileStorage.Dao;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RemoteFileStorage.ViewModels
{
    class ItemsViewModel
    {
        public const string ForwardSlash = "/";
        private string directory;

        public ObservableCollection<BlobItem> Items { get; set; }
        public ObservableCollection<string> Directories { get; set; }

        public ItemsViewModel()
        {
            Items = new ObservableCollection<BlobItem>();
            Directories = new ObservableCollection<string>();
            Refresh();
        }

        public string Directory 
        { 
            get => directory; 
            set 
            { 
                directory = value; 
                Refresh(); 
            } 
        }

        private void Refresh()
        {
            Directories.Clear();
            Items.Clear();
            Repository.Container.GetBlobs().ToList().ForEach(item =>
            {
                if (item.Name.Contains(ForwardSlash))
                {
                    string directory = item.Name.Substring(0, item.Name.LastIndexOf(ForwardSlash));
                    if (!Directories.Contains(directory))
                    {
                        Directories.Add(directory);
                    }
                }
                if (string.IsNullOrEmpty(Directory) && !item.Name.Contains(ForwardSlash))
                {
                    Items.Add(item);
                }
                else if (!string.IsNullOrEmpty(Directory) && item.Name.Contains($"{Directory}{ForwardSlash}"))
                {
                    Items.Add(item);
                }
            });
        }

        public async Task DeleteAsync(BlobItem blobItem) 
        {
            await Repository.Container.GetBlobClient(blobItem.Name).DeleteAsync();
            Refresh();
        }

        public async Task DownloadAsync(BlobItem blobItem, string fileName)
        {
            using (var fs = File.OpenWrite(fileName))
            {
                await Repository.Container.GetBlobClient(blobItem.Name).DownloadToAsync(fs);
            }
            Refresh();
        }

        public async Task UploadAsync(string path, string dir)
        {
            // c:\temp\milica\icon.png
            string fileName = path.Substring(path.LastIndexOf(Path.DirectorySeparatorChar) + 1);
            string ext = fileName.Substring(fileName.LastIndexOf(".") + 1);
            fileName = $"{ext}{ForwardSlash}{fileName}";

            using (var fs = File.OpenRead(path))
            {
                await Repository.Container.GetBlobClient(fileName).UploadAsync(fs, true);
            }
            Directory = ext;
        }
    }
}
