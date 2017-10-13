namespace AlisBatchReporter.Presentors
{
    internal class ArcvalFactory
    {
        public static ArcvalInstance GetArcvalInstance(string arcvalRow)
        {
            ArcvalInstance arcval = new ArcvalInstance(arcvalRow);
            arcval.SetKeyAndType();
            return arcval;
        }
    }
}