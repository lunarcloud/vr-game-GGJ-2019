public class VendorModelType : IProbability
{
    public static VendorModelType BlueGirl = new VendorModelType {Probability = 1f, Name = "BlueGirl"};
    public static VendorModelType BikerElf = new VendorModelType { Probability = 1f, Name = "BikerElf" };
    public static VendorModelType BlackLady = new VendorModelType { Probability = 1f, Name = "BlackLady" };
    public static VendorModelType LizardMan = new VendorModelType { Probability = 1f, Name = "LizardMan" };

    public static VendorModelType[] Types =
    {
        BlueGirl,
        BikerElf,
        BlackLady,
        LizardMan
    };

    public float Probability { get; private set; }
    public string Name { get; private set; }
}
