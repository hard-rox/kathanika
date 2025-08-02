import {faker} from "@faker-js/faker/locale/en";

describe('Quick Add Books Feature', () => {
    beforeEach(() => {
        // Visit the bib record create page (quick add books)
        cy.visit('/cataloging/bibs/add');
    });

    it('should successfully add a single book with complete information', () => {
        const bookData = {
            title: faker.book.title(),
            author: faker.person.fullName(),
            isbn: faker.string.numeric(13),
            publisher: faker.company.name(),
            yearOfPublication: faker.date.past({years: 20}).getFullYear(),
            language: 'English',
            numberOfPages: faker.number.int({min: 50, max: 800}),
            edition: faker.helpers.arrayElement(['First Edition', 'Second Edition', 'Revised Edition', 'Third Edition']),
            category: faker.helpers.arrayElement(['Fiction', 'Non-Fiction', 'Science', 'History', 'Biography', 'Technology']),
            description: faker.lorem.paragraph(3)
        };

        // Fill out the form
        cy.get('input[id="title"]').type(bookData.title);
        cy.get('input[id="author"]').type(bookData.author);
        cy.get('input[id="isbn"]').type(bookData.isbn);
        cy.get('input[id="publisher"]').type(bookData.publisher);
        cy.get('input[id="yearOfPublication"]').type(bookData.yearOfPublication.toString());
        cy.get('input[id="language"]').type(bookData.language);
        cy.get('input[id="numberOfPages"]').type(bookData.numberOfPages.toString());
        cy.get('input[id="edition"]').type(bookData.edition);
        cy.get('textarea[id="description"]').type(bookData.description);

        // Submit the form
        cy.get('form').submit();
        cy.contains(bookData.title).should('be.visible');
    });

    it('should successfully add a book with minimal required information', () => {
        const minimalBookData = {
            title: faker.book.title(),
            author: faker.person.fullName(),
            publisher: faker.company.name(),
            yearOfPublication: faker.date.past({years: 10}).getFullYear()
        };

        // Fill only required fields
        cy.get('input[id="title"]').type(minimalBookData.title);
        cy.get('input[id="author"]').type(minimalBookData.author);
        cy.get('input[id="publisher"]').type(minimalBookData.publisher);
        cy.get('input[id="yearOfPublication"]').type(minimalBookData.yearOfPublication.toString());

        // Submit the form
        cy.get('input[type="submit"][value="Save Book"]').click();

        cy.contains(minimalBookData.title).should('be.visible');
    });

    it('should add multiple books in succession', () => {
        const books = Array.from({length: 3}, () => ({
            title: faker.book.title(),
            author: faker.person.fullName(),
            isbn: faker.string.numeric(13),
            publisher: faker.company.name(),
            yearOfPublication: faker.date.past({years: 15}).getFullYear(),
            category: faker.helpers.arrayElement(['Fiction', 'Non-Fiction', 'Science', 'Biography'])
        }));

        books.forEach((book, index) => {
            // If not the first book, navigate back to add page
            if (index > 0) {
                cy.visit('/cataloging/bibs/add');
            }

            // Fill out the form
            cy.get('input[id="title"]').type(book.title);
            cy.get('input[id="author"]').type(book.author);
            cy.get('input[id="isbn"]').type(book.isbn);
            cy.get('input[id="publisher"]').type(book.publisher);
            cy.get('input[id="yearOfPublication"]').type(book.yearOfPublication.toString());

            // Submit the form
            cy.get('input[type="submit"][value="Save Book"]').click();

            cy.contains(book.title).should('be.visible');
        });
    });

    it('should handle special characters and international titles', () => {
        const internationalBookData = {
            title: 'Café de Flore: A Parisian Story',
            author: 'François Müller',
            isbn: faker.string.numeric(13),
            publisher: 'Éditions Gallimard',
            yearOfPublication: faker.date.past({years: 8}).getFullYear(),
            language: 'French',
            numberOfPages: faker.number.int({min: 200, max: 400}),
            category: 'Literature',
            description: 'Une histoire captivante qui se déroule dans le célèbre café parisien...'
        };

        // Fill form with international characters
        cy.get('input[id="title"]').type(internationalBookData.title);
        cy.get('input[id="author"]').type(internationalBookData.author);
        cy.get('input[id="isbn"]').type(internationalBookData.isbn);
        cy.get('input[id="publisher"]').type(internationalBookData.publisher);
        cy.get('input[id="yearOfPublication"]').type(internationalBookData.yearOfPublication.toString());
        cy.get('input[id="language"]').type(internationalBookData.language);
        cy.get('input[id="numberOfPages"]').type(internationalBookData.numberOfPages.toString());
        cy.get('textarea[id="description"]').type(internationalBookData.description);

        // Submit and verify
        cy.get('input[type="submit"][value="Save Book"]').click();
        cy.contains(internationalBookData.title).should('be.visible');
    });

    it('should generate random book data using faker for stress testing', () => {
        // Generate 5 random books quickly
        for (let i = 0; i < 5; i++) {
            const randomBook = {
                title: faker.book.title(),
                author: `${faker.person.lastName()}, ${faker.person.firstName()}`,
                isbn: `978${faker.string.numeric(10)}`,
                publisher: faker.company.name(),
                yearOfPublication: faker.date.between({from: '2000-01-01', to: '2023-12-31'}).getFullYear(),
                language: faker.helpers.arrayElement(['English', 'Spanish', 'French', 'German', 'Italian']),
                numberOfPages: faker.number.int({min: 50, max: 1000}),
                edition: faker.helpers.arrayElement(['First', 'Second', 'Third', 'Revised', 'Updated']),
                description: faker.lorem.sentences(faker.number.int({min: 2, max: 5}))
            };

            // If not first iteration, navigate back
            if (i > 0) {
                cy.visit('/cataloging/bibs/add');
            }

            // Fill all fields rapidly
            Object.entries(randomBook).forEach(([field, value]) => {
                if (field === 'description') {
                    cy.get(`textarea[id="${field}"]`).type(value.toString(), {delay: 0});
                } else {
                    cy.get(`input[id="${field}"]`).type(value.toString(), {delay: 0});
                }
            });

            // Submit
            cy.get('input[type="submit"][value="Save Book"]').click();
            cy.contains(randomBook.title).should('be.visible');
        }
    });
});
