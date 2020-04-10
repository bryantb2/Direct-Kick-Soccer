using dropShippingApp.Controllers;
using dropShippingApp.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace InvoiceRepoTest
{
    /*public class InvoiceTests : IDisposable
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
            PricingHistory p = new PricingHistory()
            {
                NewPrice = 15,
                DateChanged = DateTime.Parse("3/1/2020")
            };
            PricingHistory p2 = new PricingHistory()
            {
                NewPrice = 15,
                DateChanged = DateTime.Parse("3/1/2020")
            };

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

        public void Dispose()
        {
            repo = null;
            controller = null;
            invoice = null;
            invoiceItem = null;
            invoiceItem2 = null;
            prod = null;
            prod2 = null;
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
            //act
            await controller.CreateInvoice(invoice);
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

        /*[Fact]
        public void AddInvoiceItem()
        {
            //arrange

            //act
            InvoiceItem i= controller.CreateInvoice(prod, 1, 1,1);
               
            //assert
            foreach(InvoiceItem item in invoice.InvoiceItems)
            {
                if(i.PurchasedProduct==item.PurchasedProduct)
                {
                    Assert.Equal(i, item);
                }
            }
                 
        }*/
    //}
}
