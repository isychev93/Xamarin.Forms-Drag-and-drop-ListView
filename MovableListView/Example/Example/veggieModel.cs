using MovableListView;

namespace Example
{
    public class VeggieModel
    {
        public string Name { get; set; }
        public string Comment { get; set; }
        public bool IsReallyAVeggie { get; set; }
        public string Image { get; set; }
        public VeggieModel()
        {
        }
    }

    public class GroupedVeggieModel : ObservableCollectionEx<VeggieModel>
    {
        public string LongName { get; set; }
        public string ShortName { get; set; }
    }
}

