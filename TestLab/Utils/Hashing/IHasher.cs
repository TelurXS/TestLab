namespace TestLab.Utils.Hashing
{
    public interface IHasher
    {
        public string Hash(string value);

        public bool IsHashSupported(string value);

        public bool Verify(string value, string hashed);
    }
}
