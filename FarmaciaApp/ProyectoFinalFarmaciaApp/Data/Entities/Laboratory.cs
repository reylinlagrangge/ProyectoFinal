namespace ProyectoFinalFarmaciaApp.Data.Entities
{
    public class Laboratory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public ICollection<Medication> Medications { get; set; }
    }
}