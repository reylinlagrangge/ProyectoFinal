using ProyectoFinalFarmaciaApp.Data.Entities;

namespace ProyectoFinalFarmaciaApp.Reports
{
    public class MedicationReport : PharmacyReport
    {
        private List<Medication> medications;

        public MedicationReport(List<Medication> medications)
        {
            this.medications = medications;
        }

        public override string GenerateReport()
        {
            string result = "Id   Name   Laboratory   Price\n";
            result += "-----------------------------------------\n";
            foreach (var medication in medications)
            {
                result += $"{medication.Id}    {medication.Name}    {medication.Laboratory.Name}    {medication.Price}\n";
            }
            return result;
        }
    }
}