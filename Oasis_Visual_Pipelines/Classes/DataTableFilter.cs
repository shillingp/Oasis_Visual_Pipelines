using PropertyChanged;

namespace Oasis_Visual_Pipelines.Classes
{
    [AddINotifyPropertyChangedInterface]
    public sealed class DataTableFilter
    {
        public KeyValuePair<string?, Type> Column { get; set; }

        public FilterFunctor Filter { get; set; }

        public string Value { get; set; } = string.Empty;

        public DataTableFilter() { }

        public override string ToString()
        {
            if (Column.Key is null || Filter.Functor is null) return "";

            string filterFunctor = Filter.Functor.Contains("___REPLACE___")
                ? Filter.Functor.Replace("___REPLACE___", Value)
                : Filter.Functor + " " + Value;

            return "[" + Column.Key + "] " + filterFunctor;
        }
    }

    public record struct FilterFunctor(string Title, string? Functor);
}
