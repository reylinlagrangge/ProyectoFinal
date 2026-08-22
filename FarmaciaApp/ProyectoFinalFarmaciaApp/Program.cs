using ProyectoFinalFarmaciaApp.Data;
using ProyectoFinalFarmaciaApp.Data.Entities;
using Microsoft.EntityFrameworkCore;

try
{
    var dataContext = new DataContext();
    List<Laboratory> laboratories = new List<Laboratory>();
    laboratories = dataContext.Laboratories.ToList();
    List<Medication> medications = new List<Medication>();
    medications = dataContext.Medications.Include(m => m.Laboratory).ToList();
    List<Batch> batches = new List<Batch>();
    batches = dataContext.Batches.Include(b => b.Medication).ToList();

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

                if (string.IsNullOrWhiteSpace(labName))
                {
                    Console.WriteLine("Laboratory name cannot be empty");
                    break;
                }

                Console.Write("Enter country: ");
                string labCountry = Console.ReadLine();

                Laboratory newLaboratory = new Laboratory { Name = labName, Country = labCountry };
                dataContext.Laboratories.Add(newLaboratory);
                dataContext.SaveChanges();
                laboratories.Add(newLaboratory);
                Console.WriteLine("Laboratory added successfully");
                break;

            case 2:
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
                bool validLabId = int.TryParse(Console.ReadLine(), out laboratoryId);

                if (!validLabId || !laboratories.Any(l => l.Id == laboratoryId))
                {
                    Console.WriteLine("Invalid laboratory id");
                    break;
                }

                Console.Write("Enter medication name: ");
                string medName = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(medName))
                {
                    Console.WriteLine("Medication name cannot be empty");
                    break;
                }

                Console.Write("Enter price: ");
                decimal medPrice;
                bool validPrice = decimal.TryParse(Console.ReadLine(), out medPrice);

                if (!validPrice || medPrice < 0)
                {
                    Console.WriteLine("Invalid price");
                    break;
                }

                Medication newMedication = new Medication { Name = medName, Price = medPrice, LaboratoryId = laboratoryId };
                dataContext.Medications.Add(newMedication);
                dataContext.SaveChanges();
                medications.Add(newMedication);
                Console.WriteLine("Medication added successfully");
                break;

            case 3:
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
                bool validMedId = int.TryParse(Console.ReadLine(), out medicationId);

                if (!validMedId || !medications.Any(m => m.Id == medicationId))
                {
                    Console.WriteLine("Invalid medication id");
                    break;
                }

                Console.Write("Enter batch number: ");
                string batchNumber = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(batchNumber))
                {
                    Console.WriteLine("Batch number cannot be empty");
                    break;
                }

                Console.Write("Enter quantity: ");
                int quantity;
                bool validQuantity = int.TryParse(Console.ReadLine(), out quantity);

                if (!validQuantity || quantity < 0)
                {
                    Console.WriteLine("Invalid quantity");
                    break;
                }

                Console.Write("Enter expiration date (yyyy-MM-dd): ");
                DateTime expirationDate;
                bool validDate = DateTime.TryParse(Console.ReadLine(), out expirationDate);

                if (!validDate)
                {
                    Console.WriteLine("Invalid date format");
                    break;
                }

                Batch newBatch = new Batch { BatchNumber = batchNumber, MedicationId = medicationId, Quantity = quantity, ExpirationDate = expirationDate };
                dataContext.Batches.Add(newBatch);
                dataContext.SaveChanges();
                batches.Add(newBatch);
                Console.WriteLine("Batch added successfully");
                break;

            case 4:
                foreach (var med in medications)
                {
                    Console.WriteLine($"{med.Id}    {med.Name}    {med.Laboratory.Name}    {med.Price}");
                }
                break;

            case 5:
                foreach (var batch in batches)
                {
                    Console.WriteLine($"{batch.Id}    {batch.BatchNumber}    {batch.Medication.Name}    {batch.Quantity}    {batch.ExpirationDate.ToShortDateString()}");
                }
                break;

            case 6:
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
                bool validBatchId = int.TryParse(Console.ReadLine(), out batchId);
                Batch batchToUpdate = batches.FirstOrDefault(b => b.Id == batchId);

                if (!validBatchId || batchToUpdate == null)
                {
                    Console.WriteLine("Invalid batch id");
                    break;
                }

                Console.Write("Enter new quantity: ");
                int newQuantity;
                bool validNewQuantity = int.TryParse(Console.ReadLine(), out newQuantity);

                if (!validNewQuantity || newQuantity < 0)
                {
                    Console.WriteLine("Invalid quantity");
                    break;
                }

                batchToUpdate.Quantity = newQuantity;
                dataContext.SaveChanges();
                Console.WriteLine("Stock updated successfully");
                break;

            case 7:
                Console.WriteLine("Expired batches:");
                foreach (var batch in batches.Where(b => b.ExpirationDate < DateTime.Now))
                {
                    Console.WriteLine($"{batch.Id}    {batch.BatchNumber}    {batch.Medication.Name}    {batch.ExpirationDate.ToShortDateString()}    EXPIRED");
                }

                Console.WriteLine("Batches close to expiration (next 30 days):");
                foreach (var batch in batches.Where(b => b.ExpirationDate >= DateTime.Now && b.ExpirationDate <= DateTime.Now.AddDays(30)))
                {
                    Console.WriteLine($"{batch.Id}    {batch.BatchNumber}    {batch.Medication.Name}    {batch.ExpirationDate.ToShortDateString()}    CLOSE TO EXPIRATION");
                }
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