namespace EcomRevisited.Data
{
    public interface IDatabaseTransaction : IDisposable
    {
        void Commit();
        void Rollback();
    }

}
