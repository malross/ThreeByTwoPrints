using ReactiveUI;
using System;
using System.Reactive.Linq;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ThreeByTwoPrints
{
    public class PhotoViewModel : ReactiveObject
    {
        private Lazy<BitmapImage> image;
        private readonly string filePath;
        private int copies;
        private readonly ReactiveCommand<object> addCopyCommand;
        private readonly ReactiveCommand<object> removeCopyCommand;

        public PhotoViewModel(string filePath)
        {
            this.filePath = filePath;
            copies = 1;

            image = new Lazy<BitmapImage>(() => new BitmapImage(new Uri(filePath)));

            addCopyCommand = ReactiveCommand.Create();
            addCopyCommand.Subscribe(_ => Copies = Copies + 1);
            removeCopyCommand = ReactiveCommand.Create();
            removeCopyCommand.Subscribe(_ => Copies = Math.Max(1, Copies - 1));
        }

        public string FilePath { get { return filePath; } }

        public ImageSource Image { get { return image.Value; } }

        public int Copies
        {
            get { return copies; }
            set { this.RaiseAndSetIfChanged(ref copies, value); }
        }

        public ICommand AddCopyCommand { get { return addCopyCommand; } }
        public ICommand RemoveCopyCommand { get { return removeCopyCommand; } }
    }
}
