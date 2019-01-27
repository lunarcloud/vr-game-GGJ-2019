public class SocialModifierReligion : IProbability
{
    public static readonly SocialModifierReligion None = new SocialModifierReligion
    {
        Name = "None",
        Probability = 1f
    };

    public static readonly SocialModifierReligion NotDiscussed = new SocialModifierReligion
    {
        Name = "Not Discussed",
        Probability = 1f
    };

    public static readonly SocialModifierReligion PraiseBeforeTrading = new SocialModifierReligion
    {
        Name = "Praise Before Trading",
        Probability = 1f
    };

    public static readonly SocialModifierReligion PraiseAfterTrading = new SocialModifierReligion
    {
        Name = "Praise After Trading",
        Probability = 1f
    };

    public static readonly SocialModifierReligion[] Types =
    {
        None,
        NotDiscussed,
        PraiseBeforeTrading,
        PraiseAfterTrading
    };

    public float Probability { get; private set; }
    public string Name { get; private set; }
    public override string ToString() => Name;
}