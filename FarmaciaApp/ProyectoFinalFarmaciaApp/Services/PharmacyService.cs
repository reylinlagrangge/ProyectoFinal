using ProyectoFinalFarmaciaApp.Data;
using ProyectoFinalFarmaciaApp.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace ProyectoFinalFarmaciaApp.Services
{
    public class PharmacyService
    {
        private DataContext context;

        public PharmacyService(DataContext context)
        {
            this.context = context;
        }

        public List<Laboratory> GetLaboratories()
        {
            return context.Laboratories.ToList();
        }

        public List<Medication> GetMedications()
        {
            return context.Medications.Include(m => m.Laboratory).ToList();
        }

        public List<Batch> GetBatches()
        {
            return context.Batches.Include(b => b.Medication).ToList();
        }

        public bool AddLaboratory(string name, string country)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return false;
            }

            Laboratory laboratory = new Laboratory(name, country);
            context.Laboratories.Add(laboratory);
            context.SaveChanges();
            return true;
        }

        public bool AddMedication(string name, decimal price, int laboratoryId)
        {
            if (string.IsNullOrWhiteSpace(name) || price < 0)
            {
                return false;
            }

            bool laboratoryExists = context.Laboratories.Any(l => l.Id == laboratoryId);

            if (!laboratoryExists)
            {
                return false;
            }

            Medication medication = new Medication(name, price, laboratoryId);
            context.Medications.Add(medication);
            context.SaveChanges();
            return true;
        }

        public bool AddBatch(string batchNumber, int medicationId, int quantity, DateTime expirationDate)
        {
            if (string.IsNullOrWhiteSpace(batchNumber) || quantity < 0)
            {
                return false;
            }

            bool medicationExists = context.Medications.Any(m => m.Id == medicationId);

            if (!medicationExists)
            {
                return false;
            }

            Batch batch = new Batch(batchNumber, medicationId, quantity, expirationDate);
            context.Batches.Add(batch);
            context.SaveChanges();
            return true;
        }

        public bool AddBatch(string batchNumber, int medicationId, int quantity, string expirationDateText)
        {
            DateTime expirationDate;
            bool validDate = DateTime.TryParse(expirationDateText, out expirationDate);

            if (!validDate)
            {
                return false;
            }

            return AddBatch(batchNumber, medicationId, quantity, expirationDate);
        }

        public bool UpdateStock(int batchId, int newQuantity)
        {
            if (newQuantity < 0)
            {
                return false;
            }

            Batch batch = context.Batches.Find(batchId);

            if (batch == null)
            {
                return false;
            }

            batch.Quantity = newQuantity;
            context.SaveChanges();
            return true;
        }
    }
}