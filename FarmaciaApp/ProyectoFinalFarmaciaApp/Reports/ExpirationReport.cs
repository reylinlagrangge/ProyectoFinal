using ProyectoFinalFarmaciaApp.Data.Entities;

namespace ProyectoFinalFarmaciaApp.Reports
{
    public class ExpirationReport : PharmacyReport
    {
        private List<Batch> batches;
        private int daysThreshold;

        public ExpirationReport(List<Batch> batches, int daysThreshold)
        {
            this.batches = batches;
            this.daysThreshold = daysThreshold;
        }

        public override string GenerateReport()
        {
            string result = "Expired batches:\n";
            foreach (var batch in batches.Where(b => b.ExpirationDate < DateTime.Now))
            {
                result += $"{batch.Id}    {batch.BatchNumber}    {batch.Medication.Name}    {batch.ExpirationDate.ToShortDateString()}    EXPIRED\n";
            }

            result += $"Batches close to expiration (next {daysThreshold} days):\n";
            foreach (var batch in batches.Where(b => b.ExpirationDate >= DateTime.Now && b.ExpirationDate <= DateTime.Now.AddDays(daysThreshold)))
            {
                result += $"{batch.Id}    {batch.BatchNumber}    {batch.Medication.Name}    {batch.ExpirationDate.ToShortDateString()}    CLOSE TO EXPIRATION\n";
            }
            return result;
        }
    }
}