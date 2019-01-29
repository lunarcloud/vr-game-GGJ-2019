public class SocialModifierFamily : IProbability
{
    public static readonly SocialModifierFamily None = new SocialModifierFamily
    {
        Name = "None",
        Probability = 1f
    };

    public static readonly SocialModifierFamily NotDiscussed = new SocialModifierFamily
    {
        Name = "Not Discussed",
        Probability = 1f
    };

    public static readonly SocialModifierFamily OfferAcceptVisit = new SocialModifierFamily
    {
        Name = "Offer and Accept Visit",
        Probability = 1f
    };

    public static readonly SocialModifierFamily OfferRefuseVisit = new SocialModifierFamily
    {
        Name = "Offer and Refuse Visit",
        Probability = 1f
    };

    public static readonly SocialModifierFamily[] Types =
    {
        None,
        NotDiscussed,
        OfferAcceptVisit,
        OfferRefuseVisit
    };

    public float Probability { get; private set; }
    public string Name { get; private set; }
    public override string ToString() => Name;
}