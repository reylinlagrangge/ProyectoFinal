namespace ProyectoFinalFarmaciaApp.Data.Entities
{
    public class Medication
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int LaboratoryId { get; set; }
        public Laboratory Laboratory { get; set; }
        public ICollection<Batch> Batches { get; set; }

        public Medication()
        {
        }

        public Medication(string name, decimal price, int laboratoryId)
        {
            Name = name;
            Price = price;
            LaboratoryId = laboratoryId;
        }
    }
}