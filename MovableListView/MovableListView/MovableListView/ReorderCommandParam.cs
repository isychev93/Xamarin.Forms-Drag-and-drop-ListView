namespace MovableListView
{
    public class ReorderCommandParam
    {
        private readonly int sourceRow;
        private readonly int sourceSection;

        private readonly int destinationRow;
        private readonly int destinationSection;

        public ReorderCommandParam(int sourceRow, int sourceSection, int destinationRow, int destinationSection)
        {
            this.sourceRow = sourceRow;
            this.sourceSection = sourceSection;
            this.destinationRow = destinationRow;
            this.destinationSection = destinationSection;
        }

        public int SourceRow
        {
            get { return sourceRow; }
        }

        public int SourceSection
        {
            get { return sourceSection; }
        }

        public int DestinationRow
        {
            get { return destinationRow; }
        }

        public int DestinationSection
        {
            get { return destinationSection; }
        }
    }
}
