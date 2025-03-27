import {faker} from "@faker-js/faker/locale/en";

describe('Purchase order Form Test', () => {
    beforeEach(() => {
        // Visit the page containing the form
        cy.visit('/purchase-orders/create');
    });

    it('should fill out and submit the vendor form', () => {
        cy.get('[id="vendor-search"]').click();
        cy.get('[id="vendor-search-results"]')
            .children()
            .first()
            .click();
        cy.contains('+ Add Item').focus();
        
        for (let i = 1; i < faker.number.int({min: 1, max: 4}); i++) {
            cy.contains('+ Add Item').click();
            cy.get('[name="purchaseItemForm"]').within(() => {
                cy.get('input[id="title"]').type(faker.book.title());
                cy.get('input[id="quantity"]').type(faker.number.int({min: 1, max: 10}).toString());

                const random = Math.ceil(Math.random() * 10);

                if (random % 2 == 1)
                    cy.get('input[id="vendorPrice"]').type(faker.number.float(10000).toString());
                
                if (random % 2 == 1)
                    cy.get('input[id="author"]').type(faker.book.author());

                if (random % 2 == 0)
                    cy.get('textarea[id="internalNote"]').type(faker.lorem.paragraph(3));

                if (random % 2 == 1)
                    cy.get('input[id="publisher"]').type(faker.company.name());

                if (random % 2 == 0)
                    cy.get('input[id="publishingYear"]').type(faker.date.past().getFullYear().toString());

                if (random % 2 == 1)
                    cy.get('textarea[id="vendorNote"]').type(faker.lorem.paragraph(3));
            });
            cy.get('[name="purchaseItemForm"]').submit();
        }

        cy.get('textarea[id="vendorNote"]').type(faker.lorem.paragraph(1));
        cy.get('textarea[id="internalNote"]').type(faker.lorem.paragraph(1));
        cy.wait(300);
        cy.get('form[name="purchaseOrderForm"]').submit();
    });
});
