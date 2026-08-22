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
                Console.Write("Enter country: ");
                string labCountry = Console.ReadLine();

                Laboratory newLaboratory = new Laboratory { Name = labName, Country = labCountry };
                dataContext.Laboratories.Add(newLaboratory);
                dataContext.SaveChanges();
                laboratories.Add(newLaboratory);
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
                int laboratoryId = Convert.ToInt32(Console.ReadLine());

                Console.Write("Enter medication name: ");
                string medName = Console.ReadLine();
                Console.Write("Enter price: ");
                decimal medPrice = Convert.ToDecimal(Console.ReadLine());

                Medication newMedication = new Medication { Name = medName, Price = medPrice, LaboratoryId = laboratoryId };
                dataContext.Medications.Add(newMedication);
                dataContext.SaveChanges();
                medications.Add(newMedication);
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
                int medicationId = Convert.ToInt32(Console.ReadLine());

                Console.Write("Enter quantity: ");
                int quantity = Convert.ToInt32(Console.ReadLine());
                Console.Write("Enter expiration date (yyyy-MM-dd): ");
                DateTime expirationDate = Convert.ToDateTime(Console.ReadLine());

                Batch newBatch = new Batch { MedicationId = medicationId, Quantity = quantity, ExpirationDate = expirationDate };
                dataContext.Batches.Add(newBatch);
                dataContext.SaveChanges();
                batches.Add(newBatch);
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
                    Console.WriteLine($"{batch.Id}    {batch.Medication.Name}    {batch.Quantity}    {batch.ExpirationDate.ToShortDateString()}");
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
                    Console.WriteLine($"{batch.Id}    {batch.Medication.Name}    {batch.Quantity}");
                }

                Console.Write("Enter batch id: ");
                int batchId = Convert.ToInt32(Console.ReadLine());
                Batch batchToUpdate = dataContext.Batches.Find(batchId);

                Console.Write("Enter new quantity: ");
                batchToUpdate.Quantity = Convert.ToInt32(Console.ReadLine());
                dataContext.SaveChanges();
                break;

            case 7:
                foreach (var batch in batches.Where(b => b.ExpirationDate < DateTime.Now))
                {
                    Console.WriteLine($"{batch.Id}    {batch.Medication.Name}    {batch.ExpirationDate.ToShortDateString()}");
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