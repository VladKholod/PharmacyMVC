using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using Pharmacy.Core;

namespace Pharmacy.Data.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<DataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(DataContext context)
        {
            #region Pharmacies

            var pharmacies = new List<Core.Pharmacy>()
            {
                new Core.Pharmacy()
                {
                    Address = "Shevchenko street, 20",
                    Phone = "096-123-12-32",
                    Number = 123524,
                    OpenDate = new DateTime(2012, 8, 23)
                },
                new Core.Pharmacy()
                {
                    Address = "Best ave, 99a",
                    Phone = "001-424-12-32",
                    Number = 542212,
                    OpenDate = new DateTime(2013, 10, 3)
                },
                new Core.Pharmacy()
                {
                    Address = "Lenina street, 323b",
                    Phone = "096-000-00-55",
                    Number = 123524,
                    OpenDate = new DateTime(2014, 12, 31)
                },
                new Core.Pharmacy()
                {
                    Address = "Shevchenko street, 1a",
                    Phone = "096-123-12-34",
                    Number = 123524,
                    OpenDate = new DateTime(2012, 8, 23)
                }
            };
            pharmacies.ForEach(p => context.Pharmacies.Add(p));
            context.SaveChanges();
            
            #endregion Pharmacies

            #region Medicaments

            var medicaments = new List<Medicament>()
            {
                new Medicament()
                {
                    Name = "QQzolon",
                    Description = "Description, realy",
                    Price = 102.12m,
                    SerialNumber = "123-456-78"
                },
                new Medicament()
                {
                    Name = "WWzolon",
                    Description = "Description, realy",
                    Price = 2.12m,
                    SerialNumber = "333-126-xx"
                },
                new Medicament()
                {
                    Name = "OPzolon",
                    Description = "Description, realy",
                    Price = 42m,
                    SerialNumber = "3xa-902-we"
                },
                new Medicament()
                {
                    Name = "RQzolon",
                    Description = "Description, realy",
                    Price = 9.1m,
                    SerialNumber = "123-000-78"
                },
                new Medicament()
                {
                    Name = "XOzolon",
                    Description = "Description, realy",
                    Price = 1020.01m,
                    SerialNumber = "abb-arT-W1"
                }
            };
            medicaments.ForEach(m => context.Medicines.Add(m));
            context.SaveChanges();

            #endregion Medicaments

            #region Storages

            var storages = new List<Storage>()
            {
                new Storage()
                {
                    PharmacyId = 1,
                    MedicamentId = 2,
                    Quantity = 21
                },
                new Storage()
                {
                    PharmacyId = 1,
                    MedicamentId = 1,
                    Quantity = 20
                },
                new Storage()
                {
                    PharmacyId = 1,
                    MedicamentId = 3,
                    Quantity = 4
                },
                new Storage()
                {
                    PharmacyId = 2,
                    MedicamentId = 4,
                    Quantity = 9
                },
                new Storage()
                {
                    PharmacyId = 3,
                    MedicamentId = 2,
                    Quantity = 123
                }
            };
            storages.ForEach(s => context.Storages.Add(s));
            context.SaveChanges();

            #endregion Storages

            #region Orders

            var orders = new List<Order>()
            {
                new Order()
                {
                    Type = OperationType.Purchase,
                    Date = new DateTime(2014, 11, 21),
                    PharmacyId = 1
                },
                new Order()
                {
                    Type = OperationType.Sale,
                    Date = new DateTime(2014, 12, 21),
                    PharmacyId = 1
                },
                new Order()
                {
                    Type = OperationType.Purchase,
                    Date = new DateTime(2014, 10, 24),
                    PharmacyId = 1
                },
                new Order()
                {
                    Type = OperationType.Purchase,
                    Date = new DateTime(2014, 9, 10),
                    PharmacyId = 2
                },
                new Order()
                {
                    Type = OperationType.Sale,
                    Date = new DateTime(2014, 1, 1),
                    PharmacyId = 3
                }
            };
            orders.ForEach(o => context.Orders.Add(o));
            context.SaveChanges();

            #endregion Orders

            #region OrderDetailses

            var orderDetailses = new List<OrderDetails>()
            {
                new OrderDetails()
                {
                    MedicamentId = 1,
                    OrderId = 1,
                    Price = 20m,
                    Quantity = 21
                },
                new OrderDetails()
                {
                    MedicamentId = 2,
                    OrderId = 1,
                    Price = 55m,
                    Quantity = 1
                },
                new OrderDetails()
                {
                    MedicamentId = 1,
                    OrderId = 2,
                    Price = 24m,
                    Quantity = 23
                }
            };
            orderDetailses.ForEach(od => context.OrderDetailses.Add(od));
            context.SaveChanges();

            #endregion OrderDetailses

            #region MedicamentPriceHistories

            var medicamentPriceHistories = new List<MedicamentPriceHistory>()
            {
                new MedicamentPriceHistory()
                {
                    ModifiedDate = new DateTime(2013, 11, 1),
                    Price = 32.1m,
                    MedicamentId = 1
                },
                new MedicamentPriceHistory()
                {
                    ModifiedDate = new DateTime(2013, 12, 14),
                    Price = 1.1m,
                    MedicamentId = 2
                },
                new MedicamentPriceHistory()
                {
                    ModifiedDate = new DateTime(2014, 11, 9),
                    Price = 312.1m,
                    MedicamentId = 1
                }
            };
            medicamentPriceHistories.ForEach(mph => context.MedcinePriceHistories.Add(mph));
            context.SaveChanges();

            #endregion MedicamentPriceHistories
        }
    }
}
