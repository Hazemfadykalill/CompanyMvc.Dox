namespace CompanyMvc.Dox.PL.Services
{
    public class SingleTonService:ISingleTonService
    {

        public Guid Guid { get; set; }
        public SingleTonService()
        {
            Guid = Guid.NewGuid();
        }
        public string GetGuid()
        {
            return Guid.ToString();
        }
    }
}
