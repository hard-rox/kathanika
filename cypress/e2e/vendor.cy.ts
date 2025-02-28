import {faker} from "@faker-js/faker/locale/en";

describe('Vendor Form Test', () => {
    beforeEach(() => {
        // Visit the page containing the form
        cy.visit('/vendors/add');
    });

    it('should fill out and submit the vendor form', () => {
        const vendorName = faker.company.name();

        cy.get('input[id="name"]').type(vendorName);
        cy.get('textarea[id="accountDetail"]').type(faker.finance.accountName());
        cy.get('textarea[id="address"]').type(faker.location.streetAddress());
        cy.get('input[id="contactNumber"]').type(faker.phone.number({style: 'international'}));
        cy.get('input[id="contactPersonEmail"]').type(faker.internet.email());
        cy.get('input[id="contactPersonName"]').type(faker.person.fullName());
        cy.get('input[id="contactPersonPhone"]').type(faker.phone.number({style: 'international'}));
        cy.get('input[id="email"]').type(faker.internet.email());
        cy.get('input[id="website"]').type(faker.internet.url());

        // Submit the form
        cy.get('form').submit();

        // Add assertions to verify form submission
        cy.contains(vendorName).should('be.visible');
    });
});
