namespace ProyectoFinalFarmaciaApp.Reports
{
    public abstract class PharmacyReport
    {
        public abstract string GenerateReport();

        public void PrintReport()
        {
            Console.WriteLine(GenerateReport());
        }
    }
}