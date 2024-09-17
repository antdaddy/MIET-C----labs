namespace lr3
{
    interface IRateAndCopy
    {
        double Score
        {
            get;
        }
        object DeepCopy();
    }
}
