using System.Collections.Generic;

namespace PizzaProject1.Library
{
    public interface IPizzaRepository
    {
        IEnumerable<LibUser> GetAllUsersOnly();
        LibUser GetUserByIdWithLocation(int id);
        IEnumerable<LibUser> GetAllUsersWithLocation();
        IEnumerable<LibOrder> GetAllOrdersWithUserAndLocation();
        IEnumerable<LibLocation> GetAllLocationsOnly();
        IEnumerable<LibOrderEntry> GetAllOrderEntriesForOrder(int id);
        IEnumerable<LibOrderEntry> GetAllOrderEntriesForOrderWithUser(int id);
        IEnumerable<LibLocationInventory> GetLocationInventory(int id);
        IEnumerable<LibOrderEntry> GetAllOrderEntriesForOrderWithLocation(int id);
        int CreateOrder(int userId, int locationId);
        void SetLocation(int userId, int locationId);
        LibOrder GetOrderByIdWithEntries(int id);
        IEnumerable<LibOrderEntry> GetEntriesForOrder(int id);
        IEnumerable<LibPizza> GetAllPizzas();
        int AddOrderEntryGetIdBack(int orderId, int pizzaId, int quantity);
        bool OrderIsValidPrice(decimal price);
        bool OrderIsValidSize(int size);
        void RemoveOrderEntry(int entryId);
        void RemoveOrder(int orderId);
        IEnumerable<LibOrderEntry> GetOrderEntriesForDeletion(int orderId);
        bool EnoughIngredients(LibOrder order);
        void UseIngredients(LibOrder order);
    }
}