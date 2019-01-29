using System;

public static class CaptainNameGenerator
{
    public static readonly string[] First =
    {
        "Aaron", "Adam", "Al", "Alia", "Alice", "Astrid",
        "Bennett", "Bridget", "Bob", "Bruce",
        "Cal", "Cassie", "Cole", "Colleen", "Curtis",
        "Dack", "Daniel", "Dexter", "Deela", "Diana", 
        "Ed", "Elena", "Elizabeth", "Elric", "Ethan", "Eve",
        "Farrah", "Felix", "Flash",
        "Garth", "Geela", "Gordon", "Grace", "Greg", "Gwen",
        "Hal", "Hart", "Helen", "Hilda", "Hugh",
        "Jack", "Jane", "James", "Jill", "Jonathan", "Julia",
        "Kara", "Karl", "Katie", "Kent", "Kurt",
        "Lars", "Linda", "Logan", "Louis", "Lydia",
        "Madeline", "Marcus", "Maximillian", "May", "Morin", 
        "Natasha", "Nathan", "Norton", "Nova",
        "Peter", "Petra", "Phil",
        "Quinn",
        "Sam", "Sara", "Simon", "Stan", "Sue",
        "Tom", "Tessa"
    };

    public static readonly string[] Last =
    {
        "Baxton", "Beckett", "Benson", "Clements", "Connor",
        "Conrad", "Graham", "Hammond", "Hudson", "Idaho",
        "Jackson", "Jones", "Kleinman", "Knox", "Markus",
        "Sandoval", "Santiago", "Shephard", "Stone",
        "Sutter", "Wolsey"
    };

    public static string Create(int seed)
    {
        // Build random number generator for name
        var rand = new Generator(seed);

        // Select indexes
        var iFirst = (int)(rand.NextUint() % First.Length);
        var iLast = (int)(rand.NextUint() % Last.Length);

        // Combine name
        return First[iFirst] + " " + Last[iLast];
    }

    public static string Create()
    {
        var rand = new Random();
        return Create(rand.Next());
    }
}