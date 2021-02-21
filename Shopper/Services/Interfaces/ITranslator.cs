namespace Shopper.Services.Interfaces
{
    public interface ITranslator
    {
        public string this[string key] { get; }
    }
}
