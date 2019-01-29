public class SocialModifierBantering : IProbability
{
    public static readonly SocialModifierBantering None = new SocialModifierBantering
    {
        Name = "None",
        Probability = 1f
    };

    public static readonly SocialModifierBantering JokesAppreciated = new SocialModifierBantering
    {
        Name = "Jokes Appreciated",
        Probability = 1f
    };

    public static readonly SocialModifierBantering JokesInsulting = new SocialModifierBantering
    {
        Name = "Jokes Insulting",
        Probability = 1f
    };

    public static readonly SocialModifierBantering InsultsAppreciated = new SocialModifierBantering
    {
        Name = "Insults Appreciated",
        Probability = 1f
    };

    public static readonly SocialModifierBantering InsultsInsulting = new SocialModifierBantering
    {
        Name = "Insults Insulting",
        Probability = 1f
    };


    public static readonly SocialModifierBantering[] Types =
    {
        None,
        JokesAppreciated,
        JokesInsulting,
        InsultsAppreciated,
        InsultsInsulting
    };

    public float Probability { get; private set; }
    public string Name { get; private set; }
    public override string ToString() => Name;
}