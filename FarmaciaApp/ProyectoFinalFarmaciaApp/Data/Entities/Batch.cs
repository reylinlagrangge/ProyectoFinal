namespace ProyectoFinalFarmaciaApp.Data.Entities
{
    public class Batch
    {
        public int Id { get; set; }
        public string BatchNumber { get; set; }
        public int MedicationId { get; set; }
        public Medication Medication { get; set; }
        public int Quantity { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}