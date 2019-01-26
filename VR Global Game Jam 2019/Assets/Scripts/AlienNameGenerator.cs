public static class AlienNameGenerator
{
    private static readonly string[] Start =
    {
        "Aaa", "Aby", "Aca", "Alk", "Ara",
        "Axo", "Bab", "Bar", "Bor", "Bru",
        "Byn", "Cal", "Cel", "Cha", "Col",
        "Cy", "Dae", "Dee", "Dra", "Dys",
        "El-", "Exo", "Ew", "Fer", "For",
        "Gal", "G'k", "Gor", "Hal", "Hoo",
        "Hyk", "Ico", "Ish", "Jen", "J'n",
        "Kal", "Kiv", "K't", "Lax", "Log",
        "Ly-", "Mac", "Mar", "Mel", "Mug",
        "Naa", "Neb", "Nih", "Ogr", "Osi",
        "Paa", "Pen", "Pri", "Pyr", "Qua",
        "Rea", "Rob", "Sak", "Sco", "Shi",
        "Sir", "S'p", "Sul", "Tal", "Ter",
        "Thr", "Tra", "Tus", "Ul-", "Uni",
        "Vaa", "Vin", "Vyr", "Wal", "Xan",
        "Xen", "Yeh", "Yuu", "Zeb", "Zoq"
    };

    private static readonly string[] Middle =
    {
        "maz", "orm", "dro", "rit", "som",
        "dal", "tar", "ach", "uri", "ard",
        "lib", "zar", "lon", "noc", "'n",
        "led", "gon", "tur", "thr", "ton",
        string.Empty
    };

    private static readonly string[] End =
    {
        "ite", "of", "ent", "an", "ish",
        "on", "ite", "ot", "yn", "er",
        string.Empty
    };

    public static string Create(int seed)
    {
        // Build random number generator for name
        var rand = new Generator(seed);

        // Select indexes
        var iStart = (int)(rand.NextUint() % Start.Length);
        var iMiddle = (int)(rand.NextUint() % Middle.Length);
        var iEnd = (int)(rand.NextUint() % End.Length);

        // Combine name
        return Start[iStart] + Middle[iMiddle] + End[iEnd];
    }
}
