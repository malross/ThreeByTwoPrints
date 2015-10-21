using ReactiveUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace ThreeByTwoPrints
{
    public class PrintsViewModel : ReactiveObject
    {
        private readonly List<PhotoViewModel> photos = new List<PhotoViewModel>();
        private PhotoViewModel selectedPhoto;
        private readonly ICollectionView collectionView;
        private readonly ReactiveCommand<object> openPhotosCommand;

        public PrintsViewModel()
        {
            collectionView = CollectionViewSource.GetDefaultView(photos);

            openPhotosCommand = ReactiveCommand.Create();
            openPhotosCommand.Subscribe(_ => OpenPhotos());
        }

        private void OpenPhotos()
        {
            var dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.Filter = "Photos|*.jpg;*.jpeg|All Files|*.*";
            dialog.DefaultExt = ".jpg";
            dialog.Title = "Open Photos";
            dialog.Multiselect = true;

            if (dialog.ShowDialog() == true)
            {
                var newFilePaths = dialog.FileNames.Except(photos.Select(p => p.FilePath));
                photos.AddRange(newFilePaths.Select(p => new PhotoViewModel(p)));
                collectionView.Refresh();
            }
        }

        public ICollectionView Photos { get { return collectionView; } }

        public PhotoViewModel SelectedPhoto
        {
            get { return selectedPhoto; }
            set { this.RaiseAndSetIfChanged(ref selectedPhoto, value); }
        }

        public ICommand OpenPhotosCommand { get { return openPhotosCommand; } }
    }
}
