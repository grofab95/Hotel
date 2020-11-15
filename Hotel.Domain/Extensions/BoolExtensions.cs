namespace Hotel.Domain.Extensions
{
    public static class BoolExtensions
    {
        public static string GetName(this bool value) => value ? "Tak" : "Nie";
    }
}
