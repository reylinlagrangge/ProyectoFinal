using ProyectoFinalFarmaciaApp.Data;
using ProyectoFinalFarmaciaApp.Services;
using ProyectoFinalFarmaciaApp.Reports;

try
{
    var dataContext = new DataContext();
    var pharmacyService = new PharmacyService(dataContext);

    int typeOption;
    bool exitApp = false;

    Console.WriteLine("Welcome to the Pharmacy App!");

    while (!exitApp)
    {
        Console.WriteLine("Please type the option you want to do:");
        Console.WriteLine("1. Add Laboratory");
        Console.WriteLine("2. Add Medication");
        Console.WriteLine("3. Add Batch");
        Console.WriteLine("4. View Medications");
        Console.WriteLine("5. View Batches");
        Console.WriteLine("6. Update Stock");
        Console.WriteLine("7. Check Expired Batches");
        Console.WriteLine("8. Exit");

        int.TryParse(Console.ReadLine(), out typeOption);

        switch (typeOption)
        {
            case 1:
                Console.Write("Enter laboratory name: ");
                string labName = Console.ReadLine();
                Console.Write("Enter country: ");
                string labCountry = Console.ReadLine();

                bool laboratoryAdded = pharmacyService.AddLaboratory(labName, labCountry);
                Console.WriteLine(laboratoryAdded ? "Laboratory added successfully" : "Invalid laboratory data");
                break;

            case 2:
                var laboratories = pharmacyService.GetLaboratories();

                if (laboratories.Count == 0)
                {
                    Console.WriteLine("No laboratories available");
                    break;
                }

                foreach (var lab in laboratories)
                {
                    Console.WriteLine($"{lab.Id}    {lab.Name}    {lab.Country}");
                }

                Console.Write("Enter laboratory id: ");
                int laboratoryId;
                int.TryParse(Console.ReadLine(), out laboratoryId);

                Console.Write("Enter medication name: ");
                string medName = Console.ReadLine();
                Console.Write("Enter price: ");
                decimal medPrice;
                decimal.TryParse(Console.ReadLine(), out medPrice);

                bool medicationAdded = pharmacyService.AddMedication(medName, medPrice, laboratoryId);
                Console.WriteLine(medicationAdded ? "Medication added successfully" : "Invalid medication data");
                break;

            case 3:
                var medications = pharmacyService.GetMedications();

                if (medications.Count == 0)
                {
                    Console.WriteLine("No medications available");
                    break;
                }

                foreach (var med in medications)
                {
                    Console.WriteLine($"{med.Id}    {med.Name}");
                }

                Console.Write("Enter medication id: ");
                int medicationId;
                int.TryParse(Console.ReadLine(), out medicationId);

                Console.Write("Enter batch number: ");
                string batchNumber = Console.ReadLine();

                Console.Write("Enter quantity: ");
                int quantity;
                int.TryParse(Console.ReadLine(), out quantity);

                Console.Write("Enter expiration date (yyyy-MM-dd): ");
                string expirationDateText = Console.ReadLine();

                bool batchAdded = pharmacyService.AddBatch(batchNumber, medicationId, quantity, expirationDateText);
                Console.WriteLine(batchAdded ? "Batch added successfully" : "Invalid batch data");
                break;

            case 4:
                var medicationReport = new MedicationReport(pharmacyService.GetMedications());
                medicationReport.PrintReport();
                break;

            case 5:
                foreach (var batch in pharmacyService.GetBatches())
                {
                    Console.WriteLine($"{batch.Id}    {batch.BatchNumber}    {batch.Medication.Name}    {batch.Quantity}    {batch.ExpirationDate.ToShortDateString()}");
                }
                break;

            case 6:
                var batches = pharmacyService.GetBatches();

                if (batches.Count == 0)
                {
                    Console.WriteLine("No batches available");
                    break;
                }

                foreach (var batch in batches)
                {
                    Console.WriteLine($"{batch.Id}    {batch.BatchNumber}    {batch.Medication.Name}    {batch.Quantity}");
                }

                Console.Write("Enter batch id: ");
                int batchId;
                int.TryParse(Console.ReadLine(), out batchId);

                Console.Write("Enter new quantity: ");
                int newQuantity;
                int.TryParse(Console.ReadLine(), out newQuantity);

                bool stockUpdated = pharmacyService.UpdateStock(batchId, newQuantity);
                Console.WriteLine(stockUpdated ? "Stock updated successfully" : "Invalid batch id or quantity");
                break;

            case 7:
                var expirationReport = new ExpirationReport(pharmacyService.GetBatches(), 30);
                expirationReport.PrintReport();
                break;

            case 8:
                exitApp = true;
                break;

            default:
                Console.WriteLine("Invalid option");
                break;
        }
    }

    Console.ReadKey();
}
catch (Exception)
{
    Console.WriteLine("An error occurred");
}