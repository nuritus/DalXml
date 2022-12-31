namespace DO;

public static class Tools
{
    public static string ToStringProperty<T>(this T t) =>
        typeof(T).GetProperties().Aggregate("", (str, item) => str += "\n" + item.Name + ": " + item.GetValue(t, null));
}