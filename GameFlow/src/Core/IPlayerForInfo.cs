namespace GameFlow.Core
{
    public interface IPlayerForInfo
    {
        PlayerID ID { get; }

        string Name { get; set; }

        bool Exist { get; set; }

        bool Ready { get; set; }
    }
}
