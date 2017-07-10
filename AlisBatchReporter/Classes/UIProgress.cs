namespace AlisBatchReporter.Classes
{
    class UiProgress
    {
        public UiProgress(string name, long bytes, long maxbytes)
        {
            Name = name; Bytes = bytes; Maxbytes = maxbytes;
        }
        public string Name;
        public long Bytes;
        public long Maxbytes;
    }
}
