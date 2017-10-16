namespace SampQueryService.QueryResult
{
    public abstract class SampQueryResult
    {
        public char OpCode { get; internal set; }
        public bool IsCompleted { get; internal set; }

        internal abstract void Deserialize(byte[] data);
    }
}
