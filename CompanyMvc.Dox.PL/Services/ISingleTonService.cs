namespace CompanyMvc.Dox.PL.Services
{
    public interface ISingleTonService
    {

        public Guid Guid { get; set; }
        string GetGuid();
    }
}
