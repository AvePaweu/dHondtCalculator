// See https://aka.ms/new-console-template for more information
internal class Komitet
{
    public string Nazwa { get; set; }
    public int LiczbaGlosow { get; set; }
}

internal class Komitet1 : Komitet
{
    public int Iteracja { get; set; }
    public int LiczbaGlosowIteracja { get; set; }
}