namespace CompanyMvc.Dox.PL.Services
{
    public interface IScopedService
    {
        public Guid Guid { get; set; }
       string GetGuid();
    }
}
