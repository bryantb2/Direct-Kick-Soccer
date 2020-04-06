using dropShippingApp.Controllers;
using dropShippingApp.Models;
using dropShippingApp.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace InvoiceRepoTest
{
    public class InvoiceTests
    {
        IInvoiceRepo repo;
        InvoicesController controller;
        Invoice invoice;
        InvoiceItem invoiceItem, invoiceItem2;
        CustomProduct prod;
        CustomProduct prod2;
        public InvoiceTests()
        {
            // repo and controller setup
            repo = new FakeInvoiceRepo();
            controller = new InvoicesController(repo);

            // pricing history setup
            PricingHistory p = new PricingHistory();
            PricingHistory p2 = new PricingHistory();
            p.NewPrice = 15;
            p.DateChanged = DateTime.Parse("3/1/2020");
            p2.NewPrice = 15;
            p2.DateChanged = DateTime.Parse("3/1/2020");
            List<PricingHistory> pList = new List<PricingHistory>() { p };

            // product 1 setup
            prod = new CustomProduct
            {
                CustomProductID = 1,
                ProductTitle = "A Prod"
            };
            prod.AddPricingHistory(p);

            // product 2 setup
            prod2 = new CustomProduct
            {
                CustomProductID = 2,
                ProductTitle = "A Prod2"
            };
            prod2.AddPricingHistory(p2);

            // create invoice items
            invoiceItem = new InvoiceItem
            {
                InvoiceItemID = 1,
                PurchasedProduct = prod,
                ProductUnitPrice = 25,
                ItemQuantity = 1
            };
            invoiceItem2 = new InvoiceItem
            {
                InvoiceItemID = 1,
                PurchasedProduct = prod2,
                ProductUnitPrice = 25,
                ItemQuantity = 1
            };
            invoice = new Invoice
            {
                InvoiceID = 1,
                DatePlaced = DateTime.Now,
            };
        }

        [Fact]
        public void CalculateGrandTotal()
        {
            // Arrange
            // Done in the constructor
            invoice.InvoiceItems.Add(invoiceItem);
            invoice.InvoiceItems.Add(invoiceItem2);


            //Act
            decimal total = invoice.CalculateGrandTotal();

            //Assert
            Assert.Equal(30, total);
        }

        [Fact]
        public async Task CreateInvoice()
        {
            //arrange
            Invoice invoice = new Invoice();
            //act
            await controller.Create(invoice);
            //assert
            Assert.Contains<Invoice>(invoice, repo.Invoices);
        }

        [Fact]
        public void RemoveInvoiceItem()
        {
            // Arrange
            // Done in the constructor
            invoice.InvoiceItems.Add(invoiceItem);
            //Act
            InvoiceItem myInvoice = invoice.RemoveInvoiceItem(1);
            //Assert
            Assert.Equal(invoiceItem, myInvoice);
        }

        [Fact]
        public void AddInvoiceItem()
        {
            //arrange

            //act
            InvoiceItem i= controller.CreateInvoiceItem(prod, 1, 1,1);
               
            //assert
            foreach(InvoiceItem item in invoice.InvoiceItems)
            {
                if(i.PurchasedProduct==item.PurchasedProduct)
                {
                    Assert.Equal(i, item);
                }
            }
                 
        }
    }
}
