using System.Collections.ObjectModel;

namespace Buckets.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public ObservableCollection<ContainerViewModel> Containers { get; } = new();
    }
}
