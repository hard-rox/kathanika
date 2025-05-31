import {faker} from "@faker-js/faker/locale/en";

describe('Book Record Form', () => {
    beforeEach(() => {
        // Visit the bib record create page
        cy.visit('cataloging/create');
    });

    it('should display form with correct fields', () => {
        // Check panel title
        cy.contains('Create Book Bibliographic Record').should('be.visible');

        // Check all form fields are present
        cy.get('form').within(() => {
            cy.contains('label', 'Title').should('be.visible');
            cy.contains('label', 'ISBN').should('be.visible');
            cy.contains('label', 'Author').should('be.visible');
            cy.contains('label', 'Publisher Name').should('be.visible');
            cy.contains('label', 'Publication Date').should('be.visible');
            cy.contains('label', 'Extent').should('be.visible');
            cy.contains('label', 'Dimensions').should('be.visible');
        });
    });

    it('should show placeholder text for each field', () => {
        // Check placeholder texts
        cy.get('input[id="title"]').should('have.attr', 'placeholder', 'The great Gatsby');
        cy.get('input[id="isbn"]').should('have.attr', 'placeholder', '9781234567890');
        cy.get('input[id="author"]').should('have.attr', 'placeholder', 'Fitzgerald, F. Scott');
        cy.get('input[id="publisherName"]').should('have.attr', 'placeholder', 'Oxford University Press');
        cy.get('input[id="publicationDate"]').should('have.attr', 'placeholder', '2023');
        cy.get('input[id="extent"]').should('have.attr', 'placeholder', '325 pages');
        cy.get('input[id="dimensions"]').should('have.attr', 'placeholder', '23 cm');
    });

    it('should successfully submit form with all fields filled', () => {
        // Mock GraphQL response
        cy.intercept('POST', '/graphql', (req) => {
            if (req.body.operationName === 'createBibRecord') {
                req.reply({
                    body: {
                        data: {
                            createBibRecord: {
                                message: 'BibRecord created successfully',
                                data: {id: '123'},
                                errors: null
                            }
                        }
                    }
                });
            }
        }).as('createBibRecord');

        // Fill out the form
        cy.get('input[id="title"]').type('Test Book Title');
        cy.get('input[id="isbn"]').type('9781234567890');
        cy.get('input[id="author"]').type('Smith, John');
        cy.get('input[id="publisherName"]').type('Test Publisher');
        cy.get('input[id="publicationDate"]').type('2023');
        cy.get('input[id="extent"]').type('200 pages');
        cy.get('input[id="dimensions"]').type('25 cm');

        // Submit the form
        cy.get('input[type="submit"]').click();

        // Wait for API call and verify it was made with correct data
        cy.wait('@createBibRecord').its('request.body.variables.input').should('deep.include', {
            title: 'Test Book Title',
            isbn: '9781234567890',
            author: 'Smith, John',
            publisherName: 'Test Publisher',
            publicationDate: '2023',
            extent: '200 pages',
            dimensions: '25 cm'
        });

        // Check toast message appears
        cy.contains('BibRecord created successfully').should('be.visible');
    });

    it('should show error messages when API returns errors', () => {
        // Mock GraphQL error response
        cy.intercept('POST', '/graphql', {
            body: {
                data: {
                    createBibRecord: {
                        message: null,
                        data: null,
                        errors: [
                            {
                                __typename: 'ValidationError',
                                fieldName: 'title',
                                message: 'Title is too short'
                            },
                            {
                                __typename: 'Error',
                                message: 'General error occurred'
                            }
                        ]
                    }
                }
            }
        }).as('createBibRecordError');

        // Fill required field and submit
        cy.get('input[id="title"]').type('Test');
        cy.get('input[type="submit"]').click();

        // Verify error alert is shown with error messages
        cy.contains('title - Title is too short').should('be.visible');
        cy.contains('General error occurred').should('be.visible');

        // Verify alert can be closed
        cy.get('kn-alert button').click();
        cy.contains('title - Title is too short').should('not.exist');
    });

    it('should display MARC21 help information', () => {
        // Verify help section exists
        cy.contains('MARC21 Bibliographic Record Structure:').should('be.visible');

        // Verify help section has correct content
        cy.contains('Title (245$a):').should('be.visible');
        cy.contains('ISBN (020$a):').should('be.visible');
        cy.contains('Author (100$a):').should('be.visible');
        cy.contains('Publisher (260/264$b):').should('be.visible');
        cy.contains('Date (260/264$c):').should('be.visible');
        cy.contains('Extent (300$a):').should('be.visible');
        cy.contains('Dimensions (300$c):').should('be.visible');
    });
    
    // This test uses the real API and should be skipped in CI environments
    // or when running tests against a non-development environment
    it('should create a record using the API', () => {
        // Generate a unique title to avoid duplicate records
        const uniqueTitle = `Test Book ${Date.now()}`;

        // Fill out the form with test data
        cy.get('input[id="title"]').type(uniqueTitle);
        cy.get('input[id="isbn"]').type('9780123456789');
        cy.get('input[id="author"]').type('Cypress, Test');
        cy.get('input[id="publisherName"]').type('Cypress Publishing');
        cy.get('input[id="publicationDate"]').type('2023');
        cy.get('input[id="extent"]').type('150 pages');
        cy.get('input[id="dimensions"]').type('20 cm');

        // Listen for the network request without mocking
        cy.intercept('POST', '/graphql').as('createBibRecordReal');

        // Submit the form
        cy.get('input[type="submit"]').click();

        // Wait for the real API response
        cy.wait('@createBibRecordReal').then((interception) => {
            // Check that the request was made with the correct data
            expect(interception.request.body.variables.input).to.include({
                title: uniqueTitle,
                isbn: '9780123456789',
                author: 'Cypress, Test'
            });

            // Verify the response has the expected structure (but don't assert specific values)
            expect(interception.response?.statusCode).to.be.oneOf([200, 201]);
            expect(interception.response?.body).to.have.property('data');
            expect(interception.response?.body.data).to.have.property('createBibRecord');
        });

        // Check for success message
        cy.contains('BibRecord created').should('be.visible');

        // Verify navigation to the new record page
        cy.url().should('include', '/purchase-orders/');
    });

    // This test creates multiple book records using faker.js for random data with the real API
    it('should create multiple records with faker data', () => {
        // Reduce the number of records for faster test execution
        const numberOfRecords = 10;

        Cypress._.times(numberOfRecords, (index) => {
            // Visit the page before each iteration
            cy.visit('cataloging/create');

            // Generate random book data using faker
            const bookTitle = faker.lorem.words(3);
            const isbn = faker.string.numeric(13); // 13-digit ISBN
            const author = `${faker.person.lastName()}, ${faker.person.firstName()}`;
            const publisherName = faker.company.name();
            const publicationDate = faker.date.between({from: '1900-01-01', to: '2023-12-31'}).getFullYear().toString();
            const pages = faker.number.int({min: 50, max: 1000});
            const extent = `${pages} pages`;
            const dimensions = `${faker.number.int({min: 15, max: 30})} cm`;

            // Log which iteration we're on
            cy.log(`Creating book record ${index + 1} of ${numberOfRecords}: ${bookTitle}`);

            // Fill out the form with faker data
            cy.get('input[id="title"]').type(bookTitle);
            cy.get('input[id="isbn"]').type(isbn);
            cy.get('input[id="author"]').type(author);
            cy.get('input[id="publisherName"]').type(publisherName);
            cy.get('input[id="publicationDate"]').type(publicationDate);
            cy.get('input[id="extent"]').type(extent);
            cy.get('input[id="dimensions"]').type(dimensions);

            // Submit the form directly without intercepting
            cy.get('input[type="submit"]').click();

            // Verify success message appears
            cy.contains('New bib record with control number', { timeout: 10000 }).should('be.visible');

            // Verify navigation to the new record page
            cy.url().should('include', '/cataloging/');

            // Optional: Verify the record was actually created by checking if it appears in a list view
            // or by navigating to another page and then back to ensure the data persists

            // Small delay to ensure navigation completes before next iteration
            cy.wait(500);
        });
    });

});
