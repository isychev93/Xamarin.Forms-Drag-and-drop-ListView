using Xamarin.Forms;

namespace MovableListView
{
    /// <summary>
    /// ViewCell which can be reordered in ListView by drag and drop.
    /// There are 2 types of working with <see cref="MovableViewCell"/>:
    /// <list type="table">
    /// <item>
    /// <term>
    /// Internal reordering logics
    /// </term>
    /// <description>
    /// All already done for you.
    /// But for property <see cref="ListView.ItemsSource"/> you must set
    /// collection which implements <see cref="IObservableCollectionEx"/> (for example <see cref="ObservableCollectionEx"/>).
    /// </description>
    /// </item>
    /// <item>
    /// <term>
    /// Custom reordering logics
    /// </term>
    /// <description>
    /// Necessary when your reordering logic is not trivial. 
    /// In which case you must set <see cref="CustomReorderCommaond"/> 
    /// and change source list manually.
    /// </description>
    /// </item>
    /// </list>
    /// </summary>
    public class MovableViewCell : ViewCell
    {
        /// <summary>
        /// Command for custom reordering.
        /// Called when touch point within a new row.
        /// </summary>
        /// <remarks>
        /// If not null, all internal reordering logics will be ignored.
        /// </remarks>
        public Command<ReorderCommandParam> CustomReorderCommaond { get; set; }
    }
}
