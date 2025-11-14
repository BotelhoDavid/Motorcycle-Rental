namespace Application
{
    public interface IRentAppService
    {
        public RentViewModel GetRent(Guid id);
        public void CreateRent();
        public void UpdateRent();
    }
}
