namespace Graph;

/// <summary>
/// Represents a weighted edge in a graph between two vertices.
/// </summary>
public record class Edge(int From, int To, int Weight)
{
}
