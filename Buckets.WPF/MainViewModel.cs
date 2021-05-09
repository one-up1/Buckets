using Buckets.Model;
using System.Collections.ObjectModel;

namespace Buckets.WPF
{
    public class MainViewModel
    {
        public ObservableCollection<Container> Containers { get; } = new();
    }
}
